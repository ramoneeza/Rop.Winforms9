using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Rop.Winforms9.Helper;

public static class ClipboardEx
{
    public static T ByteArrayToStructure<T>(ReadOnlySpan<byte> bytes) where T : struct
    {
        if (bytes.Length< Marshal.SizeOf<T>()) throw new ArgumentException("Byte array is too small");
        return MemoryMarshal.Read<T>(bytes);
    }
    public static Bitmap CF_DIBV5ToBitmap(byte[] data)
    {
        var bmi = ByteArrayToStructure<Bitmapv5Header>(data);
        var bmp = new Bitmap(bmi.bV5Width, bmi.bV5Height, PixelFormat.Format32bppArgb);
        var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        try
        {
            var iptr = bmpdata.Scan0;
            var headersize = (int)bmi.bV5Size;
            var stride = bmpdata.Stride;
            var height = bmp.Height;
            
            var dataspan=data.AsSpan(headersize+12);

            for (var y = 0; y < height; y++)
            {
                var destPtr= iptr+(height- y - 1) * stride;
                var srcSpan = dataspan.Slice(y * stride, stride);
                Marshal.Copy(srcSpan.ToArray(),0,destPtr,stride);
            }
        }
        finally
        {
            bmp.UnlockBits(bmpdata);
        }
        return bmp;
    }
    public static Bitmap CF_DIBV5ToBitmap(MemoryStream datastream)
    {
        var data = datastream.ToArray();
        var bmp = CF_DIBV5ToBitmap(data);
        return bmp;
    }
    public static Image CreateOpaqueBitmap(this Image image, Color? backgroundColor=null)
    {
        var bk=backgroundColor?? Color.LightGray;
        var bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(bk);
        graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        return bitmap;
    }
    public static Image? GetImageEx()
    {
        var data = Clipboard.GetDataObject();
        try
        {
            Debug.Assert(data != null, "data != null");
            foreach (var format in data.GetFormats())
            {
                if (format.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var ms = (MemoryStream)data.GetData("PNG")!;
                    return Image.FromStream(ms);
                }

                if (format.Equals("FileName", StringComparison.OrdinalIgnoreCase))
                {
                    var fn = (string[])data.GetData("FileName")!;
                    return fn.Length >= 1 ? Image.FromFile(fn[0]) : null;
                }

                if (format.Equals("Format17", StringComparison.OrdinalIgnoreCase))
                {
                    using var ms = (MemoryStream)data.GetData("Format17")!;
                    return CF_DIBV5ToBitmap(ms.ToArray());
                }
            }
            if (Clipboard.ContainsImage())
            {
             var i = Clipboard.GetImage();
             (i as Bitmap)?.MakeTransparent();
             return i;
            }
            return null;
        }
        catch (Exception e)
        {
            Debug.Print(e.Message);
        }
        return null;
    }
    public static bool ContainsImageEx()
    {
        return Clipboard.ContainsImage() || Clipboard.ContainsData("PNG") || Clipboard.ContainsData("Format17");
    }

    public static void SetImageMultiFormat(Image img)
    {
        
        using var ms = new MemoryStream();
        var bmp = new Bitmap(img);
        bmp.Save(ms, ImageFormat.Bmp);
        var b = ms.ToArray().AsSpan();
        var ms2 = b.Slice(14);
        Clipboard.Clear();
        var dataObject = new DataObject();
        dataObject.SetData(DataFormats.Bitmap, bmp);
        dataObject.SetData(DataFormats.Dib, ms2.ToArray());
        Clipboard.SetDataObject(dataObject, true, 3, 1000);
    }

    public static void SetImageMultiFormatBitmapPng(this Image image)
    {
        using var opaque = image.CreateOpaqueBitmap(Color.White);
        using var stream = new MemoryStream();
        image.Save(stream, ImageFormat.Png);
        Clipboard.Clear();
        var data = new DataObject();
        data.SetData(DataFormats.Bitmap, true, opaque);
        data.SetData("PNG", true, stream);
        Clipboard.SetDataObject(data, true, 3, 1000);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Bitmapv5Header
    {
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
        public uint bV5Size;
        public int bV5Width;
        public int bV5Height;
        public ushort bV5Planes;
        public ushort bV5BitCount;
        public uint bV5Compression;
        public uint bV5SizeImage;
        public int bV5XPelsPerMeter;
        public int bV5YPelsPerMeter;
        public ushort bV5ClrUsed;
        public ushort bV5ClrImportant;
        public ushort bV5RedMask;
        public ushort bV5GreenMask;
        public ushort bV5BlueMask;
        public ushort bV5AlphaMask;
        public ushort bV5CSType;
        public IntPtr bV5Endpoints;
        public ushort bV5GammaRed;
        public ushort bV5GammaGreen;
        public ushort bV5GammaBlue;
        public ushort bV5Intent;
        public ushort bV5ProfileData;
        public ushort bV5ProfileSize;
        public ushort bV5Reserved;
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
    }
}