using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.Helper;

public class MouseHookArgs:EventArgs{
    public MouseButtons MouseButtonsStatus { get; }
    public MouseButtons MouseButtonChanged { get; }
    public Point CursorPosition { get; set; }
    public bool Handled { get; set; }
    public MouseHookArgs(MouseButtons mouseButtonChanged,MouseButtons mouseButtonsStatus,  Point cursorPosition)
    {
        MouseButtonChanged = mouseButtonChanged;
        MouseButtonsStatus = mouseButtonsStatus;
        CursorPosition = cursorPosition;
    }
}
public class MouseHook:IDisposable
{
    public event EventHandler<MouseHookArgs>? MouseActionDown;
    public event EventHandler<MouseHookArgs>? MouseActionUp;
    public event EventHandler<MouseHookArgs>? MouseAction;
    public MouseHook()
    {
        MouseHookService.Register(this);
    }
    public void Dispose()
    {
        MouseHookService.Unregister(this);
    }
    internal void OnMouseDown(MouseHookArgs arg)
    {
        MouseActionDown?.Invoke(this,arg);           
        MouseAction?.Invoke(this, arg);
    }
    internal void OnMouseUp(MouseHookArgs arg)
    {
        MouseActionUp?.Invoke(this, arg);
        MouseAction?.Invoke(this, arg);
    }
}

public static class MouseHookService
{
    private const int WH_MOUSE_LL = 14;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_RBUTTONDOWN = 0x0204;
    private const int WM_MBUTTONDOWN = 0x0207;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_RBUTTONUP = 0x0205;
    private const int WM_MBUTTONUP = 0x0208;
    private static readonly int[] WM_BUTTONS=[WM_LBUTTONDOWN, WM_RBUTTONDOWN, WM_MBUTTONDOWN, WM_LBUTTONUP, WM_RBUTTONUP, WM_MBUTTONUP];

    private static LowLevelMouseProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    
    private static List<MouseHook> _hooks = new List<MouseHook>();

    public static MouseButtons MouseButtons { get; private set; }
    public static bool MouseLButtonClicked=> MouseButtons.HasFlag(MouseButtons.Left);
    public static bool MouseRButtonClicked => MouseButtons.HasFlag(MouseButtons.Right);
    public static bool MouseMButtonClicked => MouseButtons.HasFlag(MouseButtons.Middle);

    
    internal static void Register(MouseHook hook)
    {
        if (_hooks.Count == 0)
        {
            Start();
        }
        _hooks.Add(hook);
    }
    internal static void Unregister(MouseHook hook)
    {
        _hooks.Remove(hook);
        if (_hooks.Count == 0)
        {
            Stop();
        }
    }
    private static void Start()
    {
        _hookID = SetHook(_proc);
    }

    private static void Stop()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private static bool Signal(int wParam)
    {
        var (down,button)=wParam switch
        {
            WM_LBUTTONDOWN => (true,MouseButtons.Left),
            WM_RBUTTONDOWN => (true,MouseButtons.Right),
            WM_MBUTTONDOWN => (true, MouseButtons.Middle),
            WM_LBUTTONUP=>(false, MouseButtons.Left),
            WM_RBUTTONUP=> (false, MouseButtons.Right),
            WM_MBUTTONUP => (false, MouseButtons.Middle),
            _ => (false,MouseButtons.None)
        };
        if (button == MouseButtons.None) return false;
        if (down)
        {
            MouseButtons |= button;
        }
        else
        {
            MouseButtons &= ~button;
        }
        var arg = new MouseHookArgs(button, MouseButtons, Control.MousePosition);
        foreach (var mouseHook in _hooks)
        {
            if (down)
                mouseHook.OnMouseDown(arg);
            else
                mouseHook.OnMouseUp(arg);
        }
        return arg.Handled;
    }
    
    private static IntPtr SetHook(LowLevelMouseProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule??throw new Exception("No MainModule in current process");
        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (WM_BUTTONS.Contains((int)(wParam))))
        {
            var result = Signal((int)wParam);
            if (result) return (IntPtr)1; // Evita que el clic se propague
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}