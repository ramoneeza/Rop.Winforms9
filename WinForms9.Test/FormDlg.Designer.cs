namespace WinForms9.Test
{
    partial class FormDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            concurrentBar1 = new Rop.Winforms9.Controls.ConcurrentBar();
            DownPanel.SuspendLayout();
            SuspendLayout();
            // 
            // BtnExit
            // 
            BtnExit.Location = new Point(724, 3);
            BtnExit.Size = new Size(73, 26);
            // 
            // BtnOk
            // 
            BtnOk.Size = new Size(73, 26);
            // 
            // DownPanel
            // 
            DownPanel.Location = new Point(0, 418);
            DownPanel.Size = new Size(800, 32);
            // 
            // concurrentBar1
            // 
            concurrentBar1.BarBackground = Color.Empty;
            concurrentBar1.BarColor = Color.CornflowerBlue;
            concurrentBar1.BorderColor = Color.Gray;
            concurrentBar1.Font = new Font("Segoe UI", 9F);
            concurrentBar1.IsMarquee = true;
            concurrentBar1.Location = new Point(131, 210);
            concurrentBar1.Name = "concurrentBar1";
            concurrentBar1.Size = new Size(539, 30);
            concurrentBar1.TabIndex = 2;
            concurrentBar1.Text = "concurrentBar1";
            // 
            // FormDlg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(concurrentBar1);
            Name = "FormDlg";
            Text = "FormDlg";
            Shown += FormDlg_Shown;
            Controls.SetChildIndex(DownPanel, 0);
            Controls.SetChildIndex(concurrentBar1, 0);
            DownPanel.ResumeLayout(false);
            DownPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Rop.Winforms9.Controls.ConcurrentBar concurrentBar1;
    }
}