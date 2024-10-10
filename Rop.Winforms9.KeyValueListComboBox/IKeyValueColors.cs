namespace Rop.Winforms9.KeyValueListComboBox;

public interface IKeyValueColors
{
    ColorSet RowStyleColorSet { get; }
    ColorSet RowStyleAltColorSet { get; }
    ColorSet RowStyleSelectedColorSet { get;  }
    internal ColorSet _getSet(bool selected, int index)
    {
        var vset = RowStyleColorSet;
        var aset=RowStyleAltColorSet.IfEmpty(vset);
        var cset=(index<0 || (index%2==0))? vset : aset;
        if (!selected) return cset;
        return RowStyleSelectedColorSet.IfEmpty(cset);
    }
}

public static class KeyValueColorsHelper
{
    
    public static Color KeyBackColorFinal(this IKeyValueColors ikvc, bool selected, int index,Color defaultbackcolor)
    {
        var cset = ikvc._getSet(selected, index);
        return cset.KeyColorFinal(defaultbackcolor);
    }
    
    
    public static Color ValueBackColorFinal(this IKeyValueColors ikvc,bool selected, int index,Color defaultbackcolor)
    {
        var cset = ikvc._getSet(selected, index);
        return cset.BackColorFinal(defaultbackcolor);
    }
    public static Color ForeColorFinal(this IKeyValueColors ikvc,bool selected,int index,Color defaultforecolor)
    {
        var cset = ikvc._getSet(selected, index);
        return cset.ForeColorFinal(defaultforecolor);
    }
 
    
}