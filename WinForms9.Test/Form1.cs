using Rop.Winforms9.Basic;

namespace WinForms9.Test
{
    public partial class Form1 : FormDownPanel
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            concurrentBar1.Start(0, "Hola");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            concurrentBar1.Start(0, "Hola");
        }
    }
}
