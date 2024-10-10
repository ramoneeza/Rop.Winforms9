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
            // FormDlg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "FormDlg";
            Text = "FormDlg";
            DownPanel.ResumeLayout(false);
            DownPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}