using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using Rop.Results9;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.Basic
{
    internal partial class _dummy { };

    public partial class FormSc : Form, IForm9
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SynchronizationContext SynchronizationContext { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Thread MainThread { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IResult? FormResult { get; set; }
        public Result<T> GetResult<T>() => FormResult.ToResult<T>();
        public EnumerableResult<T> GetEnumerableResult<T>() => FormResult.ToEnumerableResult<T>();
        protected Font OldFont { get; }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control?.Font.Equals(OldFont)??false) e.Control.Font = this.Font;
            base.OnControlAdded(e);
        }
        public bool IsCurrentContext() => SynchronizationContext == SynchronizationContext.Current;
        public ToolTip ToolTip { get; } = new();
        public void SetToolTip(Control control, string text) => ToolTip.SetToolTip(control,string.IsNullOrEmpty(text)?null:text);
        public FormSc() : base()
        {
            StartPosition = FormStartPosition.CenterParent;
            MainThread = Thread.CurrentThread;
            SynchronizationContext=SynchronizationContext.Current??throw new NullReferenceException("Synchonization Context is null");
            FormResult = null;
            // ReSharper disable once VirtualMemberCallInConstructor
            OldFont = this.Font;
            if (OldFont.Name.StartsWith("Microsoft Sans Serif")) 
                // ReSharper disable once VirtualMemberCallInConstructor
                this.Font = WinFormsHelper.GetDefaultSystemFont();
            this.Shown += FormSc_Shown;   
        }

        public virtual void Init()
        {
        }

        public virtual async ValueTask InitAsync()
        {
            await Task.CompletedTask;
        }
        private async void FormSc_Shown(object? sender, EventArgs e)
        {
            Init();
            await InitAsync();
        }
        private readonly SemaphoreSlim _locknoreenter=new(1, 1);
        public async Task LockPost(Action accion)
        {
            await _locknoreenter.WaitAsync();
            try
            {
                Post(accion);
            }
            finally
            {
                _locknoreenter.Release();
            }
        }
        public async Task LockPost<T>(Action<T> accion, T variable)
        {
            await _locknoreenter.WaitAsync();
            try
            {
                Post(accion, variable);
            }
            finally
            {
                _locknoreenter.Release();
            }
        }
        public void Post<T>(Action<T> accion, T variable) => SynchronizationContext.Post(v => accion((T)v!), variable);

        public void Send<T>(Action<T> accion, T state) => SynchronizationContext.Send(o => accion((T)o!), state);

        public void Post(Action accion) => SynchronizationContext.Post(o => accion(), null);

        public void Send(Action accion) => SynchronizationContext.Send(o => accion(), null);

        public void DoExit(bool showerror = false)
        {
            var error = FormResult?.Error;
            if (showerror && (error!=null))
                ShowError(error.ToString());
            DialogResult = (error!=null)? DialogResult.Cancel : DialogResult.OK;
            Close();
        }
        public void DoExit(IResult formresult, bool showerror = false)
        {
            FormResult = formresult;
            DoExit(showerror);
        }
        public void DoExitFailed(Error error, bool showerror = false)
        {
            FormResult = new VoidResult(error);
            DoExit(showerror);
        }
        public void DoExitOk() => DoExit(VoidResult.Ok);
        public void DoExitOk<T>(T result) => DoExit(Result.Ok(result));
        public void ShowError(string error) => MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public void ShowError(Error error) => ShowError(error.ToString());
        public void ShowInfo(string info) => MessageBox.Show(info, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public DialogResult ShowYesNo(string msg,string caption) => MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public DialogResult ShowOkCancel(string msg,string caption) => MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
    }
}