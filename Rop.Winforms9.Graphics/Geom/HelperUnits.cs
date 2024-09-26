using System.Drawing;
namespace Rop.Winforms9.GraphicsEx.Geom;
public static partial class HelperUnits
{
    public static float PixelToUnit(this Graphics g, float v, GraphicsUnit? unit = null)
    {
        var u = unit ?? g.PageUnit;
        switch (u)
        {
            case GraphicsUnit.Display: return (v / g.DpiX) * 100;
            case GraphicsUnit.Document: return (v / g.DpiX) * 300;
            case GraphicsUnit.Inch: return v / g.DpiX;
            case GraphicsUnit.Millimeter: return (v / g.DpiX) * 25.4F;
            case GraphicsUnit.Pixel: return v;
            case GraphicsUnit.Point: return (v / g.DpiX) * 72;
            default: return v;
        }
    }
    public static float UnitToPixel(this Graphics g, float v, GraphicsUnit? unit = null)
    {
        var u = unit ?? g.PageUnit;
        switch (u)
        {
            case GraphicsUnit.Display: return (v / 100) * g.DpiX;
            case GraphicsUnit.Document: return (v / 300) * g.DpiX;
            case GraphicsUnit.Inch: return v * g.DpiX;
            case GraphicsUnit.Millimeter: return (v / 25.4F) * g.DpiX;
            case GraphicsUnit.Pixel: return v;
            case GraphicsUnit.Point: return (v / 72) * g.DpiX;
            default: return v;
        }
    }
    public static float UnitToUnit(this Graphics g, float v, GraphicsUnit ori, GraphicsUnit dst) => PixelToUnit(g, UnitToPixel(g, v, ori), dst);
    public static float PixelToMM(this Graphics g, float p)
    {
        var resmm = g.DpiX / 25.4F;
        return p / resmm;
    }
    public static PointF PixelsToMM(this Graphics g, PointF p) => new PointF(g.PixelToMM(p.X), g.PixelToMM(p.Y));
    public static SizeF PixelsToMM(this Graphics g, SizeF p) => new SizeF(g.PixelToMM(p.Width), g.PixelToMM(p.Height));
    public static float MMToPixel(this Graphics g, float mm)
    {
        var resmm = g.DpiX / 25.4F;
        return mm * resmm;
    }
    public static Point MMToPixel(this Graphics g, PointF mm)
    {
        var resmm = g.DpiX / 25.4;
        return new Point((int)Math.Round(mm.X * resmm), (int)Math.Round(mm.Y * resmm));
    }
    public static Size MMToPixel(this Graphics g, SizeF mm)
    {
        var resmm = g.DpiX / 25.4;
        return new Size((int)Math.Round(mm.Width * resmm), (int)Math.Round(mm.Height * resmm));
    }
}