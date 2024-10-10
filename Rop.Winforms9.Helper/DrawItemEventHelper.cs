namespace Rop.Winforms9.Helper;

public static class DrawItemEventHelper
{
    public static Color ColorSel(this DrawItemEventArgs e, Color color, Color colorSel) => ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? colorSel : color;
    public static void DrawBackgroundEx(this DrawItemEventArgs e, Color backcolor) => e.Graphics.FillRectangle(new SolidBrush(backcolor), e.Bounds);
}