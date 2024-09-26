using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Colors;

/// <summary>
/// Color as Alpha Hue Saturation and Luminosity values
/// </summary>
// ReSharper disable once InconsistentNaming
public record struct ColorHSL
{
    private float _a;

    /// <summary>
    /// Alpha channel
    /// </summary>
    public float A
    {
        readonly get => _a;
        init => _a =ColorHelper.InRange01(value);
    }

    private float _h;

    /// <summary>
    /// Hue
    /// </summary>
    public float H
    {
        readonly get => _h;
        init => _h =ColorHelper.NormalizeEulerAngle(value);
    }

    private float _s;

    /// <summary>
    /// Saturarion
    /// </summary>
    public float S
    {
        readonly get => _s;
        init => _s =ColorHelper.InRange01(value);
    }

    private float _l;

    /// <summary>
    /// Luminosity
    /// </summary>
    public float L
    {
        readonly get => _l;
        init => _l = ColorHelper.InRange01(value);
    }
    /// <summary>
    /// HSL from Color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static ColorHSL FromColorF(ColorF color)
    {
        var r = color.R;
        var g = color.G;
        var b = color.B;
        var hsla = color.A;
        var max = Math.Max(Math.Max(r, g), b);
        var min = Math.Min(Math.Min(r, g), b);
        var delta = max - min;
        var Lx2 = max + min;
        var hslL = Lx2 / 2.0f; // Luminosity
        if (hslL <= 0) return new ColorHSL(hsla,0,0,0);
        var hslS = delta;
        if (hslS <= 0) return new ColorHSL(hsla,hslL,0,0);
        hslS /= (hslL <= 0.5f) ? Lx2 : (2.0f - Lx2);
        var r2 = (max - r) / delta;
        var g2 = (max - g) / delta;
        var b2 = (max - b) / delta;
        var hslH = 0f;
        if (FloatEqual(r,max)) hslH = (FloatEqual(g,min)) ? 5.0f + b2 : 1.0f - g2;
        else
        if (FloatEqual(g,max)) hslH = (FloatEqual(b,min)) ? 1.0f + r2 : 3.0f - b2;
        else
            hslH = (FloatEqual(r,min)) ? 3.0f + g2 : 5.0f - r2;
        hslH *= 60.0f;
        return new ColorHSL(hsla, hslH, hslS, hslL);
        
        bool FloatEqual(float x, float y)
        {
            return Math.Abs(x - y) < 0.00001;
        }
    }
    public static ColorHSL FromColor(Color color) => FromColorF((ColorF)color);

    public static ColorHSL FromHSL(float h, float s, float l) => FromAHSL(1, h, s, l);

    public static ColorHSL FromAHSL(float a, float h, float s, float l) => new(a,h,s,l);

    /// <summary>
    /// Convert to ColorF
    /// </summary>
    /// <param name="chsl">HSL Color</param>
    /// <returns></returns>
    public static ColorF ToColorF(ColorHSL chsl)
    {
        var r = chsl.L;
        var g = chsl.L;
        var b = chsl.L;
        var a = chsl.A;
        float v = (chsl.L <= 0.5) ? (chsl.L * (1.0f + chsl.S)) : (chsl.L + chsl.S - chsl.L * chsl.S);
        if (v > 0)
        {
            var m = 2 * chsl.L - v;
            var sv = (v - m) / v;
            var sextant = (int)(chsl.H / 60.0f);
            var fract = (chsl.H / 60.0f) - sextant;
            var vsf = v * sv * fract;
            var mid1 = m + vsf;
            var mid2 = v - vsf;
            switch (sextant)
            {
                case 0: r = v; g = mid1; b = m; break;
                case 1: r = mid2; g = v; b = m; break;
                case 2: r = m; g = v; b = mid1; break;
                case 3: r = m; g = mid2; b = v; break;
                case 4: r = mid1; g = m; b = v; break;
                case 5: r = v; g = m; b = mid2; break;
            }

        }
        return ColorF.FromArgb(a, r, g, b);
    }
    public static Color ToColor(ColorHSL color) => ToColorF(color);

    public ColorHSL WithDeltaH(float h) => this with { H = H + h };

    public ColorHSL WithDeltaL(float l) => this with { L = L + l };

    public ColorHSL WithDeltaS(float s) => this with { S = S + s };

    public ColorHSL WithDeltaHLS(float h, float l, float s) => this with { H = H + h, L = L + l, S = S + s };

    public ColorHSL WithContrast(float c = 0.25f) => L >= 0.5 ? this.WithDeltaL(c) : this.WithDeltaL(-c);
    public Color ToColor() => ToColor(this);
    public ColorF ToColorF() => ToColorF(this);
    public static implicit operator Color(ColorHSL c) => c.ToColor();
    /// <summary>
    /// Implicit conversion to ColorF
    /// </summary>
    /// <param name="c">Color</param>
    public static explicit operator ColorHSL(Color c) => FromColor(c);

    /// <summary>
    /// Create HSL from values
    /// </summary>
    /// <param name="a">Alpha</param>
    /// <param name="h">Hue</param>
    /// <param name="s">Saturation</param>
    /// <param name="l">Luminosity</param>
    public ColorHSL(float a, float h, float s, float l)
    {
        H = h;
        S = s;
        L = l;
        A = a;
    }
    /// <summary>
    /// Create HSL from Color
    /// </summary>
    /// <param name="color">Color</param>
    public ColorHSL(ColorF color) : this(FromColor(color))
    {
    }
    public ColorHSL(Color color) : this(FromColor(color))
    {
    }

    /// <summary>
    /// Create HSL from HSL
    /// </summary>
    /// <param name="hsl">Color as HSL</param>
    /// <param name="deltah">Increase values in degrees</param>
    /// <param name="deltas">Scale Saturation</param>
    /// <param name="deltal">Scale Luminosity</param>
    public ColorHSL(ColorHSL hsl) : this(hsl.A, hsl.H, hsl.S, hsl.L)
    {
    }

    /// <summary>
    /// Conversion to ColorHSL
    /// </summary>
    /// <param name="c">ColorF</param>
    public static explicit operator ColorHSL(ColorF c) => new(c);
    /// <summary>
    /// Conversion to ColorF
    /// </summary>
    /// <param name="c">Color</param>
    public static explicit operator ColorF(ColorHSL c) => ToColorF(c);

}