using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static partial class HelperGeom
{
    public static void Deconstruct(this Point p, out int x, out int y)
    {
        x = p.X;
        y = p.Y;
    }

    public static void Deconstruct(this PointF p, out float x, out float y)
    {
        x = p.X;
        y = p.Y;
    }

    public static void Deconstruct(this Size s, out int width, out int height)
    {
        width = s.Width;
        height = s.Height;
    }

    public static void Deconstruct(this SizeF s, out float width, out float height)
    {
        width = s.Width;
        height = s.Height;
    }

    public static void Deconstruct(this Rectangle s, out Point location, out Size size)
    {
        location = s.Location;
        size = s.Size;
    }

    public static void Deconstruct(this RectangleF s, out PointF location, out SizeF size)
    {
        location = s.Location;
        size = s.Size;
    }

    public static Point ToPoint(this (int, int) tp) => new Point(tp.Item1, tp.Item2);
    public static Size ToSize(this (int, int) tp) => new Size(tp.Item1, tp.Item2);
    public static PointF ToPointF(this (float, float) tp) => new PointF(tp.Item1, tp.Item2);
    public static SizeF ToSizeF(this (float, float) tp) => new SizeF(tp.Item1, tp.Item2);
}