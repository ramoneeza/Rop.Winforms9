using System.Collections.Concurrent;

namespace Rop.Winforms9.Helper;

public static class FullNameHelper
{
    private static readonly ConcurrentDictionary<Control, string> _dic = new ConcurrentDictionary<Control, string>();
    public static string GetFullName(this Control control)
    {
        if (!_dic.TryGetValue(control, out string? value))
        {
            value = (control.Parent == null) ? control.Name : (control.Parent.GetFullName() + "." + control.Name);
            _dic[control] = value;
        }
        return value;
    }
}