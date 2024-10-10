namespace Rop.Winforms9.KeyValueListComboBox;

public interface IKeyValueControlDraw:IKeyValueControl
{
    event EventHandler<DrawKeyValueItemEventArgs>? DrawKeyValueItem;
    event EventHandler<DrawKeyValueItemEventArgs>? DrawKeyItem;
    event EventHandler<DrawKeyValueItemEventArgs>? DrawValueItem;
    event EventHandler<DrawKeyValueItemEventArgs>? DrawStringItem;
    event EventHandler<DrawKeyValueItemEventArgs>? PostDrawKeyItem;
    event EventHandler<DrawKeyValueItemEventArgs>? PostDrawValueItem;
    event EventHandler<DrawKeyValueItemIconArgs>? DrawLeftIcon;
    event EventHandler<DrawKeyValueItemIconArgs>? DrawRightIcon;
    int DesiredHeight { get; }
    int ItemHeight { get; }
    Color BorderColor { get; set; }
    Rectangle DefBoundsTextKey();
    Rectangle DefBoundsTextValue();
    Rectangle DefBounds();
    Rectangle DefBoundsValue();
    Rectangle DefBoundsKey();
    Rectangle DefLeftBoundsIcon(int index);
    Rectangle DefRightBoundsIcon(int index);
        
    void OnDrawKeyValueItem(DrawKeyValueItemEventArgs e);
    void OnDrawKeyItem(DrawKeyValueItemEventArgs e);
    void OnDrawValueItem(DrawKeyValueItemEventArgs e);

    void OnDrawLeftIcon(DrawKeyValueItemEventArgs e,int i);
    void OnDrawRightIcon(DrawKeyValueItemEventArgs e,int i);
    //void OnDrawItem(DrawItemEventArgs e);
}