using System.Collections;

namespace Rop.Winforms9.ListComboBox
{
    public class CompatibleItems : /*IList<object>,*/ IList,IEnumerable<object>
    {
        public IList BaseItems { get; private set; }
        private readonly Action<IEnumerable> _addrange;
        public CompatibleItems(ComboBox owner) : this(owner.Items) { }
        public CompatibleItems(ListBox owner) : this(owner.Items) { }

        public CompatibleItems(IList items)
        {
            BaseItems = items;
            switch (items)
            {
                case ComboBox.ObjectCollection cb:
                    _addrange = ai => cb.AddRange(ai.Cast<object>().ToArray());
                    break;
                case ListBox.ObjectCollection lb:
                    _addrange = ai => lb.AddRange(ai.Cast<object>().ToArray());
                    break;
                default:
                    _addrange = ai => throw new NotImplementedException("AddRange not allowed");
                    break;
            }
        }

        #region Implementation of IEnumerable

        // ReSharper disable once NotDisposedResourceIsReturned
        IEnumerator<object> IEnumerable<object>.GetEnumerator() => BaseItems.Cast<object>().GetEnumerator();

        // ReSharper disable once NotDisposedResourceIsReturned
        public IEnumerator GetEnumerator() => BaseItems.GetEnumerator();

        #endregion

        #region Implementation of ICollection

        public void CopyTo(Array array, int index) => BaseItems.CopyTo(array, index);

        public int Count => BaseItems.Count;
        public object SyncRoot => BaseItems.SyncRoot;
        public bool IsSynchronized => BaseItems.IsSynchronized;

        #endregion

        #region Implementation of IList

        public void Add(object value) => BaseItems.Add(value);

        public void AddRange(IEnumerable values) => _addrange(values);
        public bool Contains(object? value) => BaseItems.Contains(value);
        public void CopyTo(object[] array, int arrayIndex) => BaseItems.CopyTo(array, arrayIndex);

        public void Clear() => BaseItems.Clear();

        public int IndexOf(object? value) => BaseItems.IndexOf(value);

        public void Insert(int index, object? value) => BaseItems.Insert(index, value);

        public bool Remove(object? value)
        {
            BaseItems.Remove(value);
            return true;
        }

        public void RemoveAt(int index) => BaseItems.RemoveAt(index);

        public object? this[int index] { get => BaseItems[index]; set => BaseItems[index] = value; }

        public bool IsReadOnly => BaseItems.IsReadOnly;
        public bool IsFixedSize => BaseItems.IsFixedSize;

        #region Implementation of IList

        int IList.Add(object? value) => BaseItems.Add(value);

        void IList.Remove(object? value) => BaseItems.Remove(value);

        #endregion

        #endregion
    }

}
