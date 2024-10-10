namespace Rop.Winforms9.KeyValueListComboBox;

public class DrawKeyValueItemIconArgs
{
    public DrawKeyValueItemEventArgs DrawKeyValueItemEventArgs { get; }
    public Graphics Graphics => DrawKeyValueItemEventArgs.Graphics;
    public int IconIndex { get; }
    public Rectangle Bounds { get; }

    public DrawKeyValueItemIconArgs(DrawKeyValueItemEventArgs drawKeyValueItemEventArgs, int iconIndex, Rectangle bounds)
    {
        DrawKeyValueItemEventArgs = drawKeyValueItemEventArgs;
        IconIndex = iconIndex;
        Bounds = bounds;
    }
}