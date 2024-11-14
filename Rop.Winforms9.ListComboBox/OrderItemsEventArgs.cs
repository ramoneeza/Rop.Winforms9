namespace Rop.Winforms9.ListComboBox
{
    public class OrderItemsEventArgs : EventArgs
    {
        public IReadOnlyList<object> Items { get;private set; }
        public OrderItemsEventArgs(IEnumerable<object>? items = null)
        {
            Items = items?.ToList<object>() ?? [];
        }
        public IEnumerable<T> GetItems<T>() => Items.OfType<T>().ToList();
        public void SetItems<T>(IEnumerable<T> items) => Items = items.Cast<object>().ToList();
    }
}