using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.KeyValueListComboBox;

[DesignerCategory("Code")]
[DummyPartial]
[AlsoInclude(typeof(PartialKeyValueControl))]
internal partial class PartialKeyValueControlDraw : Control, IKeyValueControlDraw
{
    public event EventHandler<DrawKeyValueItemEventArgs>? DrawKeyValueItem;
    public event EventHandler<DrawKeyValueItemEventArgs>? DrawKeyItem;
    public event EventHandler<DrawKeyValueItemEventArgs>? DrawValueItem;
    public event EventHandler<DrawKeyValueItemEventArgs>? DrawStringItem;
    public event EventHandler<DrawKeyValueItemEventArgs>? PostDrawKeyItem;
    public event EventHandler<DrawKeyValueItemEventArgs>? PostDrawValueItem;
    public event EventHandler<DrawKeyValueItemIconArgs>? DrawLeftIcon;
    public event EventHandler<DrawKeyValueItemIconArgs>? DrawRightIcon;

    private int _desiredHeight=15;
    [Browsable(false)]
    [DefaultValue(15)]
    public virtual int DesiredHeight
    {
        get => _desiredHeight;
        set
        {
            _desiredHeight = value; 
            Invalidate();
        }
    }
    [ExcludeThis]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ItemHeight { get; set; }
    private Color _borderColor;
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }
    public Rectangle DefBoundsTextKey()
    {
        var keyBounds = DefBoundsKey();
        return DefBoundsTextKey(keyBounds, CenterMargin, TopMargin);
    }

    public static Rectangle DefBoundsTextKey(Rectangle keyBounds, int centerMargin, int topMargin)
    {
        return keyBounds.DeltaWidth(-centerMargin).DeltaPos(0, topMargin, true);
    }

    public void DefDrawKey(DrawKeyValueItemEventArgs e)
    {
        e.DrawKey();
    }
    public Rectangle DefBoundsTextValue(DrawKeyValueItemEventArgs e)
    {
        return e.DefBoundsTextValue();
    }
    public Rectangle DefBounds()
    {
        var kvc = this as Control;
        var height = kvc switch
        {
            ComboBox cb => cb.ItemHeight,
            ListBox lb => lb.ItemHeight,
            IKeyValueControlDraw kvcd => kvcd.DesiredHeight,
            _ => kvc.ClientSize.Height
        };
        return new Rectangle(0, 0, kvc.ClientSize.Width, height);
    }
    public Rectangle DefBoundsValue()
    {
        var bounds = DefBounds();
        var x = KeyWidth;
        return DefBoundsValue(bounds, x);
    }
    public static Rectangle DefBoundsValue(Rectangle bounds, int keyWidth)
    {
        return new Rectangle(keyWidth, 0, bounds.Width - keyWidth, bounds.Height);
    }

    public Rectangle DefBoundsKey()
    {
        var bounds = DefBounds();
        var x = KeyWidth;
        return DefBoundsKey(bounds, x);
    }

    public static Rectangle DefBoundsKey(Rectangle bounds, int keywidth)
    {
        return new Rectangle(0, 0, keywidth, bounds.Height);
    }

    public virtual void OnDrawKeyValueItem(DrawKeyValueItemEventArgs e)
    {
        e.DrawBackground();
        e.DrawFocusRectangle();
        DrawKeyValueItem?.Invoke(this, e);
        if (e.Handled) return;
        if (!e.HandledKBg) e.DrawKeyBackground();
        if (!e.HandledVBg) e.DrawValueBackground();
        if (!e.HandledFocus) e.DrawFocusRectangle();
        OnDrawKeyItem(e);
        OnDrawValueItem(e);
    }

    public virtual void OnDrawKeyItem(DrawKeyValueItemEventArgs e)
    {
        DrawKeyItem?.Invoke(this, e);
        if (!e.Handled && !e.HandledKBg) e.DrawKeyBackground();
        if (!e.Handled) DefDrawKey(e);
        PostDrawKeyItem?.Invoke(this, e);
    }

    public virtual void OnDrawValueItem(DrawKeyValueItemEventArgs e)
    {
        if (e.IsString)
        {
            DrawStringItem?.Invoke(this, e);
            if (e.Handled) return;
        }

        DrawValueItem?.Invoke(this, e);
        if (!e.Handled && !e.HandledVBg) e.DrawValueBackground();
        if (!e.Handled)
        {
            e.DrawValue();
            if (e.LeftIconSpaces != 0)
            {
                for (var i = 0; i < e.LeftIconSpaces; i++)
                {
                    var b = e.DefLeftBoundsIcon(i);
                    OnDrawLeftIcon(e, i);
                }
            }

            if (e.RightIconSpaces != 0)
            {
                for (var i = 0; i < e.RightIconSpaces; i++)
                {
                    var b = e.DefRightBoundsIcon(i);
                    OnDrawRightIcon(e, i);
                }
            }
        }

        PostDrawValueItem?.Invoke(this, e);
    }

    public virtual void OnDrawLeftIcon(DrawKeyValueItemEventArgs e, int i)
    {
        var args = new DrawKeyValueItemIconArgs(e, i, e.DefLeftBoundsIcon(i));
        DrawLeftIcon?.Invoke(this, args);
    }

    public virtual void OnDrawRightIcon(DrawKeyValueItemEventArgs e, int i)
    {
        var args = new DrawKeyValueItemIconArgs(e, i, e.DefRightBoundsIcon(i));
        DrawRightIcon?.Invoke(this, args);
    }
    public virtual void OnDrawItem2(DrawItemEventArgs e)
    {
        var newe = new DrawKeyValueItemEventArgs(this, e.Graphics, e.Index, e.Bounds, e.State);
        OnDrawKeyValueItem(newe);
        if (BorderColor != Color.Black)
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
    }
    [ExcludeThis]
    public virtual void OnDrawItem(DrawItemEventArgs e)
    {
        OnDrawItem2(e);
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        _ajMinHeight();
    }
    private void _ajMinHeight()
    {
        using var g = CreateGraphics();
        var h = (int)g.MeasureString("X", Font).Height;
        DesiredHeight = h + 2;
    }
    public Rectangle DefLeftBoundsIcon(int index)
    {
        var valuebounds = DefBoundsValue();
        return DefLeftBoundsIcon(index, valuebounds, CenterMargin);
    }
    public static Rectangle DefLeftBoundsIcon(int index,Rectangle valuebounds,int centermargin)
    {
        var w = valuebounds.Height;
        var x = valuebounds.X+centermargin + index * (w+2);
        return new Rectangle(x,valuebounds.Y, w+2, w);
    }
    public Rectangle DefRightBoundsIcon(int index)
    {
        var valuebounds = DefBoundsValue();
        return DefRightBoundsIcon(index, valuebounds);
    }
    public static Rectangle DefRightBoundsIcon(int index,Rectangle valuebounds)
    {
        var w = valuebounds.Height;
        var x = valuebounds.Right - (index+1) * (w+2);
        return new Rectangle(x,valuebounds.Y, w+2, w);
    }

    public Rectangle DefBoundsTextValue()
    {
        var valuebounds= DefBoundsValue();
        return DefBoundsTextValue(valuebounds, CenterMargin, TopMargin, LeftIconSpaces, RightIconSpaces);
    }

    public static Rectangle DefBoundsTextValue(Rectangle valuebounds,int centerMargin,int topmargin,int leftIconSpaces,int rightIconSpaces)
    {
        var w =  valuebounds.Height;
        var x = centerMargin + w * leftIconSpaces;
        var mx=w*rightIconSpaces;
        return valuebounds.DeltaPos(x, topmargin, true).DeltaWidth(-mx);
    }

    [ExcludeThis]
    public object? GetItem(int item) => null;
}