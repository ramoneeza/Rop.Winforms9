namespace Rop.Winforms9.Helper;

public static class TabControlHelper
{
    public static void HideTabPage(this TabControl tabcontrol, TabPage tp)
    {
        if (tabcontrol.TabPages.Contains(tp)) tabcontrol.TabPages.Remove(tp);
    }
    public static void ShowTabPage(this TabControl tabcontrol, TabPage tp)
    {
        if (tabcontrol.TabPages.Contains(tp)) return;
        tabcontrol.TabPages.Add(tp);
        tabcontrol.SelectedTab = tp;
    }
}