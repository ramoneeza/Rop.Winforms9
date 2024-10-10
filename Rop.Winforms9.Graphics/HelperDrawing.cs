using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Rop.Winforms9.GraphicsEx.Geom;

namespace Rop.Winforms9.GraphicsEx
{
    /// <summary>
    /// Helper class for Drawing
    /// </summary>
    public static class HelperDrawing
    {
        public static void AddMemoryFont(this PrivateFontCollection pfc, byte[] font)
        {
            var data = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, data, font.Length);
            pfc.AddMemoryFont(data, font.Length);
            Marshal.FreeCoTaskMem(data);
        }
        

        public static float Baseline(this Graphics g, string s, Font f)
        {
            var baselineOffset = f.SizeInPoints / f.FontFamily.GetEmHeight(f.Style) *
                                 f.FontFamily.GetCellAscent(f.Style);
            return g.UnitToUnit(baselineOffset, GraphicsUnit.Point, g.PageUnit);
        }

        public static SizeF MeasureTextBaseline(this Graphics g, string s, Font f)
        {
            var baselineOffset = g.Baseline(s, f);
            var medida = g.MeasureString(s, f);
            medida.Height = baselineOffset;
            return medida;
        }

        public static void DrawOnBaseline(this Graphics g, string s, Font f, Brush b, PointF pos)
        {
            var baselineOffset = g.Baseline(s, f);
            g.DrawString(s, f, b, new PointF(pos.X, pos.Y - baselineOffset), StringFormat.GenericTypographic);
        }
        
        public static void DrawAligmentString(this Graphics g, string cad, Font f, Brush br, RectangleF rect, ContentAlignment aligment, StringFormat sf)
        {
            var sz = g.MeasureString(cad, f, rect.Size, sf).ToSize();
            var res = rect;
            switch (aligment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    res = res.DeltaPos((rect.Width - sz.Width) / 2, 0);
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    res = res.DeltaPos((rect.Width - sz.Width), 0);
                    break;
            }

            switch (aligment)
            {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    res = res.DeltaPos(0, (rect.Height - sz.Height) / 2);
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    res = res.DeltaPos(0, (rect.Height - sz.Height));
                    break;
            }

            g.DrawString(cad, f, br, res, sf);
        }

        public static void DrawCenterString(this Graphics g, string cad, Font f, Brush br, float centerx, float y)
        {
            var sz = g.MeasureString(cad, f);
            var x = centerx - sz.Width / 2;
            g.DrawString(cad, f, br, x, y);
        }

        /// <summary>
        /// Extension to Draw a String Right Aligned
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width</param>
        public static void DrawRightString(this Graphics g, string cad, Font f, Brush br, float x, float y, float width)
        {
            var sz = g.MeasureString(cad, f);
            g.DrawString(cad, f, br, x + width - sz.Width, y);
        }

        /// <summary>
        /// Extension to Draw a String Right Aligned and Vertical Centered
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public static void DrawRightMiddleString(this Graphics g, string cad, Font f, Brush br, float x, float y, float width,float height)
        {
            var sz = g.MeasureString(cad, f);
            g.DrawString(cad, f, br, x + width - sz.Width, y + (height - sz.Height) / 2);
        }

        /// <summary>
        /// Extension to Draw a String Centered
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width</param>
        public static void DrawCenterString(this Graphics g, string cad, Font f, Brush br, float x, float y, float width)
        {
            var sz = g.MeasureString(cad, f);
            g.DrawString(cad, f, br, x + (width - sz.Width) / 2, y);
        }

        /// <summary>
        /// Extension to Draw a String Vertical and Horizontal Centered
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public static void DrawCenterMiddleString(this Graphics g, string cad, Font f, Brush br, float x, float y,float width, float height)
        {
            var sz = g.MeasureString(cad, f);
            g.DrawString(cad, f, br, x + (width - sz.Width) / 2, y + (height - sz.Height) / 2);
        }

        /// <summary>
        /// Extension to Draw a Right Aligned String
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="rect">Container Rectangle</param>
        public static void DrawRightString(this Graphics g, string cad, Font f, Brush br, RectangleF rect)
        {
            DrawRightString(g, cad, f, br, rect.X, rect.Y, rect.Width);
        }

        /// <summary>
        /// Extension to Draw a Right Aligned String Vertical Centered
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="rect">Container Rectangle</param>

        public static void DrawRightMiddleString(this Graphics g, string cad, Font f, Brush br, RectangleF rect)
        {
            DrawRightMiddleString(g, cad, f, br, rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Extension to Draw a Centered String
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="rect">Container Rectangle</param>
        public static void DrawCenterString(this Graphics g, string cad, Font f, Brush br, RectangleF rect)
        {
            DrawCenterString(g, cad, f, br, rect.X, rect.Y, rect.Width);
        }

        /// <summary>
        /// Extension to Draw a Horizontal and Vertical Centered String
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cad">String</param>
        /// <param name="f">Font</param>
        /// <param name="br">Brush</param>
        /// <param name="rect">Container Rectangle</param>
        public static void DrawCenterMiddleString(this Graphics g, string cad, Font f, Brush br, RectangleF rect)
        {
            DrawCenterMiddleString(g, cad, f, br, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static void DrawSoftImage(this Graphics g,Bitmap img,RectangleF dest,InterpolationMode? interpolationMode=null)
        {
            interpolationMode ??= InterpolationMode.HighQualityBilinear;
            var im = g.InterpolationMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = interpolationMode.Value;
            g.DrawImage(img, dest, new RectangleF(PointF.Empty, img.Size), GraphicsUnit.Pixel);
            g.InterpolationMode = im;
        }
    }
}