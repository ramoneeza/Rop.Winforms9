using System.Drawing;
using System.Runtime.CompilerServices;

namespace Rop.Winforms9.GraphicsEx.Colors;

/// <summary>
/// FloatColor with ARGB values from 0 to 1
/// </summary>
public record struct ColorF
{
    private readonly float _a;

    /// <summary>
    /// Alpha channel
    /// </summary>
    public float A
    {
        readonly get => _a;
        init => _a = ColorHelper.InRange01(value);
    }

    private readonly float _r;

    /// <summary>
    /// Red channel
    /// </summary>
    public float R
    {
        readonly get => _r;
        init => _r = ColorHelper.InRange01(value);
    }

    private readonly float _g;

    /// <summary>
    /// Green channel
    /// </summary>
    public float G
    {
        readonly get => _g;
        init => _g = ColorHelper.InRange01(value);
    }

    private readonly float _b;

    /// <summary>
    /// Blue channel
    /// </summary>
    public float B
    {
        readonly get => _b;
        init => _b = ColorHelper.InRange01(value);
    }

    /// <summary>
    /// ColorF from Color
    /// </summary>
    /// <param name="c">Color</param>
    /// <returns></returns>
    public static ColorF FromColor(Color c) => new ColorF { A =_byteToFloat(c.A), R =_byteToFloat(c.R), G =_byteToFloat(c.G), B =_byteToFloat(c.B)};
    /// <summary>
    /// ColorF from ARGB
    /// </summary>
    /// <param name="a">Alpha</param>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <returns></returns>
    public static ColorF FromArgb(float a, float r, float g, float b) => new ColorF { A = a, R = r, G = g, B = b };
    /// <summary>
    /// Solid Color from RGB
    /// </summary>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <returns></returns>
    public static ColorF FromRgb(float r, float g, float b) => new ColorF { A = 1, R = r, G = g, B = b };
    /// <summary>
    /// To Color
    /// </summary>
    /// <returns>Standard Color</returns>
    public Color ToColor() => Color.FromArgb(_floatToByte(A),_floatToByte(R),_floatToByte(G),_floatToByte(B));

    private static byte _floatToByte(float f)
    {
        var b = (int) Math.Round(f * 255);
        return b switch
        {
            < 0 => 0,
            > 255 => 255,
            _ => (byte)b
        };
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float _byteToFloat(byte b)
    {
        return b / 255f;
    }
    /// <summary>
    /// Implicit conversion to Color
    /// </summary>
    /// <param name="c">ColorF</param>
    public static implicit operator Color(ColorF c) => c.ToColor();
    /// <summary>
    /// Implicit conversion to ColorF
    /// </summary>
    /// <param name="c">Color</param>
    public static explicit operator ColorF(Color c) => FromColor(c);

}