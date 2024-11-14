using System.ComponentModel;
using Rop.Winforms9.GraphicsEx;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.Controls;

[DesignerCategory("code")]
public class NullDateOnlyPicker:DateTimePicker
{
    private static readonly DateOnly _mindatem = new DateOnly(1901, 1, 1);
    private DateOnly _mindate;
    
    private DateOnly? _value;

    protected DateTime OriginalValue
    {
        get => base.Value;
        set => base.Value = value;
    }

    public NullDateOnlyPicker()
    {
        _mindate=_mindatem;
        OriginalValue = DateTime.Today;
        _value= DateOnly.FromDateTime(OriginalValue);
    }
    protected bool NoReenterChanges { get; set; }
    protected override void OnValueChanged(EventArgs eventargs)
    {
        if (NoReenterChanges) return;
        if (_trackchanges)
        {
            Value = DateOnly.FromDateTime(base.Value);
        }
        base.OnValueChanged(eventargs);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DateOnly? Value
    {
        get => _value;
        set
        {
            if (_value==value) return;
            _value = value;
            NoReenterChanges= true;
            OriginalValue = value?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
            NoReenterChanges = false;
            OnValueChanged(EventArgs.Empty);
        }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new DateOnly MinDate
    {
        get=> _mindate;
        set
        {
            if (value < _mindatem) value = _mindatem;
            if (value==_mindate) return;
            _mindate = value;
            base.MinDate = _mindate.ToDateTime(TimeOnly.MinValue);
        }
    }

    private bool _trackchanges;
    protected override void OnDropDown(EventArgs eventargs)
    {
        _trackchanges= true;
        base.OnDropDown(eventargs);
    }

    protected override void OnCloseUp(EventArgs eventargs)
    {
        _trackchanges= false;
        //Value = DateOnly.FromDateTime(base.Value);
        base.OnCloseUp(eventargs);
        OnValueChanged(eventargs);
        Invalidate();
    }

    public bool IsNull() => Value is null;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => IsNull()?"":(Value?.ToShortDateString()??"");
        set
        {
            if (string.IsNullOrEmpty(value)) Value = null;
            else
                if (DateOnly.TryParse(value, out DateOnly resultado)) 
                    Value = resultado;
        }
    }
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        switch (m.Msg())
        {
            case Wm.Paint:
                if (IsNull()) 
                    _nullPaint();
                else 
                    _xPaint();
                break;
            case Wm.Lbuttondown:
                if (!IsNull()) 
                    _checkDoNull(m.LParamToPoint());
                break;
        }
    }
    private RectangleF _getXPos()
    {
        var r = new RectangleF(Width - 34 - 16, 1, 16, Height - 2);
        return r;
    }

    private void _nullPaint()
    {
        using var g = CreateGraphics();
        var b = g.VisibleClipBounds;
        var r = new RectangleF(b.X + 1, b.Y + 1, b.Width - 34, b.Height - 2);
        g.FillRectangle(Brushes.White, r);
        g.DrawString("<Null>", Font, Brushes.Red, r);
    }

    private void _xPaint()
    {
        using var g = CreateGraphics();
        var r = _getXPos();
        g.FillRectangle(Brushes.LightGray, r);
        g.DrawCenterMiddleString("X", Font, Brushes.Red, Rectangle.Round(r));
    }

    private void _checkDoNull(Point p)
    {
        var r = _getXPos();
        if (r.Contains(p)) 
            Value = null;
    }
    
}