using Rop.Results9;

namespace Rop.Winforms9.Basic;

public interface IForm9
{
    SynchronizationContext SynchronizationContext { get; }
    Thread MainThread { get; }
    IResult? FormResult { get; set; }
    Result<T> GetResult<T>();
    EnumerableResult<T> GetEnumerableResult<T>();
    bool IsCurrentContext();
    void SetToolTip(Control control, string text);
    void Init();
    ValueTask InitAsync();
    Task LockPost(Action accion);
    Task LockPost<T>(Action<T> accion, T variable);
    void Post<T>(Action<T> accion, T variable);
    void Post(Action accion);
    void Send<T>(Action<T> accion, T state);
    void Send(Action accion);
    void DoExit(bool showerror = false);
    void DoExit(IResult formresult, bool showerror = false);
    void DoExitFailed(Error error, bool showerror = false);
    void DoExitOk();
    void DoExitOk<T>(T result);
    void ShowError(string error);
    void ShowError(Error error);
    void ShowInfo(string info);
    DialogResult ShowYesNo(string msg,string caption);
    DialogResult ShowOkCancel(string msg,string caption);
}