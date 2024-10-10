using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rop.Winforms9.Helper;

public static partial class WinFormsHelper
{
    public static bool IsRealDesignerMode(this Control ctrl)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
        var c = ctrl;
        while (c != null)
        {
            if (c.Site is { DesignMode: true }) return true;
            c = c.Parent;
        }
        return System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
    }
    

    public static void SetIcon(this Form f, Icon? ic = null)
    {
        ic ??= f.ParentForm?.Icon;
        if (ic==null) return;
        try
        {
            f.Icon = ic;
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.Print(e.Message);
        }
    }
    [DllImport("user32.dll")]
    public static extern int SendMessage(
        IntPtr hWnd,      // handle to destination window
        int Msg,       // message
        IntPtr wParam,  // first message parameter
        IntPtr lParam   // second message parameter
    );
        
    private static readonly FieldInfo? _defaultIconField=typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static);
    private static readonly FieldInfo? _defaultFontField=typeof(Form).GetField("defaultFont", BindingFlags.NonPublic | BindingFlags.Static);

    public static void SetDefaultIcon(Icon ico)
    {
        _defaultIconField?.SetValue(null, ico);
    }
    public static void SetDefaultFont(Font font)
    {
        _defaultFontField?.SetValue(null, font);
    }
    
    private static readonly MethodInfo? _setvisiblecore=typeof(Control).GetMethod("SetVisibleCore", BindingFlags.NonPublic | BindingFlags.Instance);
    public static void ForceSetVisibleCore(this Control ctrl, bool value)
    {
        _setvisiblecore?.Invoke(ctrl, new object[] { value });
    }

    public static Font GetDefaultSystemFont() => SystemFonts.MessageBoxFont??throw new NullReferenceException("No messageboxfont");
    public static Form SetVersion(this Form f)
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        if (version is null) return f;
        f.Text += " " + version.ToString();
        return f;
    }
    public static Control? FindControlAtPoint(this Control container, Point pos)
    {
        Control? child;
        foreach (Control c in container.Controls)
        {
            if (c.Visible && c.Bounds.Contains(pos))
            {
                child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                return child ?? c;
            }
        }
        return null;
    }

    public static Control? FindControlAtCursor(this Form form)
    {
        Point pos = Cursor.Position;
        if (form.Bounds.Contains(pos))
            return FindControlAtPoint(form, form.PointToClient(pos));
        return null;
    }
}