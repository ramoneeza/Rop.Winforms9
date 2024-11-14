namespace WinForms9.Test
{
    partial class Form2
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
            button1 = new Button();
            keyValueLabel1 = new Rop.Winforms9.KeyValueListComboBox.KeyValueLabel();
            keyValueComboBox1 = new Rop.Winforms9.KeyValueListComboBox.KeyValueComboBox();
            keyValueListBox1 = new Rop.Winforms9.KeyValueListComboBox.KeyValueListBox();
            nullDateOnlyPicker1 = new Rop.Winforms9.Controls.NullDateOnlyPicker();
            concurrentBar1 = new Rop.Winforms9.Controls.ConcurrentBar();
            MainPanel.SuspendLayout();
            DownPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.BackColor = Color.FromArgb(255, 192, 192);
            MainPanel.Controls.Add(concurrentBar1);
            MainPanel.Controls.Add(nullDateOnlyPicker1);
            MainPanel.Controls.Add(keyValueListBox1);
            MainPanel.Controls.Add(keyValueComboBox1);
            MainPanel.Controls.Add(keyValueLabel1);
            MainPanel.Controls.Add(button1);
            MainPanel.Size = new Size(800, 369);
            // 
            // PanelUp
            // 
            PanelUp.BackColor = SystemColors.ControlDark;
            PanelUp.Size = new Size(800, 37);
            // 
            // BtnExit
            // 
            BtnExit.Location = new Point(724, 3);
            BtnExit.Size = new Size(73, 38);
            // 
            // BtnOk
            // 
            BtnOk.Font = new Font("Segoe UI", 9F);
            BtnOk.Size = new Size(73, 38);
            // 
            // DownPanel
            // 
            DownPanel.Location = new Point(0, 406);
            DownPanel.Size = new Size(800, 44);
            // 
            // button1
            // 
            button1.Location = new Point(227, 122);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // keyValueLabel1
            // 
            keyValueLabel1.BackColor = SystemColors.Control;
            keyValueLabel1.BorderColor = Color.Gray;
            keyValueLabel1.CenterMargin = 0;
            keyValueLabel1.LeftIconSpaces = 0;
            keyValueLabel1.Location = new Point(319, 259);
            keyValueLabel1.Name = "keyValueLabel1";
            keyValueLabel1.RightIconSpaces = 0;
            keyValueLabel1.RowStyleAltColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(Color.Empty, Color.Empty, Color.Empty);
            keyValueLabel1.RowStyleColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(Color.Empty, Color.Empty, Color.Empty);
            keyValueLabel1.RowStyleSelectedColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(Color.Empty, Color.Empty, Color.Empty);
            keyValueLabel1.Size = new Size(293, 23);
            keyValueLabel1.TabIndex = 3;
            keyValueLabel1.Text = "keyValueLabel1";
            keyValueLabel1.TopMargin = 2;
            // 
            // keyValueComboBox1
            // 
            keyValueComboBox1.BorderColor = Color.Empty;
            keyValueComboBox1.CenterMargin = 0;
            keyValueComboBox1.FormattingEnabled = true;
            keyValueComboBox1.LeftIconSpaces = 0;
            keyValueComboBox1.Location = new Point(78, 259);
            keyValueComboBox1.Name = "keyValueComboBox1";
            keyValueComboBox1.RightIconSpaces = 0;
            keyValueComboBox1.RowStyleAltColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(SystemColors.Control, Color.Empty, Color.Empty);
            keyValueComboBox1.RowStyleColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(Color.White, Color.Empty, Color.Empty);
            keyValueComboBox1.RowStyleSelectedColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(SystemColors.Highlight, Color.White, SystemColors.Highlight);
            keyValueComboBox1.Size = new Size(224, 24);
            keyValueComboBox1.TabIndex = 4;
            keyValueComboBox1.TopMargin = 0;
            // 
            // keyValueListBox1
            // 
            keyValueListBox1.BackColor = SystemColors.ControlDark;
            keyValueListBox1.BorderColor = Color.Empty;
            keyValueListBox1.CenterMargin = 0;
            keyValueListBox1.FormattingEnabled = true;
            keyValueListBox1.IntegralHeight = false;
            keyValueListBox1.ItemHeight = 15;
            keyValueListBox1.LeftIconSpaces = 0;
            keyValueListBox1.Location = new Point(462, 43);
            keyValueListBox1.Name = "keyValueListBox1";
            keyValueListBox1.RightIconSpaces = 0;
            keyValueListBox1.RowStyleAltColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(SystemColors.Control, Color.Empty, Color.Empty);
            keyValueListBox1.RowStyleColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(Color.White, Color.Empty, Color.Empty);
            keyValueListBox1.RowStyleSelectedColorSet = new Rop.Winforms9.KeyValueListComboBox.ColorSet(SystemColors.Highlight, Color.White, SystemColors.Highlight);
            keyValueListBox1.Size = new Size(309, 203);
            keyValueListBox1.TabIndex = 5;
            keyValueListBox1.TopMargin = 0;
            // 
            // nullDateOnlyPicker1
            // 
            nullDateOnlyPicker1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nullDateOnlyPicker1.Location = new Point(106, 192);
            nullDateOnlyPicker1.MinDate = new DateOnly(1901, 1, 1);
            nullDateOnlyPicker1.Name = "nullDateOnlyPicker1";
            nullDateOnlyPicker1.Size = new Size(251, 23);
            nullDateOnlyPicker1.TabIndex = 6;
            // 
            // concurrentBar1
            // 
            concurrentBar1.BarBackground = Color.Empty;
            concurrentBar1.BarColor = Color.CornflowerBlue;
            concurrentBar1.BorderColor = Color.Gray;
            concurrentBar1.IsMarquee = true;
            concurrentBar1.Location = new Point(68, 298);
            concurrentBar1.Name = "concurrentBar1";
            concurrentBar1.Size = new Size(628, 26);
            concurrentBar1.TabIndex = 7;
            concurrentBar1.Text = "concurrentBar1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 192);
            ClientSize = new Size(800, 450);
            Name = "Form2";
            Text = "Form2";
            MainPanel.ResumeLayout(false);
            DownPanel.ResumeLayout(false);
            DownPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Rop.Winforms9.KeyValueListComboBox.KeyValueLabel keyValueLabel1;
        private Rop.Winforms9.KeyValueListComboBox.KeyValueListBox keyValueListBox1;
        private Rop.Winforms9.KeyValueListComboBox.KeyValueComboBox keyValueComboBox1;
        private Rop.Winforms9.Controls.NullDateOnlyPicker nullDateOnlyPicker1;
        private Rop.Winforms9.Controls.ConcurrentBar concurrentBar1;
    }
}