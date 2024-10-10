using System.Reflection;

namespace Rop.Winforms9.Helper;

public static class Resources
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    private static readonly string Assemblyname = Assembly.GetName().Name??"";
    public static Stream? Get(string name)
    {
        return Assembly.GetManifestResourceStream(Assemblyname + "." + name);
    }
}