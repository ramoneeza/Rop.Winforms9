using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Colors;

public static class ColorHelper
{
    /// <summary>
    /// Extension method to derive a new color from delta HSL values
    /// </summary>
    /// <param name="c">Color</param>
    /// <param name="deltah">Increase Hue values in degrees</param>
    /// <param name="deltas">Scale Saturation</param>
    /// <param name="deltal">Scale Luminosity</param>
    /// <returns></returns>
    public static Color AddHSL(this Color c, float deltah = 0, float deltas = 0, float deltal = 0)
    {
        if (c.IsTOrEmpty()) return c;
        var r = ColorHSL.FromColor(c).WithDeltaHLS(deltah,deltal,deltas);
        return r;
    }

    public static Color AddL(this Color c, float deltal = 0) => AddHSL(c, 0, 0, deltal);
    public static Color AddS(this Color c, float deltas = 0) => AddHSL(c, 0, deltas);
    public static ColorF Contrast(this ColorF c, float cont = 0.25f)
    {
        var nc = new ColorHSL(c).WithContrast(cont);
        return (ColorF)nc;
    }
    public static Color Contrast(this Color c, float cont = 0.25f)=> Contrast((ColorF)c, cont);
    public static ColorF AddHSL(this ColorF c, float deltah = 0, float deltas = 0, float deltal = 0)
    {
        var r = ColorHSL.FromColor(c).WithDeltaHLS(deltah,deltal,deltas);
        return r.ToColorF();
    }

    /// <summary>
    /// Mix two colors.
    /// </summary>
    /// <param name="c">Color 1</param>
    /// <param name="n">Color 2</param>
    /// <param name="a">Proportion</param>
    /// <returns></returns>
    public static Color MixColors(this Color c, Color n, float a = 0.5f)
    {
        var r = (float)(c.R);
        var g = (float)(c.G);
        var b = (float)(c.B);
        var nr = (float)(n.R);
        var ng = (float)(n.G);
        var nb = (float)(n.B);
        var fr = r * a + nr * (1 - a);
        var fg = g * a + ng * (1 - a);
        var fb = b * a + nb * (1 - a);
        return Color.FromArgb((int)Math.Round(fr), (int)Math.Round(fg), (int)Math.Round(fb));
    }
    /// <summary>
    /// Check if a Color is Transparent or Empty
    /// </summary>
    /// <param name="c">Color</param>
    /// <returns>True if is Transparent or Empty</returns>
    public static bool IsTOrEmpty(this Color c) => ((c == Color.Transparent) || (c == Color.Empty));
    /// <summary>
    /// Returns a default color if is Transparent or Empty
    /// </summary>
    /// <param name="c">Color to check</param>
    /// <param name="def">Default value if Transparent or Empty</param>
    /// <returns></returns>
    public static Color IfTOrEmpty(this Color c, Color def) => c.IsTOrEmpty() ? def : c;

    [SuppressMessage("ReSharper", "RedundantCast")]
    public static double ColorDistance(Color e1, Color e2)
    {
        long rmean = ((long)e1.R + (long)e2.R) / 2;
        long r = (long)e1.R - (long)e2.R;
        long g = (long)e1.G - (long)e2.G;
        long b = (long)e1.B - (long)e2.B;
        return Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
    }

    public static ColorF Solid(this ColorF a) => a with{A=1};

    public static ColorF PreMult(this ColorF a)
    {
        return ColorF.FromArgb(1, a.R * a.A, a.G * a.A, a.B * a.A);
    }
    public static ColorF Blend(ColorF a, ColorF b, Func<float, float, float> operation, bool premult = true)
    {
        var fb = (premult) ? b.PreMult() : b;
        return ColorF.FromArgb(1, operation(a.R, fb.R), operation(a.G, fb.G), operation(a.B, fb.B));
    }
    public static ColorF BlendMix(ColorF a, ColorF b, Func<float, float, float> operation, float weightb = 1, bool premult = true)
    {
        if (weightb >= 1) return Blend(a, b, operation, premult);
        if (weightb <= 0) return a;
        var fb = Blend(a, b, operation, premult);
        Func<float, float, float> mix = (aa, bb) => aa * (weightb - 1) + bb * weightb;
        return Blend(a, fb, mix, false);
    }
    public static ColorF Blend(ColorF a, ColorF b, Func<float, float, float, float> operation)
    {
        return ColorF.FromArgb(1, operation(a.R, b.R, b.A), operation(a.G, b.G, b.A), operation(a.B, b.B, b.A));
    }

    public static ColorF Multiply(ColorF a, ColorF b, float weightb = 1)
    {
        return BlendMix(a, b, (aa, bb) => aa * bb, weightb);
    }
    public static ColorF Screen(ColorF a, ColorF b, float weightb = 1)
    {
        return BlendMix(a, b, (aa, bb) => 1 - (1 - aa) * (1 - bb), weightb);
    }

    public static float NormalizeEulerAngle(float angle){
        var normalized = angle % 360;
        if(normalized < 0) normalized += 360;
        return normalized;
    }

    public static float InRange01(float v) =>
        v switch
        {
            < 0 => 0,
            > 1 => 1,
            _ => v
        };


    public static ColorF Normal(ColorF a, ColorF b)
    {
        return Blend(a, b, (aa, bb, al) => (1 - al) * aa + al * bb);
    }
    public static ColorF Add(ColorF a, ColorF b, float weightb = 1)
    {
        return BlendMix(a, b, (aa, bb) => InRange01(aa + bb));
    }
    public static ColorF Sub(ColorF a, ColorF b, float weightb = 1)
    {
        return BlendMix(a, b, (aa, bb) => InRange01(aa - bb));
    }
    public static ColorF Mix(ColorF a, ColorF b, float weightb = 1)
    {
        a = a.Solid();
        b = b.Solid();
        return Blend(a, b, (aa, bb) => (1 - weightb) * aa + weightb * bb, false);
    }

    public static ColorHSL Blend(ColorHSL a, ColorHSL b, Func<float, float, float> operationH, Func<float, float, float> operationS, Func<float, float, float> operationL)
    {
        return new ColorHSL(1, operationH(a.H, b.H), operationS(a.S, b.S), operationL(a.L, b.L));
    }
    public static ColorHSL BlendMix(ColorHSL a, ColorHSL b, Func<float, float, float> operationH, Func<float, float, float> operationS, Func<float, float, float> operationL, float weightb = 1)
    {
        if (weightb >= 1) return Blend(a, b, operationH, operationS, operationL);
        if (weightb <= 0) return a;
        var fb = Blend(a, b, operationH, operationS, operationL);
        Func<float, float, float> mix = (aa, bb) => aa * (weightb - 1) + bb * weightb;
        return Blend(a, fb, mix, mix, mix);
    }

    public static ColorHSL Hue(ColorHSL a, ColorHSL b, float weightb = 1)
    {
        return Blend(a, b, (aa, bb) =>NormalizeEulerAngle(aa * (1 - weightb) + bb * weightb), (aa, _) => aa, (aa, _) => aa);
    }
    public static ColorHSL Saturation(ColorHSL a, ColorHSL b, float weightb = 1)
    {
        return Blend(a, b, (aa, _) => aa, (aa, bb) => InRange01(aa * (1 - weightb) + bb * weightb), (aa, _) => aa);
    }
    public static ColorHSL Luminosity(ColorHSL a, ColorHSL b, float weightb = 1)
    {
        return Blend(a, b, (aa, _) => aa, (aa, _) => aa, (aa, bb) => InRange01(aa * (1 - weightb) + bb * weightb));
    }
    public static ColorF Hue(ColorF a, ColorF b, float weightb = 1)
    {
        return Hue(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
    public static ColorF Saturation(ColorF a, ColorF b, float weightb = 1)
    {
        return Saturation(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
    public static ColorF Luminosity(ColorF a, ColorF b, float weightb = 1)
    {
        return Luminosity(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
    public static Color Hue(Color a, Color b, float weightb = 1)
    {
        return Hue(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
    public static Color Saturation(Color a, Color b, float weightb = 1)
    {
        return Saturation(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
    public static Color Luminosity(Color a, Color b, float weightb = 1)
    {
        return Luminosity(ColorHSL.FromColor(a), ColorHSL.FromColor(b), weightb).ToColorF();
    }
}