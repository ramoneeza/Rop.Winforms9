using System.Collections.Immutable;
using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

public static partial class BorderHelper
{

    private static readonly ImmutableArray<Pen> SunkenPens =[SystemPens.ControlDark, SystemPens.ControlDarkDark, SystemPens.ControlLightLight, SystemPens.ControlLight];
    private static readonly ImmutableArray<Pen> NoSunkenPens =[SystemPens.ControlLightLight, SystemPens.ControlLight, SystemPens.ControlDark, SystemPens.ControlDarkDark];
    /// <summary>
    /// Draw a standard 3D Border
    /// </summary>
    /// <param name="gr">Graphics</param>
    /// <param name="rect">Border Measure</param>
    /// <param name="sunken">Sunken</param>
    /// <param name="soft">Soft</param>
    public static void Draw3DBorder(this Graphics gr, Rectangle rect, bool sunken, bool soft = false)
    {
        var pens = sunken ? SunkenPens : NoSunkenPens;
        gr.DrawLine(pens[0], rect.X, rect.Y + rect.Height - 1, rect.X, rect.Y);
        gr.DrawLine(pens[0], rect.X, rect.Y, rect.X + rect.Width - 1, rect.Y);
        if (!soft) gr.DrawLine(pens[1], rect.X + 1, rect.Y + rect.Height - 2, rect.X + 1, rect.Y + 1);
        if (!soft) gr.DrawLine(pens[1], rect.X + 1, rect.Y + 1, rect.X + rect.Width - 2, rect.Y + 1);
        gr.DrawLine(pens[2], rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width - 1, rect.Y + rect.Height - 1);
        gr.DrawLine(pens[2], rect.X + rect.Width - 1, rect.Y + rect.Height - 1, rect.X + rect.Width - 1, rect.Y);
        if (!soft) gr.DrawLine(pens[3], rect.X + 1, rect.Y + rect.Height - 2, rect.X + rect.Width - 2, rect.Y + rect.Height - 2);
        if (!soft) gr.DrawLine(pens[3], rect.X + rect.Width - 2, rect.Y + rect.Height - 2, rect.X + rect.Width - 2, rect.Y + 1);
    }
    /// <summary>
    /// Draw a standard Border (3D or Flat)
    /// </summary>
    /// <param name="gr">Graphics</param>
    /// <param name="rect">Border measure</param>
    /// <param name="bordercolor">Border Color</param>
    /// <param name="is3D">Determine if Flat or 3D</param>
    /// <param name="sunken">Sunken</param>
    /// <param name="soft">Soft</param>
    public static void DrawBorder(this Graphics gr, Rectangle rect, Color bordercolor, bool is3D = false, bool sunken = false, bool soft = true)
    {
        if (!is3D)
            gr.DrawRectangle(new Pen(bordercolor), rect.DeltaSize(-1, -1));
        else
            Draw3DBorder(gr, rect, sunken, soft);
    }
    
}