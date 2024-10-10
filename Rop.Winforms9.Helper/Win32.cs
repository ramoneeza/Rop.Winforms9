using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace Rop.Winforms9.Helper;

public static class Win32Helper
{
    public static Wm Msg(this Message m) => (Wm)m.Msg;
    public static long LParam(this Message m) => m.LParam.ToInt64();

    public static Point LParamToPoint(this Message m)
    {
        var xy = m.LParam();
        var x = unchecked((int)(xy & 0x00FFL));
        var y = unchecked((int)((xy >> 16) & 0x00FFL));
        return new Point(x, y);
    }
        
    [DllImport("user32.dll")]
    public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

    [DllImport("user32.dll")]
    public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern bool GetObject(IntPtr hObject, int nCount, out BITMAP lpObject);
        
    [DllImport("user32.dll")]
    public static extern IntPtr CreateIconIndirect(ref ICONINFO icon);
        
    [StructLayout(LayoutKind.Sequential)]
    public struct ICONINFO
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAP
    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public ushort bmPlanes;
        public ushort bmBitsPixel;
        public IntPtr bmBits;
    }
    const int IDC_ARROW = 32512;

    public static (Size,Point) SystemIconSize()
    {
        IntPtr hCursor = LoadCursor(IntPtr.Zero, IDC_ARROW);
        if (hCursor==IntPtr.Zero) return default;
        if (!GetIconInfo(hCursor, out  var iconInfo)) return default;
        if (!GetObject(iconInfo.hbmMask, Marshal.SizeOf(typeof(BITMAP)), out var bitmap)) return default;
        return (new Size(bitmap.bmWidth, bitmap.bmHeight),new Point(iconInfo.xHotspot,iconInfo.yHotspot));
    }
}