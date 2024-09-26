using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

/// <summary>
/// Geometric Helper Extensions
/// </summary>
public static partial class HelperGeom
{

    /// <summary>
    /// Diagonal of a RectangleF
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static float Diagonal(this RectangleF r) => Diagonal(r.Size);

    /// <summary>
    /// Diagonal of a SizeF
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static float Diagonal(this SizeF r) => (float)Math.Sqrt(r.Width * r.Width + r.Height * r.Height);

    /// <summary>
    /// Size BetWeen Two PointF
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static SizeF Diff(PointF p, PointF p2) => new(p2.Sub(p));

    /// <summary>
    /// Distance between two points
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static float Distance(PointF p, PointF p2)
    {
        var s = Diff(p, p2);
        return s.Diagonal();
    }

    /// <summary>
    /// Unity Rectangle Zero Base
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static RectangleF Unity(this RectangleF r) => new(PointF.Empty, Proportion(r));

    /// <summary>
    /// Proportion of a Rectangle (Unity Vector)
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static SizeF Proportion(this RectangleF r) => r.Size.Proportion();

    /// <summary>
    /// Proportion of a Size (Unity Vector)
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static SizeF Proportion(this SizeF r)
    {
        r.Proportion(out var w, out var h, out var d);
        return new SizeF(w, h);
    }
    
    public static void Proportion(this SizeF r,out float w,out float h,out float d)
    {
        d = Diagonal(r);
        if (d == 0) d = 1;
        w = r.Width / d;
        h = r.Height / d;
    }

    /// <summary>
    /// Multiply a Size (Scale)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="mult"></param>
    /// <returns></returns>
    public static SizeF Multiply(this SizeF s, float mult)=>s.Scale(mult);

    /// <summary>
    /// Multiply a Point (Scale)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="mult"></param>
    /// <returns></returns>
    public static PointF Multiply(this PointF s, float mult) => s.Scale(mult);
    /// <summary>
    /// Set Diagonal of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="diagonal"></param>
    /// <returns></returns>
    public static RectangleF SetDiagonal(this RectangleF r, float diagonal)
    {
        var ru = r.Unity();
        return new RectangleF(r.Location, ru.Size.Multiply(diagonal));
    }
    /// <summary>
    /// Set Diagonal of a Size
    /// </summary>
    /// <param name="r"></param>
    /// <param name="diagonal"></param>
    /// <returns></returns>
    public static SizeF SetDiagonal(this SizeF r, float diagonal)
    {
        var ru = r.Proportion();
        return new SizeF(ru.Multiply(diagonal));
    }
    /// <summary>
    /// Get Anchor Point of a Rectangle
    /// 0---1
    /// | 4 |
    /// 2---3
    /// </summary>
    /// <param name="r"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static PointF GetAnchor(this RectangleF r, int a)
    {
        return a switch
        {
            0 => new PointF(r.Left, r.Top),
            1 => new PointF(r.Right, r.Top),
            2 => new PointF(r.Left, r.Bottom),
            3 => new PointF(r.Right, r.Bottom),
            4 => r.Location.Add(r.Size.Multiply(0.5f)),
            _ => r.Location
        };
    }
    /// <summary>
    /// Get Anchors of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF[] GetAnchors(this RectangleF r)
    {
        return new PointF[] { r.GetAnchor(0), r.GetAnchor(1), r.GetAnchor(2), r.GetAnchor(3), r.GetAnchor(4) };
    }
    /// <summary>
    /// Set Anchor position of a Rectangle (No Resize)
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="a">Anchor Index</param>
    /// <param name="p">New Position of Anchor</param>
    /// <returns></returns>
    public static RectangleF SetAnchor(this RectangleF r, int a, PointF p)
    {
        var sz = r.Size;
        var l = a switch
        {
            0 => p,
            1 => new PointF(p.X - sz.Width, p.Y),
            2 => new PointF(p.X, p.Y - sz.Height),
            3 => new PointF(p.X - sz.Width, p.Y - sz.Height),
            4 => p.Add(sz.Multiply(-0.5f)),
            _ => r.Location
        };
        return new RectangleF(l, sz);
    }
    /// <summary>
    /// Add a Point with a Size
    /// </summary>
    /// <param name="p"></param>
    /// <param name="sz"></param>
    /// <returns></returns>
    public static PointF Add(this PointF p, SizeF sz) => new(p.X + sz.Width, p.Y + sz.Height);

    /// <summary>
    /// Get Center of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF GetCenter(this RectangleF r) => r.Location.Add(r.Size.Multiply(0.5f));

    /// <summary>
    /// Set Center of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static RectangleF SetCenter(this RectangleF r, PointF p)
    {
        p = p.Add(r.Size.Multiply(-0.5f));
        r.Location = p;
        return r;
    }
    /// <summary>
    /// Set Padding of a Rectangle
    /// +---+
    /// |+-+|
    /// || ||
    /// |+-+|
    /// +---+ 
    /// </summary>
    /// <param name="r"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static RectangleF SetPadding(this RectangleF r, float a, float b, float c, float d) => new(r.Location.AddXY(a,b), r.Size.Add(-c-a, -d-b));
}