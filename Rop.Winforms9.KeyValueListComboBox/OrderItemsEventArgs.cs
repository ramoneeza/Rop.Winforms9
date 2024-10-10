namespace Rop.Winforms9.KeyValueListComboBox
{
    public class OrderItemsEventArgs : EventArgs
    {
        private List<object> _items;
        public IReadOnlyList<object> Items => _items;
        public OrderItemsEventArgs(IEnumerable<object>? items = null)
        {
            _items = new List<object>();
            if (items != null) _items.AddRange(items);
        }
        public IEnumerable<T> GetItems<T>() => _items.OfType<T>().ToList();
        public void SetItems<T>(IEnumerable<T> items) => _items = items.Cast<object>().ToList();
    }
}