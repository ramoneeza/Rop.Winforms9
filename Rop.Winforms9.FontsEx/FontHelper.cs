using System.Drawing;

namespace Rop.Winforms9.FontsEx;

public static class FontHelper
{

    public static int PointsToPixels(float points, float dpi) => (int)(points * (dpi / 72f));
    public static int PointsToPixels(this Graphics g, float points) => PointsToPixels(points, g.DpiY);

    public static float GetAscentPoints(this Font f)
    {
        var a = f.GetAscentUnit();
        return a * f.SizeInPoints;
    }

    public static float GetAscentUnit(this Font f)
    {
        float heightEm = f.FontFamily.GetEmHeight(f.Style);
        float ascent = f.FontFamily.GetCellAscent(f.Style);
        return ascent / heightEm;
    }

    public static int GetAscentPixels(this Font f, float dpi)
    {
        var a = f.GetAscentUnit();
        return (int)(a * PointsToPixels(f.SizeInPoints, dpi));
    }

    public static int GetAscentPixels(this Font f, Graphics gr)
    {
        return f.GetAscentPixels(gr.DpiY);

    }
    public static FontSizeF MeasureTextSizeWithAscent(this Graphics gr,Font font, string text)
    {
        var sz = gr.MeasureString(text, font, PointF.Empty, StringFormat.GenericTypographic);
        var a = font.GetAscentPixels(gr);
        return new FontSizeF(sz, a);
    }

    public static FontRectangleF MeasureStringWithBaseLine(this Graphics gr,  Font font, PointF baseline, string text)
    {
        var sz = gr.MeasureTextSizeWithAscent(font, text);
        return FontRectangleF.FromBaseLine(baseline.X, baseline.Y, sz.Width, sz.Height, sz.Ascent);
    }
}