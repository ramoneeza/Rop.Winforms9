using System.ComponentModel;
using System.Drawing;

namespace Rop.Winforms9.FontsEx;

/// <summary>
///     A struct that represents a rectangle whit an ascent, descent and baseline.
/// </summary>
public record struct FontRectangleF(float X, float Y, float Width, float Height, float Ascent)
{
    /// <summary>
    ///     A struct that represents an empty FontRectangleF.
    /// </summary>
    public static readonly FontRectangleF Empty = default;

    /// <summary>
    ///     Initializes a new instance of the FontRectangleF struct.
    /// </summary>
    /// <param name="location">The coordinates for the starting location of the rectangle.</param>
    /// <param name="size">The FontSizeF of the rectangle.</param>
    public FontRectangleF(PointF location, FontSizeF size) : this(location.X, location.Y, size.Width, size.Height,
        size.Ascent)
    {
    }

    /// <summary>
    ///     Gets the rectangle corresponding to this FontRectangleF's ascent.
    /// </summary>
    [Browsable(false)]
    public readonly RectangleF AscentBounds => new(X, Y, Width, Ascent);

    /// <summary>
    ///     Gets or sets the baseline value.
    /// </summary>
    [Browsable(false)]
    public float BaseLine
    {
        readonly get => Y + Ascent;
        set => Y = value - Ascent;
    }

    /// <summary>
    ///     Gets or sets the location of the baseline.
    /// </summary>
    [Browsable(false)]
    public PointF BaseLineLocation
    {
        readonly get => new(X, BaseLine);
        set
        {
            X = value.X;
            BaseLine = value.Y;
        }
    }

    /// <summary>
    ///     Gets or sets the location of the bottom of the rectangle.
    /// </summary>
    [Browsable(false)]
    public PointF BotomLocation
    {
        readonly get => new(X, Y + Height);
        set
        {
            X = value.X;
            Y = value.Y - Height;
        }
    }

    /// <summary>
    ///     Gets or sets the bottom value.
    /// </summary>
    [Browsable(false)]
    public float Bottom
    {
        readonly get => Y + Height;
        set => Y = value - Height;
    }

    /// <summary>
    ///     Gets or sets the rectangle value containing both the ascent and descent values.
    /// </summary>
    [Browsable(false)]
    public RectangleF Bounds
    {
        readonly get => new(X, Y, Width, Height);
        set
        {
            X = value.X;
            Y = value.Y;
            Width = value.Width;
            Height = value.Height;
        }
    }

    /// <summary>
    ///     Gets or sets the X value of the center point.
    /// </summary>
    [Browsable(false)]
    public float CenterX
    {
        readonly get => X + Width / 2;
        set => X = value - Width / 2;
    }

    /// <summary>
    ///     Gets or sets the Y value of the center point.
    /// </summary>
    [Browsable(false)]
    public float CenterY
    {
        readonly get => Y + Height / 2;
        set => Y = value - Height / 2;
    }

    /// <summary>
    ///     Gets or sets the descent value.
    /// </summary>
    [Browsable(false)]
    public float Descent
    {
        readonly get => Height - Ascent;
        set => Height = Ascent + value;
    }

    /// <summary>
    ///     Gets the rectangle corresponding to this FontRectangleF's descent.
    /// </summary>
    [Browsable(false)]
    public readonly RectangleF DescentBounds => new(X, BaseLine, Width, Descent);

    /// <summary>
    ///     Gets a value indicating whether this FontRectangleF is empty.
    /// </summary>
    [Browsable(false)]
    public readonly bool IsEmpty => Equals(Empty);

    /// <summary>
    ///     Gets the left value.
    /// </summary>
    [Browsable(false)]
    public float Left
    {
        readonly get => X;
        set => X = value;
    }

    /// <summary>
    ///     Gets or sets the right value.
    /// </summary>
    [Browsable(false)]
    public float Right
    {
        readonly get => X + Width;
        set => X = value - Width;
    }

    /// <summary>
    ///     Gets or sets the FontSizeF of this FontRectangleF.
    /// </summary>
    [Browsable(false)]
    public FontSizeF Size
    {
        readonly get => new(Width, Height, Ascent);
        set
        {
            Width = value.Width;
            Height = value.Height;
            Ascent = value.Ascent;
        }
    }

    /// <summary>
    ///     Gets the top value.
    /// </summary>
    [Browsable(false)]
    public float Top
    {
        readonly get => Y;
        set => Y = value;
    }

    /// <summary>
    ///     Gets the topleft location.
    /// </summary>
    [Browsable(false)]
    public PointF TopLeftLocation
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    /// <summary>
    ///     Gets the topright location.
    /// </summary>
    [Browsable(false)]
    public PointF TopRightLocation
    {
        readonly get => new(Right, Top);
        set
        {
            Right = value.X;
            Y = value.Y;
        }
    }

    /// <summary>
    ///     Implicitly converts a FontRectangleF object to a RectangleF object.
    /// </summary>
    /// <param name="r">The FontRectangleF object to convert.</param>
    /// <returns>A RectangleF object that is equivalent to the FontRectangleF passed.</returns>
    public static explicit operator RectangleF(FontRectangleF r) => new(r.X, r.Y, r.Width, r.Height);

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct using the specified base line.
    /// </summary>
    /// <param name="baseline">The baseline of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF FromBaseLine(PointF baseline, FontSizeF size) => FromBaseLine(baseline.X, baseline.Y, size.Width, size.Height, size.Ascent);

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct using the specified coordinates and size.
    /// </summary>
    /// <param name="x">The x component of the starting coordinates for the rectangle.</param>
    /// <param name="baseline">The y component of the base line coordinates for the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="ascent">The ascent of the rectangle.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF FromBaseLine(float x, float baseline, float width, float height, float ascent) => new(x, baseline - ascent, width, height, ascent);

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct using the specified coordinates, ascent and descent values.
    /// </summary>
    /// <param name="x">The x component of the starting coordinates for the rectangle.</param>
    /// <param name="baseline">The y component of the base line coordinates for the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="ascent">The ascent of the rectangle.</param>
    /// <param name="descent">The descent of the rectangle.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF FromBaseLineAscentDescent(float x, float baseline, float width, float ascent,
        float descent) =>
        FromBaseLine(x, baseline, width, ascent + descent, ascent);

    public static FontRectangleF UnionByAscent(params FontRectangleF[] rectangles)
    {
        var rbaseline = rectangles.MaxBy(a => a.Ascent).BaseLine;
        return UnionByBaseLine(rectangles, rbaseline);
    }

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct using the specified FontRectangleFs and setting the baseline
    ///     Value accordingly.
    /// </summary>
    /// <param name="rectangles">The FontRectangleFs to include in the union.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF UnionByAscent(IEnumerable<FontRectangleF> rectangles) => UnionByAscent(rectangles.ToArray());

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct, calling the BaseLine method to set the baseline value.
    /// </summary>
    /// <param name="baseline">The baseline value to use.</param>
    /// <param name="rectangles">FontRectangleFs to include.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF UnionByBaseLine(float baseline,params FontRectangleF[] rectangles)
    {
        return UnionByBaseLine(rectangles,baseline);
    }

    /// <summary>
    ///     Creates a new instance of the FontRectangleF struct, calling the BaseLine method to set the baseline value.
    /// </summary>
    /// <param name="rectangles">The FontRectangleFs to include in the union.</param>
    /// <param name="baseline">The baseline value to use.</param>
    /// <returns>A new instance of the FontRectangleF struct.</returns>
    public static FontRectangleF UnionByBaseLine(IEnumerable<FontRectangleF> rectangles, float baseline)
    {
        var all = rectangles.Select(r => r with { BaseLine = baseline }).ToArray();
        var x = all.Min(r => r.X);
        var right = all.Max(r => r.Right);
        var y = all.Min(r => r.Y);
        var bottom = all.Max(r => r.Bottom);
        return FromBaseLine(x, baseline, right - x, bottom - y, baseline - y);
    }

    /// <summary>
    ///     Determines if the specified point is contained within this FontRectangleF.
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <returns>True if the specified point is contained, otherwise false</returns>
    public readonly bool Contains(float x, float y)
    {
        return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
    }

    /// <summary>
    ///     Determines if the specified point is contained within this FontRectangleF.
    /// </summary>
    /// <param name="pt">Point to compare</param>
    /// <returns>True if the specidied point is contained, otherwise false</returns>
    public readonly bool Contains(PointF pt)
    {
        return Contains(pt.X, pt.Y);
    }

    /// <summary>
    ///     Determines if the specified FontRectangleF is contained within this FontRectangleF.
    /// </summary>
    /// <param name="rect">FontRectangleF to compare</param>
    /// <returns>True if the specified FontRectangleF is contained, otherwise false</returns>
    public readonly bool Contains(RectangleF rect)
    {
        // Calcular directamente
        return X <= rect.X && Y <= rect.Y && X + Width >= rect.X + rect.Width && Y + Height >= rect.Y + rect.Height;
    }

    /// <summary>
    ///     Offsets the FontRectangleF by the specified amount.
    /// </summary>
    /// <param name="pos">Amount to Offset</param>
    public void Offset(PointF pos)
    {
        Offset(pos.X, pos.Y);
    }

    /// <summary>
    ///     Offsets the FontRectangleF by the specified amount.
    /// </summary>
    /// <param name="x">Offset X</param>
    /// <param name="y">Offset Y</param>
    public void Offset(float x, float y)
    {
        X += x;
        Y += y;
    }

    /// <summary>
    ///     Converts the FontRectangleF to a RectangleF.
    /// </summary>
    /// <returns>A new RectangleF instance.</returns>
    public RectangleF ToRectangleF()
    {
        return new RectangleF(X, Y, Width, Height);
    }

    /// <summary>
    ///     Returns a FontRectangleF that is offset by the specified amount.
    /// </summary>
    /// <param name="dx">Delta x value</param>
    /// <param name="dy">Delta y value</param>
    /// <returns>FontRectengleF with offset</returns>
    public FontRectangleF WithOffset(float dx, float dy)
    {
        var a = this;
        a.X += dx;
        a.Y += dy;
        return a;
    }

    public FontRectangleF WithOffset(PointF delta)
    {
        return WithOffset(delta.X, delta.Y);
    }
}