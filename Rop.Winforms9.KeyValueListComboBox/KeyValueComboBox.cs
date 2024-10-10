using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.KeyValueListComboBox
{
    [IncludeFrom(typeof(PartialKeyValueItems))]
    [IncludeFrom(typeof(PartialKeyValueControlDraw))]
    public partial class KeyValueComboBox : CompatibleComboBox, IKeyValueItems, IKeyValueControlDraw
    {
        public new void RefreshItem(int index)
        {
            var a = SelectedIndex;
            base.RefreshItem(index);
            SelectedIndex = a;
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
        public new event DrawItemEventHandler DrawItem { add { throw new NotSupportedException(); } remove { } }
        // Quitar Propiedades no browsables
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once ValueParameterNotUsed
        public new ComboBoxStyle DropDownStyle { get => base.DropDownStyle; set { } }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        // ReSharper disable once ValueParameterNotUsed
        public new DrawMode DrawMode { get => base.DrawMode; set => base.DrawMode = DrawMode.OwnerDrawFixed; }
        
        public KeyValueComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            RowStyleSelectedColorSet=ColorSet.DefaultSel;
            RowStyleColorSet=ColorSet.Default;
            RowStyleAltColorSet= ColorSet.DefaultAlt;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            OnDrawItem2(e);
        }
        public object? GetItem(int item)=>(item<0||item>=Items.Count)?null:Items[item];

    }
    

}