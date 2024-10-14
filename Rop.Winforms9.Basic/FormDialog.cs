using System.ComponentModel;

namespace Rop.Winforms9.Basic;

internal partial class _dummy{}
public class FormDialog:FormDownPanel
{
    private new FormStartPosition StartPosition
    {
        get => base.StartPosition;
        set { }
    }
    private new FormBorderStyle FormBorderStyle
    {
        get => base.FormBorderStyle;
        set { }
    }
    private new bool MinimizeBox
    {
        get => base.MinimizeBox;
        set { }
    }
    private new bool MaximizeBox
    {
        get => base.MaximizeBox;
        set { }
    }
    private new bool ShowInTaskbar
    {
        get => base.ShowInTaskbar;
        set { }
    }
    private new bool TopMost
    {
        get => base.TopMost;
        set { }
    }
    public FormDialog()
    {
        base.StartPosition = FormStartPosition.CenterParent;
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        base.MinimizeBox= false;
        base.MaximizeBox = false;
        base.ShowInTaskbar = false;
        base.TopMost = true;
    }

    public override void Init()
    {
        base.Init();
        this.Activate();
    }
}