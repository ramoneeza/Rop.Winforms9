using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Rop.Winforms9.Helper;

public class SlimControlDialogDecorator
{
    public Form SlimControlDialog { get; }
    public Control DecotatedControl { get; }
    public Rectangle DecoratedBounds { get; }
    public Form ParentForm { get; }
    public Size DesiredDialogSize { get; set; }
    public Point DesiredDialogLocation { get; set; }
    public bool FadeOut { get; set; }
    public int FadeOutTime { get; set; } = 2500;
    public int FadeOutStep { get; set; } = 100;
    private float _fadeoutcount = 0;
    private readonly System.Windows.Forms.Timer _timer;
    private MouseHook? _mouseHook;
    public SlimControlDialogDecorator(Form slimControlDialog, Control decotatedControl,Rectangle? decoratedControlBounds=null)
    {
        decoratedControlBounds ??= decotatedControl.Bounds;
        SlimControlDialog = slimControlDialog;
        DecotatedControl = decotatedControl;
        ParentForm = decotatedControl.FindForm()?? throw new ArgumentException("Decorated control has not parent form") ;
        
        DecoratedBounds =decotatedControl.RectangleToScreen(decoratedControlBounds.Value);
        _timer = new System.Windows.Forms.Timer()
        {
            Enabled = false,
            Interval = FadeOutStep
        };
        _timer.Tick += Timer_Tick;
        DesiredDialogLocation = new Point(DecoratedBounds.X, DecoratedBounds.Y + DecoratedBounds.Height);
        DesiredDialogSize = new Size(DecoratedBounds.Width, 200);
        slimControlDialog.StartPosition = FormStartPosition.Manual;
        slimControlDialog.FormBorderStyle = FormBorderStyle.None;
        slimControlDialog.ShowInTaskbar = false;
        slimControlDialog.TopMost = true;
        slimControlDialog.AllowTransparency= true;
        slimControlDialog.Opacity = 1;
        slimControlDialog.Size = DesiredDialogSize;
        slimControlDialog.Shown += SlimControlDialog_Shown;
        slimControlDialog.FormClosing += SlimControlDialog_FormClosing;
        
    }

    private bool _mouseclicked;
    private void MouseHook_MouseActionDown(object? sender, MouseHookArgs e)
    {
        _mouseclicked = true;
        var cursorPos = e.CursorPosition;
        if (!DecoratedBounds.Contains(cursorPos) && !SlimControlDialog.Bounds.Contains(cursorPos))
        {
            e.Handled=true;
        }
    }
    private void MouseHook_MouseActionUp(object? sender, MouseHookArgs e)
    {
        _mouseclicked = false;
    }

    private void SlimControlDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_mouseHook != null)
        {
            _mouseHook.MouseActionDown -= MouseHook_MouseActionDown;
            _mouseHook.MouseActionUp -= MouseHook_MouseActionUp;
            _mouseHook.Dispose();
            _mouseHook = null;
        }
    }

    

    private void SlimControlDialog_Shown(object? sender, EventArgs e)
    {
        SlimControlDialog.Location = DesiredDialogLocation;
        SlimControlDialog.Size = DesiredDialogSize;
        _resetFade();
        _mouseHook = new MouseHook();
        _mouseHook.MouseActionDown += MouseHook_MouseActionDown;
        _mouseHook.MouseActionUp += MouseHook_MouseActionUp;
        _timer.Enabled = true;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        var p= Control.MousePosition;
        if (DecoratedBounds.Contains(p) || SlimControlDialog.Bounds.Contains(p))
            _mouseinside(p);
        else
            _mouseoutside(p);
    }

    private void _mouseoutside(Point point)
    {
        if (_mouseclicked)
        {
            _doclose();
            return;
        }
        if (!FadeOut) return;
        _fadeoutcount -= FadeOutStep;
        if (_fadeoutcount<0) _fadeoutcount = 0;
        if (_fadeoutcount <=0)
        {
            _doclose();
            return;
        }
        SlimControlDialog.Opacity = _fadeoutcount / FadeOutTime;
    }

    private void _doclose()
    {
        _timer.Enabled = false;
        SlimControlDialog.DialogResult = DialogResult.Cancel;
        SlimControlDialog.Close();
    }

    private void _mouseinside(Point point)
    {
        _resetFade();
    }

    private void _resetFade()
    {
        if (SlimControlDialog.IsDisposed) return;
        _fadeoutcount = FadeOutTime;
        SlimControlDialog.Opacity = 1;
    }
}