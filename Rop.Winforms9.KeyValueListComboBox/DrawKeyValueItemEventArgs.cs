using Rop.Helper;
using Rop.Winforms9.GraphicsEx;
using Rop.Winforms9.GraphicsEx.Geom;
using Rop.Winforms9.ListComboBox;
using Rop.Winforms9.Helper;
namespace Rop.Winforms9.KeyValueListComboBox;

public class DrawKeyValueItemEventArgs:IKeyValueColors
{
    public ColorSet RowStyleColorSet { get; set; }

    public ColorSet RowStyleAltColorSet { get; set; }

    public ColorSet RowStyleSelectedColorSet { get; set; }

    public Color KeyBackColorFinal => this.KeyBackColorFinal(Selected, Index,ControlBackground);
    public Color KeyForeColorFinal => this.ForeColorFinal(Selected,Index,ControlForeground);
    public Color ValueBackColorFinal => this.ValueBackColorFinal(Selected, Index, ControlBackground);
    public Color ValueForeColorFinal=> this.ForeColorFinal(Selected, Index, ControlForeground);
    public bool Selected { get; private set; }
    public bool Focused { get; private set; }
    public bool NoFocusRect { get; private set; }
    public bool IsString { get; private set; }
    public Font Font { get; set; }
    public Font? StringFont { get; set; }
    public Color ControlBackground { get; }
    public Color ControlForeground { get; }
    public Graphics Graphics { get; private set; }
    public int Index { get; private set; }
    public int CenterMargin { get; set; }
    public int TopMargin { get; set; }
    public int LeftIconSpaces { get; set; }
    public int RightIconSpaces { get; set; }
    public IKeyValue? Item=>RawItem as IKeyValue;
    public object? RawItem { get; set; }
    public Rectangle Bounds { get; private set; }
    public Rectangle KeyBounds { get; set; }
    public Rectangle ValueBounds { get; set; }
    public string DisplayKey { get; set; } = "";
    public string DisplayValue { get; set; } = "";
    public int KeyWidth { get; private set; }
    public bool Handled { get; set; }
    public bool HandledKBg { get; set; }
    public bool HandledVBg { get; set; }
    public bool HandledFocus { get; set; }

    public DrawKeyValueItemEventArgs(IKeyValueControl cb, Graphics graphics, int i, Rectangle rect,
        DrawItemState state)
    {
        var control = cb as Control;
        RowStyleColorSet = cb.RowStyleColorSet;
        RowStyleAltColorSet = cb.RowStyleAltColorSet;
        RowStyleSelectedColorSet = cb.RowStyleSelectedColorSet;
        ControlBackground = control?.BackColor ?? SystemColors.Window;
        ControlForeground = control?.ForeColor ?? SystemColors.WindowText;
        Font = control?.Font??Control.DefaultFont;

        Selected = (state & DrawItemState.Selected) == DrawItemState.Selected;
        Focused = (state & DrawItemState.Focus) == DrawItemState.Focus;
        NoFocusRect = (state & DrawItemState.NoFocusRect) == DrawItemState.NoFocusRect;
        Graphics = graphics;
        Index = i;
        IsString = false;
        Handled = false;
        HandledKBg = false;
        HandledVBg = false;
        HandledFocus = false;
        // ReSharper disable once SuspiciousTypeConversion.Global
        //if (cb is not ListControl listcontrol || (i < 0) || (i >= listcontrol.GetItemsCount()))
        //{
        //    RawItem = null;
        //    DisplayKey = string.Empty;
        //    DisplayValue = string.Empty;
        //}
        //else
        //{
            var item =  cb.GetItem(i);
            // comprobar si estamos en diseño de controles
            
            
            RawItem = item;
            switch (item)
            {
                case IKeyValue ikv:
                    DisplayKey = ikv.GetKey();
                    DisplayValue = ikv.GetValue();
                    IsString = false;
                    break;
                case string s:
                    DisplayValue = s ?? "";
                    DisplayKey = "";
                    IsString = true;
                    break;
                default:
                    DisplayKey = "";
                    DisplayValue = item?.ToString() ?? "";
                    break;
            }
            if (item is null && (control?.IsRealDesignerMode()??false))
            {
                DisplayKey = "[Key]";
                DisplayValue = "[Value]";
            }
        //}
        KeyWidth = cb.KeyWidth;
        TopMargin=cb.TopMargin;
        RightIconSpaces= cb.RightIconSpaces;
        LeftIconSpaces=cb.LeftIconSpaces;
        Bounds = rect;
        KeyBounds = new Rectangle(Bounds.X, Bounds.Y, KeyWidth, Bounds.Height);
        ValueBounds = new Rectangle(Bounds.X + KeyWidth, Bounds.Y, Bounds.Width - KeyWidth, Bounds.Height);
    }

    public void DrawBackground()
    {
        DrawValueBackground();
        DrawKeyBackground();
    }

    public void DrawKeyBackground()
    {
        var brush =new SolidBrush(KeyBackColorFinal);
        this.Graphics.FillRectangle(brush, KeyBounds);
        HandledKBg = true;
        HandledFocus = false;
    }

    public void DrawValueBackground()
    {
        var brush = new SolidBrush(ValueBackColorFinal);
        this.Graphics.FillRectangle(brush, ValueBounds);
        HandledVBg = true;
        HandledFocus = false;
    }

    public void DrawFocusRectangle()
    {
        if (Focused && !NoFocusRect)
        {
            ControlPaint.DrawFocusRectangle(this.Graphics, this.Bounds, ValueForeColorFinal, ValueBackColorFinal);
        }

        HandledFocus = true;
    }
    public Rectangle DefBoundsTextValue()
    {
        var w = ValueBounds.Height;
        var x = CenterMargin + w * LeftIconSpaces;
        var mx=w*RightIconSpaces;
        return ValueBounds.DeltaPos(x, TopMargin, true).DeltaWidth(-mx);
    }
    public Rectangle DefBoundsTextKey()
    {
        return KeyBounds.DeltaWidth(-CenterMargin).DeltaPos(0, TopMargin, true);
    }

    public void DrawKey()
    {
        var br = new SolidBrush(KeyForeColorFinal);
        this.Graphics.DrawRightString(DisplayKey, Font, br,DefBoundsTextKey());
    }
    public void DrawValue()
    {
        var br =new SolidBrush(ValueForeColorFinal);
        var font = Font;
        if (IsString) font = StringFont??new Font(Font, FontStyle.Bold);
        Graphics.DrawString(DisplayValue, font, br,DefBoundsTextValue());
    }
    
    public Rectangle DefLeftBoundsIcon(int index)
    {
        var w = ValueBounds.Height;
        var x = ValueBounds.X+CenterMargin + index * (w+2);
        return new Rectangle(x,ValueBounds.Y, w+2, w);
    }
    public Rectangle DefRightBoundsIcon(int index)
    {
        var w = ValueBounds.Height;
        var x = ValueBounds.Right - (index+1) * (w+2);
        return new Rectangle(x,ValueBounds.Y, w+2, w);
    }
    
    
    

    public void SetForeColor(Color forecolor,bool includeselected=true)
    {
        RowStyleColorSet = RowStyleColorSet.WithForeColor(forecolor);
        RowStyleAltColorSet = RowStyleAltColorSet.WithForeColor(forecolor);
        if (includeselected) RowStyleSelectedColorSet = RowStyleSelectedColorSet.WithForeColor(forecolor);
    }
    public void SetSelectedForeColor(Color forecolor)
    {
        RowStyleSelectedColorSet = RowStyleSelectedColorSet.WithForeColor(forecolor);
    }
    public void SetValueBackColor(Color backcolor,Color backcoloralt)
    {
        RowStyleColorSet = RowStyleColorSet.WithBackColor(backcolor);
        RowStyleAltColorSet = RowStyleAltColorSet.WithBackColor(backcoloralt);
    }
    public void SetKeyBackColor(Color backcolor,Color backcoloralt)
    {
        RowStyleColorSet = RowStyleColorSet.WithKeyColor(backcolor);
        RowStyleAltColorSet = RowStyleAltColorSet.WithKeyColor(backcoloralt);
    }
}