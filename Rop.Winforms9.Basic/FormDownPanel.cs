using System.ComponentModel;
using System.Drawing;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.Basic
{
    internal partial class _dummy { };
    [DefaultEvent(nameof(FormDownPanel.ExitButtonClick))]
    public partial class FormDownPanel : FormSc
    {
        protected Button BtnExit;
        protected Button BtnOk;
        protected Panel DownPanel;
        public event EventHandler? ExitButtonClick;
        public event EventHandler? OkButtonClick;
        protected virtual Button FactoryDownButton() => new Button();
        public FormDownPanel() : base()
        {
            this.DownPanel = new System.Windows.Forms.Panel();
            // ReSharper disable once VirtualMemberCallInConstructor
            this.BtnExit = FactoryDownButton();
            // ReSharper disable once VirtualMemberCallInConstructor
            this.BtnOk = FactoryDownButton();
            InitializeComponent();
            this.SizeChanged+=(_,_)=> _ajPosition();
            DownPanel.SizeChanged += (_,_)=> _ajPosition();
            DownPanel.PaddingChanged += (_,_)=> _ajPosition();
            BtnExit.Click += Btnexit_Click;
            BtnExit.SizeChanged += (_,_)=> _ajPosition();
            BtnOk.Click += BtnOk_Click;
            BtnOk.SizeChanged +=  (_,_)=> _ajPosition();
        }
        private bool _btnokVisible = false;
        [DefaultValue(false)]
        public bool BtnOkVisible
        {
            get => _btnokVisible;
            set
            {
                _btnokVisible = value;
                AjBtnOkVisibility();
            }
        }

        private void AjBtnOkVisibility()
        {
            if (_btnokVisible)
            {
                if (!DownPanel.Controls.Contains(BtnOk))
                {
                    DownPanel.Controls.Add(BtnOk);
                    _ajPosition();
                    
                }
            }
            else
            {
                if (DownPanel.Controls.Contains(BtnOk))
                {
                    DownPanel.Controls.Remove(BtnOk);
                }
            }
        }

        private void _ajPosition()
        {
            var h = DownPanel.Height - DownPanel.Padding.Vertical;
            var pok = new Point(DownPanel.Padding.Left, DownPanel.Padding.Top);
            var pexit = new Point(DownPanel.Width - DownPanel.Padding.Right - BtnExit.Width, DownPanel.Padding.Top);
            var sok = new Size(BtnOk.Width, h);
            var sexit = new Size(BtnExit.Width, h);
            BtnOk.SetBounds(pok.X,pok.Y,sok.Width,sok.Height);
            BtnExit.SetBounds(pexit.X,pexit.Y,sexit.Width,sexit.Height);
        }
        private void BtnOk_Click(object? sender, EventArgs e)
        {
            OnBtnOkClick();
        }
        protected virtual void OnBtnExitClick()
        {
            ExitButtonClick?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnBtnOkClick()
        {
            OkButtonClick?.Invoke(this, EventArgs.Empty);
        }
        private void Btnexit_Click(object? sender, EventArgs e)
        {
            OnBtnExitClick();
        }

        private void InitializeComponent()
        {
            
            this.DownPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // DownPanel
            //
            this.DownPanel.BackColor = System.Drawing.Color.White;
            this.DownPanel.Controls.Add(this.BtnExit);
            //this.DownPanel.Controls.Add(this.BtnOk);
            this.DownPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            //this.DownPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DownPanel.Location = new System.Drawing.Point(0, 232);
            this.DownPanel.Name = "DownPanel";
            this.DownPanel.Size = new System.Drawing.Size(this.Width, 29);
            this.DownPanel.TabIndex = 0;
            this.DownPanel.Padding = new Padding(3);

            //
            // _btnexit
            //
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.Margin = new System.Windows.Forms.Padding(3);
            this.BtnExit.Size = new System.Drawing.Size(73, 23);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Text =CommonString.Default.StrExit;
            this.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.AutoSize = true;
            this.BtnExit.Visible = true;
            this.BtnExit.Location = new Point(this.DownPanel.Width - BtnExit.Width, 0);
            
            //
            // _btnok
            //
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnOk.Margin = new System.Windows.Forms.Padding(3);
            this.BtnOk.Size = new System.Drawing.Size(73, 23);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text =CommonString.Default.StrOk;
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.AutoSize = true;
            this.BtnOk.Visible = true;
            
            //
            // FormDownPanel
            //
            this.ClientSize = new System.Drawing.Size(341, 261);
            this.Controls.Add(this.DownPanel);
            this.Name = "FormDownPanel";
            _ajPosition();
            this.DownPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}