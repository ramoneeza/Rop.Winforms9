using System.ComponentModel;
using Rop.Helper;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.KeyValueListComboBox;

[DesignerCategory("Code")]
[IncludeFrom(typeof(PartialKeyValueControlDraw))]
public partial class KeyValueListBox : CompatibleListBox,IKeyValueItems,IKeyValueControlDraw
{
    public event EventHandler<IconClickEventArgs>? LeftIconClick;
    public event EventHandler<IconClickEventArgs>? RightIconClick;
       
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event DrawItemEventHandler? DrawItem
    {
        add { throw new NotSupportedException(); }
#pragma warning disable RECS0029 // Warns about property or indexer setters and event adders or removers that do not use the value parameter
        // ReSharper disable once ValueParameterNotUsed
        remove { }
#pragma warning restore RECS0029 // Warns about property or indexer setters and event adders or removers that do not use the value parameter
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DrawMode DrawMode
    {
        get => base.DrawMode;
        set => throw new NotSupportedException();
    }


    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TopVisibleKey
    {
        get
        {
            var i = TopIndex;
            if (i >= Items.Count) return "";
            if (i < 0) return "";
            var k = Items[i] as IKeyValue;
            return k?.GetKey() ?? "";
        }
        set
        {
            if (value == "")
            {
                TopIndex = 0;
                return;
            }

            var i = _FindKeyIndex(value);
            if (i < 0)
            {
                TopIndex = 0;
                return;
            }

            TopIndex = i;

            // Local fn
            // ReSharper disable once InconsistentNaming
            int _FindKeyIndex(string s)
            {
                for (var f = 0; f < Items.Count; f++)
                {
                    if (Items[f] is IKeyValue kv && kv.GetKey() == s) return f;
                }

                return -1;
            }
        }
    }

    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        if (Parent != null) Parent.MouseWheel += Parent_MouseWheel;
    }

    private void Parent_MouseWheel(object? sender, MouseEventArgs e)
    {
        OnMouseWheel(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        var d=-e.Delta;
        if (d<-100) d = -100;
        if (d>100) d = 100;
        TopIndex += d;
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);
        if (e.Handled) return;
        if (char.IsLetterOrDigit(e.KeyChar))
        {
            var p = Items.OfType<IKeyValue>().FirstOrDefault(i =>
                i.GetValue().StartsWith(e.KeyChar.ToString(), StringComparison.OrdinalIgnoreCase));
            if (p == null) return;
            TopVisibleKey = p.GetKey();
            e.Handled = true;
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override int ItemHeight
    {
        get => base.ItemHeight;
        set
        {
            if (value < DesiredHeight) value = DesiredHeight;
            base.ItemHeight = value;
        }
    }

    public KeyValueListBox()
    {
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        this.UpdateStyles();
        // ReSharper disable once VirtualMemberCallInConstructor
        BackColor = SystemColors.ControlDark;
        base.DrawMode = DrawMode.OwnerDrawFixed;
        IntegralHeight = false;
        // ReSharper disable once VirtualMemberCallInConstructor
        base.ItemHeight = DesiredHeight;
        RowStyleSelectedColorSet=ColorSet.DefaultSel;
        RowStyleColorSet=ColorSet.Default;
        RowStyleAltColorSet= ColorSet.DefaultAlt;
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        var i = IndexFromPoint(e.Location);
        var vb=DefBoundsValue();
        if (i >= 0 && i < Items.Count)
        {
            var item = Items[i];
            if (item is null) return;
            for(var x=0; x<LeftIconSpaces; x++)
            {
                var r = DefLeftBoundsIcon(x);
                if (r.X<= e.Location.X && e.Location.X <= r.Right)
                {
                    OnLeftIconClick(new IconClickEventArgs(x,i,item,e.Button));
                    break;
                }
            }
            for(var x2=0; x2<RightIconSpaces; x2++)
            {
                var r = DefRightBoundsIcon(x2);
                if (r.X<= e.Location.X && e.Location.X <= r.Right)
                {
                    OnRightIconClick(new IconClickEventArgs(x2,i,item,e.Button));
                    break;
                }
            }
        }
    }

    protected virtual void OnLeftIconClick(IconClickEventArgs args)
    {
        LeftIconClick?.Invoke(this, args);
    }
    protected virtual void OnRightIconClick(IconClickEventArgs args)
    {
        RightIconClick?.Invoke(this, args);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        OnDrawItem2(e);
    }
    public object? GetItem(int item)=>(item<0||item>=Items.Count)?null:Items[item];
    public new string SelectedValue
    {
        get
        {
            if (NoSelectedItem) return "";
            var v = Items[SelectedIndex];
            return v is IKeyValue kv ? kv.GetValue() : v?.ToString() ?? "";
        }
    }
}