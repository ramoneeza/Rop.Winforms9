using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static partial class HelperGeom
{
    #region FLOATS
    /// <summary>
    /// Add Two Sizes
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static SizeF Add(this SizeF sz, SizeF size) => new SizeF(sz.Width + size.Width, sz.Height + size.Height);
    /// <summary>
    /// Add Size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public static SizeF Add(this SizeF sz, float w, float h) => new SizeF(sz.Width + w, sz.Height + h);
    /// <summary>
    /// Subtracts Sizes
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static SizeF Sub(this SizeF sz, SizeF size) => new SizeF(sz.Width - size.Width, sz.Height - size.Height);
    /// <summary>
    /// Scale Size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SizeF Scale(this SizeF sz, float scale) => new SizeF(sz.Width * scale, sz.Height * scale);
    /// <summary>
    /// Scale Size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns></returns>
    public static SizeF ScaleF(this SizeF sz, float scalex, float scaley) => new SizeF(sz.Width * scalex, sz.Height * scaley);
    /// <summary>
    /// Center between two sizes
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>

    public static SizeF Center(SizeF p1, SizeF p2) => (p2.Add(p1)).Scale(0.5f);
    /// <summary>
    /// Scale Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static PointF Scale(this PointF p, float scale) => new PointF(p.X * scale, p.Y * scale);
    /// <summary>
    /// Scale Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns></returns>
    public static PointF Scale(this PointF p, float scalex, float scaley) => new PointF(p.X * scalex, p.Y * scaley);
    /// <summary>
    /// Add X to Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static PointF AddX(this PointF p, float x) => new PointF(p.X + x, p.Y);
    /// <summary>
    /// Add Y to Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static PointF AddY(this PointF p, float y) => new PointF(p.X, p.Y + y);
    /// <summary>
    /// Add X and Y to Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static PointF AddXY(this PointF p, float x, float y) => new PointF(p.X + x, p.Y + y);
    /// <summary>
    /// Add Two Points
    /// </summary>
    /// <param name="p"></param>
    /// <param name="pto"></param>
    /// <returns></returns>
    public static PointF Add(this PointF p, PointF pto) => new PointF(p.X + pto.X, p.Y + pto.Y);
    /// <summary>
    /// Substracts X and Y from Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static PointF SubXY(this PointF p, float x, float y) => p.AddXY(-x, -y);
    /// <summary>
    ///  Substracts Two Points
    /// </summary>
    /// <param name="p"></param>
    /// <param name="pto"></param>
    /// <returns></returns>
    public static PointF Sub(this PointF p, PointF pto) => new PointF(p.X - pto.X, p.Y + pto.Y);
    /// <summary>
    /// Center between two points
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static PointF Center(PointF p1, PointF p2) => p2.Add(p1).Scale(0.5f);

    /// <summary>
    /// Increase Width of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="w">Increase Width</param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static RectangleF DeltaWidth(this RectangleF r, float w, bool changepos = false) => r.DeltaSize(w, 0, changepos);
    /// <summary>
    /// Increase Height of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="h">Height</param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static RectangleF DeltaHeight(this RectangleF r, float h, bool changepos = false) => r.DeltaSize(0, h, changepos);
    /// <summary>
    /// Increase Size of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="w">Increase Width</param>
    /// <param name="h">Increase Height</param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static RectangleF DeltaSize(this RectangleF r, float w, float h, bool changepos = false) => r.DeltaSize(new SizeF(w, h), changepos);
    /// <summary>
    /// Increase Size of Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="sz"></param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static RectangleF DeltaSize(this RectangleF r, SizeF sz, bool changepos = false) => r.DeltaPosSize(changepos ? (sz.Scale(-1)) : SizeF.Empty, sz);
    /// <summary>
    /// Increase Position of Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="changesize">Change size</param>
    /// <returns></returns>
    public static RectangleF DeltaPos(this RectangleF r, float x, float y, bool changesize = false) => r.DeltaPos(new SizeF(x, y), changesize);
    /// <summary>
    /// Increase Position of Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="offset"></param>
    /// <param name="changesize"></param>
    /// <returns></returns>
    public static RectangleF DeltaPos(this RectangleF r, SizeF offset, bool changesize = false) => r.DeltaPosSize(offset, changesize ? (offset.Scale(-1)) : SizeF.Empty);
    /// <summary>
    /// Increase Both. Location and Size of a REetangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="offset"></param>
    /// <param name="deltasize"></param>
    /// <returns></returns>
    public static RectangleF DeltaPosSize(this RectangleF r, SizeF offset, SizeF deltasize) => new RectangleF(r.Location + offset, r.Size + deltasize);
    public static RectangleF DeltaPosSize(this RectangleF r,float dx,float dy,float dw,float dh) =>r.DeltaPosSize(new SizeF(dx,dy),new SizeF(dw,dh));
    /// <summary>
    /// Centar a Rectangle
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="sz2"></param>
    /// <returns></returns>
    public static RectangleF Center(this RectangleF r1, SizeF sz2)
    {
        var sz1 = r1.Size;
        var delta = (sz2 - sz1).Scale(0.5f);
        return DeltaPos(r1, delta);
    }
    /// <summary>
    /// Center a Rectangle
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <returns></returns>
    public static RectangleF Center(this RectangleF r1, RectangleF r2) => Center(r1, r2.Size);
    /// <summary>
    /// Get CenterPoint of a Rectangle
    /// </summary>
    /// <param name="r1"></param>
    /// <returns></returns>
    public static PointF CenterPoint(this RectangleF r1) => new PointF((r1.Left + r1.Right) / 2.0f, (r1.Top + r1.Bottom) / 2.0f);
    /// <summary>
    /// Get TopLeft of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF TopLeft(this RectangleF r) => new PointF(r.Left, r.Top);
    /// <summary>
    /// Get TopRight of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF TopRight(this RectangleF r) => new PointF(r.Right, r.Top);
    /// <summary>
    /// Get BottomRight of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF BottomRight(this RectangleF r) => new PointF(r.Right, r.Bottom);
    /// <summary>
    /// Get BottomLeft of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static PointF BottomLeft(this RectangleF r) => new PointF(r.Left, r.Bottom);
    /// <summary>
    /// Obtain Rectangle from two points;
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static RectangleF RectangleFrom2Points(PointF p1, PointF p2) => new RectangleF(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
    /// <summary>
    /// Split Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="Up"></param>
    /// <param name="Down"></param>
    /// <param name="y"></param>
    public static void SplitHorizontal(this RectangleF r, out RectangleF Up, out RectangleF Down, float? y = null)
    {
        float ys = y ?? ((r.Bottom + r.Top) / 2.0f);
        Up = RectangleFrom2Points(r.TopLeft(), new PointF(r.Right, ys));
        Down = RectangleFrom2Points(new PointF(r.Left, ys), r.BottomRight());
    }
    /// <summary>
    /// Split Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <param name="Left"></param>
    /// <param name="Right"></param>
    /// <param name="x"></param>
    public static void SplitVertical(this RectangleF r, out RectangleF Left, out RectangleF Right, float? x = null)
    {
        float xs = x ?? ((r.Left + r.Right) / 2.0f);
        Left = RectangleFrom2Points(r.TopLeft(), new PointF(xs, r.Bottom));
        Right = RectangleFrom2Points(new PointF(xs, r.Top), r.BottomRight());
    }

    #endregion
}