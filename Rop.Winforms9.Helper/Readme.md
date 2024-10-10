# Rop.Winforms9.Helper

Features
--------

Rop.Winforms9.Helper is a group of helpers to work with WinForms

## FullNameHelper

This class provides a extension method to get the full name of a control, including its parent hierarchy

## WinFormsHelper

This class contains helper methods for working with Windows Forms controls and forms. 
It includes methods to check if a control is in designer mode, set default icons and fonts, 
and find controls at specific points.

````csharp
public static partial class WinFormsHelper
{
    public static bool IsRealDesignerMode(this Control ctrl) { ... }
    public static void SetIcon(this Form f, Icon? ic = null) { ... }
    public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    public static void SetDefaultIcon(Icon ico) { ... }
    public static void SetDefaultFont(Font font) { ... }
    public static Font GetDefaultSystemFont();
    public static Form SetVersion(this Form f) { ... }
    public static Control? FindControlAtPoint(this Control container, Point pos) { ... }
    public static Control? FindControlAtCursor(this Form form) { ... }
}
````

## TabControlHelper

This class provides extension methods for working with TabControl controls.
````csharp
public static class TabControlHelper
{
    public static void HideTabPage(this TabControl tabcontrol, TabPage tp){...}
    public static void ShowTabPage(this TabControl tabcontrol, TabPage tp){...}
}
````

## DrawItemEventHelper

This class contains helper methods for handling item drawing events in Windows Forms controls

````csharp
public static class DrawItemEventHelper
{
    public static Color ColorSel(this DrawItemEventArgs e, Color color, Color colorSel){...}
    public static void DrawBackgroundEx(this DrawItemEventArgs e, Color backcolor){...}
}
````

## ClipboardEx

This class provides a static class for working with the clipboard

````csharp
public static class ClipboardEx
{
    public static T ByteArrayToStructure<T>(ReadOnlySpan<byte> bytes) where T : struct {... }
    public static Bitmap CF_DIBV5ToBitmap(byte[] data){...}
    public static Bitmap CF_DIBV5ToBitmap(MemoryStream datastream){...}
    public static Image CreateOpaqueBitmap(this Image image, Color? backgroundColor=null){...}
    public static Image? GetImageEx(){...}
    public static bool ContainsImageEx(){...}
    public static void SetImageMultiFormat(Image img){...}
    public static void SetImageMultiFormatBitmapPng(this Image image){...}
}
````

## CommonStringHelper

This class provides a static class for working with common located strings

## SortableBindingList

This class provides a generic sortable binding list


 ------
 (C)2024 Ramón Ordiales Plaza
