using System.ComponentModel;

namespace Rop.Winforms9.Helper;

public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor? _sortProperty;
    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;
    protected override ListSortDirection SortDirectionCore => _sortDirection;
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        _sortProperty = prop;
        _sortDirection = direction;
        List<T> list = (List<T>)Items;
        list.Sort(_compare);
        _isSorted = true;
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }
    private int _compare(T lhs, T rhs)
    {
        int result = OnComparison(lhs, rhs);
        if (_sortDirection == ListSortDirection.Descending)
        {
            result = -result;
        }
        return result;
    }
    private int OnComparison(T lhs, T rhs)
    {
        object? lhsValue = lhs == null ? null : _sortProperty?.GetValue(lhs);
        object? rhsValue = rhs == null ? null : _sortProperty?.GetValue(rhs);
        if (lhsValue is null && rhsValue is null) return 0;
        if (lhsValue is null || rhsValue is null)
        {
            return (rhsValue is null) ? 1 : -1;
        }
        if (lhsValue is IComparable lhsComparable)
        {
            return lhsComparable.CompareTo(rhsValue);
        }
        if (rhsValue is IComparable rhsComparable)
        {
            return -rhsComparable.CompareTo(lhsValue);
        }
        return lhsValue.Equals(rhsValue) ? 0 : string.Compare(lhsValue.ToString(), rhsValue.ToString(), StringComparison.Ordinal);
    }
    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }
    public SortableBindingList()
    {
    }
    public SortableBindingList(IList<T> list) : base(list)
    {
    }
    public SortableBindingList(IEnumerable<T> list) : base(list.ToList())
    {
    }
}