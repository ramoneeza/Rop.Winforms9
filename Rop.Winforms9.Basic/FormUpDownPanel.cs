using System.ComponentModel;
using System.Drawing;

namespace Rop.Winforms9.Basic
{
    internal partial class _dummy { };

    public partial class FormUpDownPanel:FormDownPanel
    {
        protected Panel MainPanel;
        protected Panel PanelUp;
        public FormUpDownPanel():base()
        {
            PanelUp = new Panel();
            MainPanel = new Panel();
            InitializeComponent();
            MainPanel.BackColorChanged += MainPanel_BackColorChanged;
        }

        private void MainPanel_BackColorChanged(object? sender, EventArgs e)
        {
            base.BackColor = MainPanel.BackColor;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                MainPanel.BackColor = value;
            }
        }
        private void InitializeComponent()
        {
            
            SuspendLayout();
            // 
            // PanelUp
            // 
            PanelUp.BackColor = SystemColors.Control;
            PanelUp.Dock = DockStyle.Top;
            //this.PanelUp.Font = new System.Drawing.Font("Segoe UI", 9F);
            PanelUp.Location = new Point(0, 0);
            PanelUp.Name = "PanelUp";
            PanelUp.Size = new Size(341, 37);
            PanelUp.TabIndex = 1;
            // 
            // MainPanel
            // 
            MainPanel.Dock = DockStyle.Fill;
            //this.MainPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            MainPanel.Location = new Point(0, 37);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(341, 195);
            MainPanel.TabIndex = 2;
            // 
            // FormUpDownPanel
            // 
            ClientSize = new Size(341, 261);
            Controls.Add(MainPanel);
            Controls.Add(PanelUp);
            Name = "FormUpDownPanel";
            Controls.SetChildIndex(DownPanel, 0);
            Controls.SetChildIndex(PanelUp, 0);
            Controls.SetChildIndex(MainPanel, 0);
            ResumeLayout(false);

        }
    }
}
