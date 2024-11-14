namespace Rop.Winforms9.Helper;

public interface ITypeConvertible<out T> where T:class,ITypeConvertible<T>
{
    string ToParsableString();
    public static abstract T? Parse(string text);
    public static abstract string[] SortedProperties();
}