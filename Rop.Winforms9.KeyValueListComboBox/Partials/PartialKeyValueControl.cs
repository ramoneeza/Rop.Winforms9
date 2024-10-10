using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.GraphicsEx.Colors;

namespace Rop.Winforms9.KeyValueListComboBox;

[DesignerCategory("Code")]
[DummyPartial]
internal partial class PartialKeyValueControl:Control, IKeyValueControl
{
    private int _keyWidth = 75;
    [Description("Key Width")]
    [DefaultValue(75)]
    public int KeyWidth
    {
        get => _keyWidth;
        set
        {
            _keyWidth = value;
            Invalidate();
        }
    }

    private int _centerMargin;

    [Description("Margin between Key and Value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CenterMargin
    {
        get => _centerMargin;
        set
        {
            _centerMargin = value;
            Invalidate();
        }
    }

    private int _topMargin;

    [Description("Top Margin")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int TopMargin
    {
        get => _topMargin;
        set
        {
            _topMargin = value;
            Invalidate();
        }
    }

    private int _leftIconSpaces;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int LeftIconSpaces
    {
        get => _leftIconSpaces;
        set
        {
            _leftIconSpaces = value;
            Invalidate();
        }
    }

    private int _rightIconSpaces;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int RightIconSpaces
    {
        get => _rightIconSpaces;
        set
        {
            _rightIconSpaces = value;
            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ColorSet RowStyleColorSet { get; set; } = ColorSet.Default;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ColorSet RowStyleAltColorSet { get; set; }=ColorSet.DefaultAlt;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ColorSet RowStyleSelectedColorSet { get; set; }=ColorSet.DefaultSel;
    public void SetColorPalette(Color bgcolor, Color forecolor, float contrastkey = 0.25f, float contrastalt = 0.25f)
    {

        var bgcoloralt = bgcolor.AddL(contrastalt);
        RowStyleColorSet = new ColorSet(bgcolor, forecolor, bgcolor.AddL(-contrastkey));
        RowStyleAltColorSet = new ColorSet(bgcoloralt, forecolor, bgcoloralt.AddL(-contrastkey));
    }
    public void SetSelectedColorPalette(Color bgcolor, Color forecolor, float contrastkey = 0.25f)
    {
        RowStyleSelectedColorSet = new ColorSet(bgcolor, forecolor, bgcolor.AddL(-contrastkey));
    }
    [ExcludeThis]
    public object? GetItem(int item) => null;
}