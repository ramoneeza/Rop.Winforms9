using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static partial class HelperRectangle
{
    public static Rectangle NewCentered(this Rectangle outrectangle, Size newsize)
    {
        var x = outrectangle.Left + (outrectangle.Width - newsize.Width) / 2;
        var y = outrectangle.Top + (outrectangle.Height - newsize.Height) / 2;
        return new Rectangle(new Point(x, y), newsize);
    }
    public static RectangleF NewCentered(this RectangleF outrectangle, SizeF newsize)
    {
        var x = outrectangle.Left + (outrectangle.Width - newsize.Width) / 2F;
        var y = outrectangle.Top + (outrectangle.Height - newsize.Height) / 2F;
        return new RectangleF(new PointF(x, y), newsize);
    }
    public static SizeF FillInto(this SizeF sizein, SizeF sizeout)
    {
        if ((sizeout.Width < 0.001) || (sizeout.Height < 0.001))
            sizeout = new SizeF(1, 1);
        var s1 = (sizein.Width) / (sizeout.Width);
        var s2 = (sizein.Height) / (sizeout.Height);
        var s = Math.Max(s1, s2);
        return sizein.Multiply(1 / s);
    }
    public static Size FillInto(this Size sizein, Size sizeout)
    {
        var sf = (SizeF)sizein;
        return sf.FillInto(sizeout).ToSize();
    }
    public static Rectangle FillIntoCentered(this Rectangle innerrectangle, Rectangle outerrectangle)
    {
        var newsz = innerrectangle.Size.FillInto(outerrectangle.Size);
        return outerrectangle.NewCentered(newsz);
    }
    public static RectangleF FillIntoCentered(this RectangleF innerrectangle, RectangleF outerrectangle)
    {
        var newsz = innerrectangle.Size.FillInto(outerrectangle.Size);
        return outerrectangle.NewCentered(newsz);
    }
}