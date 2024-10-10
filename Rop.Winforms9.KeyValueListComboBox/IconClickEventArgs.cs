namespace Rop.Winforms9.KeyValueListComboBox;

public class IconClickEventArgs:EventArgs{
    public int IconIndex { get; }
    public int ItemIndex { get; }
    public object Item { get; }
    public MouseButtons Button { get; }
    public IconClickEventArgs(int iconindex,int itemindex, object item, MouseButtons button)
    {
        IconIndex=iconindex;
        ItemIndex= itemindex;
        Item = item;
        Button = button;
    }
}