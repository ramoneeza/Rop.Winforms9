using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Helper;
using Rop.Winforms9.Basic;

namespace WinForms9.Test
{
    public partial class Form2 : FormUpDownPanel
    {
        public Form2()
        {
            InitializeComponent();
            keyValueLabel1.Value =new FooKeyValue("Hola","Saludo");
            keyValueComboBox1.Items.AddRange(new FooKeyValue[]
            {
                new ("hola","Saludo"),
                new ("adios","Despedida"),
                new ("buenos dias","Saludo"),
                new ("buenas noches","Despedida"),
            });
            keyValueListBox1.Items.AddRange(new FooKeyValue[]
            {
                new ("hola","Saludo"),
                new ("adios","Despedida"),
                new ("buenos dias","Saludo"),
                new ("buenas noches","Despedida"),
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var dlg = new FormDlg();
            dlg.ShowDialog();
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
    }
}
