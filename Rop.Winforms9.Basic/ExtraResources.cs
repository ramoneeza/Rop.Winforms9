using Rop.Winforms9.Helper;

namespace Rop.Winforms9.Basic
{
    public static class ExtraResources
    {
        private static readonly Lazy<Bitmap> _bmpcursor;
        private static readonly PointF _cursorHotSpot = new PointF(125/512f, 25/512f);
        public static Bitmap BmpCursor=>_bmpcursor.Value;
        private static readonly Lazy<Cursor> _transparentCursor;
        public static Cursor TransparentCursor => _transparentCursor.Value;
        static ExtraResources()
        {
            _bmpcursor=new Lazy<Bitmap>(_loadbmpcursor);
            _transparentCursor= new Lazy<Cursor>(_loadTransparentCursor);
        }
        private static Cursor _loadTransparentCursor()
        {
            var szhs = Win32Helper.SystemIconSize();
            var sz = szhs.Item1;
            if (sz.IsEmpty) return Cursors.Default;
            var bmp = new Bitmap(sz.Width, sz.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawImage(BmpCursor, 0, 0, sz.Width, sz.Height);
            }
            Win32Helper.ICONINFO iconInfo = new Win32Helper.ICONINFO();
            iconInfo.fIcon = false; // Especifica que es un cursor, no un icono
            iconInfo.xHotspot =(int)(_cursorHotSpot.X*sz.Width); // Define el xHotspot
            iconInfo.yHotspot =(int)(_cursorHotSpot.Y*sz.Height); // Define el yHotspot
            iconInfo.hbmMask = bmp.GetHbitmap();
            iconInfo.hbmColor=bmp.GetHbitmap();
            // Crea el cursor
            IntPtr hIcon = Win32Helper.CreateIconIndirect(ref iconInfo);
            Cursor cursor = new Cursor(hIcon);
            return cursor;
        }
        private static Bitmap _loadbmpcursor()
        {
            using var stream = Resources.Get("cursor.png")??throw new Exception("Recurso Cursor no encontrado");
            return new Bitmap(stream);
        }
    }
}
