using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static partial class HelperGeom
{


    /// <summary>
    /// Add 2 sizes
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="size"></param>
    /// <returns>new Size</returns>
    public static Size Add(this Size sz, Size size) => new Size(sz.Width + size.Width, sz.Height + size.Height);
    /// <summary>
    /// Add Width and Height to a size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns>new Size</returns>
    public static Size Add(this Size sz, int w, int h) => new Size(sz.Width + w, sz.Height + h);
    /// <summary>
    /// Substracts two sizes
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="size"></param>
    /// <returns>new Size</returns>
    public static Size Sub(this Size sz, Size size) => new Size(sz.Width - size.Width, sz.Height - size.Height);
    /// <summary>
    /// Scale a size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scale"></param>
    /// <returns>new Size</returns>
    public static Size Scale(this Size sz, int scale) => new Size(sz.Width * scale, sz.Height * scale);
    /// <summary>
    /// Scale a size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns>new Size</returns>
    public static Size Scale(this Size sz, int scalex, int scaley) => new Size(sz.Width * scalex, sz.Height * scaley);
    /// <summary>
    /// Scale a size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scale"></param>
    /// <returns>new Size</returns>
    public static Size Scale(this Size sz, float scale) => new Size((int)Math.Round(sz.Width * scale), (int)Math.Round(sz.Height * scale));
    /// <summary>
    /// Scale a size
    /// </summary>
    /// <param name="sz"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns>new Size</returns>
    public static Size Scale(this Size sz, float scalex, float scaley) => new Size((int)Math.Round(sz.Width * scalex), (int)Math.Round(sz.Height * scaley));
    /// <summary>
    /// Center between two Sizes
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns>Center point</returns>
    public static Size Center(Size p1, Size p2) => (p2.Add(p1)).Scale(0.5f);
    /// <summary>
    /// Scale a Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scale"></param>
    /// <returns>Scaled point</returns>
    public static Point Scale(this Point p, int scale) => new Point(p.X * scale, p.Y * scale);
    /// <summary>
    /// Scale a Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns>Scaled Point</returns>
    public static Point Scale(this Point p, int scalex, int scaley) => new Point(p.X * scalex, p.Y * scaley);
    /// <summary>
    /// Scale a Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scale"></param>
    /// <returns>Scaled Point</returns>
    public static Point Scale(this Point p, float scale) => new Point((int)Math.Round(p.X * scale), (int)Math.Round(p.Y * scale));
    /// <summary>
    /// Scale a Point
    /// </summary>
    /// <param name="p"></param>
    /// <param name="scalex"></param>
    /// <param name="scaley"></param>
    /// <returns>Scaled Point</returns>
    public static Point Scale(this Point p, float scalex, float scaley) => new Point((int)Math.Round(p.X * scalex), (int)Math.Round(p.Y * scaley));

    public static Size ScaleHeight(this Size p, int height)
    {
        var h = p.Height;
        if (h == 0) return p;
        p.Height = height;
        p.Width = (int)Math.Round(p.Width * height / (h * 1.0));
        return p;
    }

    public static Size ScaleWidth(this Size p, int width)
    {
        var w = p.Width;
        if (w == 0) return p;
        p.Width = width;
        p.Height = (int)Math.Round(p.Height * width / (w * 1.0));
        return p;
    }

    /// <summary>
    /// Increase X value
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Point AddX(this Point p, int x) => new Point(p.X + x, p.Y);
    /// <summary>
    /// Increase Y value
    /// </summary>
    /// <param name="p"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Point AddY(this Point p, int y) => new Point(p.X, p.Y + y);
    /// <summary>
    /// Increase X and Y values
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Point AddXY(this Point p, int x, int y) => new Point(p.X + x, p.Y + y);
    /// <summary>
    /// Add Two points
    /// </summary>
    /// <param name="p"></param>
    /// <param name="pto"></param>
    /// <returns></returns>
    public static Point Add(this Point p, Point pto) => new Point(p.X + pto.X, p.Y + pto.Y);
    public static Point Add(this Point p, Size sz) => new Point(p.X + sz.Width, p.Y + sz.Height);
    /// <summary>
    /// Substracts X and Y values
    /// </summary>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Point SubXY(this Point p, int x, int y) => p.AddXY(-x, -y);
    /// <summary>
    /// Substracts two points
    /// </summary>
    /// <param name="p"></param>
    /// <param name="pto"></param>
    /// <returns></returns>
    public static Point Sub(this Point p, Point pto) => new Point(p.X - pto.X, p.Y + pto.Y);
    /// <summary>
    /// Center between two points
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns>Center Point</returns>
    public static Point Center(Point p1, Point p2) => p2.Add(p1).Scale(0.5f);

    /// <summary>
    /// Increase Width of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="w">Increase Width</param>
    /// <param name="changepos">Change X Position</param>
    /// <returns></returns>
    public static Rectangle DeltaWidth(this Rectangle r, int w, bool changepos = false) => r.DeltaSize(w, 0, changepos);
    /// <summary>
    /// Increase Heigth of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="h">Increase Height</param>
    /// <param name="changepos">Change Y Position</param>
    /// <returns></returns>
    public static Rectangle DeltaHeight(this Rectangle r, int h, bool changepos = false) => r.DeltaSize(0, h, changepos);
    /// <summary>
    /// Increase Size of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="w">Increase Width</param>
    /// <param name="h">Increase Height</param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static Rectangle DeltaSize(this Rectangle r, int w, int h, bool changepos = false) => r.DeltaSize(new Size(w, h), changepos);
    /// <summary>
    /// Increase Size of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="sz">Increase Size</param>
    /// <param name="changepos">Change Position</param>
    /// <returns></returns>
    public static Rectangle DeltaSize(this Rectangle r, Size sz, bool changepos = false) => r.DeltaPosSize(changepos ? (sz.Scale(-1)) : Size.Empty, sz);
    /// <summary>
    /// Change Position of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="x">New X position</param>
    /// <param name="y">New Y position</param>
    /// <param name="changesize">Change Size of Rectangle</param>
    /// <returns></returns>
    public static Rectangle DeltaPos(this Rectangle r, int x, int y, bool changesize = false) => r.DeltaPos(new Size(x, y), changesize);
    /// <summary>
    /// Change Position of Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="offset">Position Offset</param>
    /// <param name="changesize">Change Size of Rectangle</param>
    /// <returns></returns>
    public static Rectangle DeltaPos(this Rectangle r, Size offset, bool changesize = false) => r.DeltaPosSize(offset, changesize ? (offset.Scale(-1)) : Size.Empty);
    /// <summary>
    /// Change Both (Position and Size) or Rectangle
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="offset">Position Offset</param>
    /// <param name="deltasize">Increase Size</param>
    /// <returns></returns>
    public static Rectangle DeltaPosSize(this Rectangle r, Size offset, Size deltasize) => new Rectangle(r.Location + offset, r.Size + deltasize);

    public static Rectangle DeltaPosSize(this Rectangle r,int dx,int dy,int dw,int dh) =>r.DeltaPosSize(new Size(dx,dy),new Size(dw,dh));

    /// <summary>
    /// Center a Rectangle in a new Size
    /// </summary>
    /// <param name="r1">Rectangle</param>
    /// <param name="sz2">Size</param>
    /// <returns>Centered Rectangle</returns>
    public static Rectangle Center(this Rectangle r1, Size sz2)
    {
        var sz1 = r1.Size;
        var delta = (sz2 - sz1).Scale(0.5f);
        return DeltaPos(r1, delta);
    }
    /// <summary>
    /// Center a Rectangle in a new Size
    /// </summary>
    /// <param name="r1">Rectangle</param>
    /// <param name="r2">Rectangle with Size</param>
    /// <returns>Centered Rectangle</returns>
    public static Rectangle Center(this Rectangle r1, Rectangle r2) => Center(r1, r2.Size);
    /// <summary>
    /// Center Point of a Rectangle
    /// </summary>
    /// <param name="r1"></param>
    /// <returns></returns>
    public static Point CenterPoint(this Rectangle r1) => new Point((r1.Left + r1.Right) / 2, (r1.Top + r1.Bottom) / 2);
    /// <summary>
    /// TopLeft of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Point TopLeft(this Rectangle r) => new Point(r.Left, r.Top);
    /// <summary>
    /// TopRight of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Point TopRight(this Rectangle r) => new Point(r.Right, r.Top);
    /// <summary>
    /// BottomRight of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Point BottomRight(this Rectangle r) => new Point(r.Right, r.Bottom);
    /// <summary>
    /// BottomLeft of a Rectanle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static Point BottomLeft(this Rectangle r) => new Point(r.Left, r.Bottom);
    /// <summary>
    /// Rectangle from Two Points
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Rectangle RectangleFrom2Points(Point p1, Point p2) => new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
    /// <summary>
    /// Split a Rectangle
    /// </summary>
    /// <param name="r">Original Rectangle</param>
    /// <param name="Up">Out Up Rectangle</param>
    /// <param name="Down">Out Down Rectangle</param>
    /// <param name="y">Cut Point (Split center if null)</param>
    public static void SplitHorizontal(this Rectangle r, out Rectangle Up, out Rectangle Down, int? y = null)
    {
        int ys = y ?? ((r.Bottom + r.Top) / 2);
        Up = RectangleFrom2Points(r.TopLeft(), new Point(r.Right, ys));
        Down = RectangleFrom2Points(new Point(r.Left, ys), r.BottomRight());
    }
    /// <summary>
    /// Split a Rectangle
    /// </summary>
    /// <param name="r">Original Rectangle</param>
    /// <param name="Left">Out Left Rectangle</param>
    /// <param name="Right">Out Right Rectangle</param>
    /// <param name="x">Cut Point (Split center if null)</param>
    public static void SplitVertical(this Rectangle r, out Rectangle Left, out Rectangle Right, int? x = null)
    {
        int xs = x ?? ((r.Left + r.Right) / 2);
        Left = RectangleFrom2Points(r.TopLeft(), new Point(xs, r.Bottom));
        Right = RectangleFrom2Points(new Point(xs, r.Top), r.BottomRight());
    }


}