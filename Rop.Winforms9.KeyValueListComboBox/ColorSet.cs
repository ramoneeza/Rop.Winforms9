using System.ComponentModel;
using Rop.Winforms9.GraphicsEx.Colors;
using Rop.Winforms9.KeyValueListComboBox.Converters;

namespace Rop.Winforms9.KeyValueListComboBox
{
    [TypeConverter(typeof(ColorSetConverter))]
    public readonly struct ColorSet:IEquatable<ColorSet>
    {
        private static readonly Color _defKeyColor= Color.FromArgb(25, Color.Black);
        public Color BackColor { get; }=Color.Empty;
        public Color ForeColor { get; }=Color.Empty;
        public Color KeyColor { get; }=Color.Empty;
        [Browsable(false)]
        public Color[] Colors=> new[] { BackColor, ForeColor,KeyColor };
        public ColorSet(Color backColor,Color foreColor,Color keyColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            KeyColor = keyColor;
        }
        public ColorSet(Color[] color):this(color[0],color[1],color[2]){
        }
        public bool IsEmpty() => BackColor.IsEmpty && ForeColor.IsEmpty && KeyColor.IsEmpty;
        public ColorSet IfEmpty(ColorSet other) => IsEmpty() ? other : this;
        public override string ToString()
        {
            var strcolors= string.Join(",", Colors.Select(ColorNames.ColorToString));
            return $"({strcolors})";
        }
        
        // DefaultValues
        public static readonly ColorSet Empty=new ColorSet(Color.Empty,Color.Empty,Color.Empty);
        public static readonly ColorSet Default= new ColorSet(Color.White, Color.Empty,Color.Empty);
        public static readonly ColorSet DefaultAlt = new ColorSet(SystemColors.Control, Color.Empty, Color.Empty);
        public static readonly ColorSet DefaultSel = new ColorSet(SystemColors.Highlight, Color.White, SystemColors.Highlight);
        public bool Equals(ColorSet other)
        {
            return other.Colors.SequenceEqual(Colors);
        }
        public override bool Equals(object? obj)
        {
            return obj is ColorSet other && Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(BackColor, ForeColor,KeyColor);
        }
        public static Color Blend(Color color1, Color color2)
        {
            if (color2.IsTOrEmpty() || color2.A == 0) return color1;
            if (color2.A == 255) return color2;
            var a=color2.A/255.0;
            var r = (int)((color1.R * (1 - a)) + (color2.R * a));
            var g = (int)((color1.G * (1 - a)) + (color2.G * a));
            var b = (int)((color1.B * (1 - a)) + (color2.B * a));
            return Color.FromArgb(r, g, b);
        }
        public Color BackColorFinal(Color subbackcolor)
        {
            return Blend(subbackcolor,BackColor);
        }
        public Color ForeColorFinal(Color forecolor)
        {
            return Blend(forecolor,ForeColor);
        }
        public Color KeyColorFinal(Color backcolor)
        {
            var bc = BackColorFinal(backcolor);
            var kc = KeyColor.IfTOrEmpty(_defKeyColor);
            return Blend(bc,kc);
        }
        public static ColorSet FromString(string str)
        {
            if (string.IsNullOrEmpty(str)) return Empty;
            if (str.StartsWith('(') && str.EndsWith(')')) str=str[1..^1];
            var colors = str.Split(',');
            if (colors.Length != 3) return Empty;
            return new ColorSet(ColorNames.ColorFromString(colors[0]),ColorNames.ColorFromString(colors[1]), ColorNames.ColorFromString(colors[2]));
        }
        public ColorSet WithBackColor(Color color) => new ColorSet(color,ForeColor,KeyColor);
        public ColorSet WithForeColor(Color color) => new ColorSet(BackColor, color, KeyColor);        
        public ColorSet WithKeyColor(Color color) => new ColorSet(BackColor, ForeColor,color);
    }
}