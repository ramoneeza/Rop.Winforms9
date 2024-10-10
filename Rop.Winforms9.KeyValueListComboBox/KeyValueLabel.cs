using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rop.IncludeFrom.Annotations;

namespace Rop.Winforms9.KeyValueListComboBox
{
    [DesignerCategory("code")]
    [IncludeFrom(typeof(PartialKeyValueControlDraw))]
    public partial class KeyValueLabel:Control,IKeyValueControlDraw
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ItemHeight { get=>Height; set=>Height=value; }
        private new bool AutoSize => false;
        public KeyValueLabel()
        {
            base.AutoSize = false;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? Value { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            var newe = new DrawItemEventArgs(e.Graphics,Font,this.ClientRectangle,0,DrawItemState.Default);
            OnDrawItem2(newe);
        }
        public object? GetItem(int item) => Value;
    }
}
