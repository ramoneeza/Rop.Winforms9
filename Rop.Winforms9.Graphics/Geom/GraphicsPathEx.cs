using System.Drawing;
using System.Drawing.Drawing2D;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static class GraphicsPathEx
{
    public static GraphicsPath Factory(FillMode fm,params (PointF,PathPointType)[] figure)
    {
        // Que params es mejor en .NET 9 para luego hacer un Select?
        var pts = figure.Select(f => f.Item1).ToArray();
        var tps = figure.Select(f => (byte)f.Item2).ToArray();
        return new GraphicsPath(pts, tps, fm);
    }
}