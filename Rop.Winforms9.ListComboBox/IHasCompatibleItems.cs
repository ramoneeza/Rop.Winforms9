namespace Rop.Winforms9.ListComboBox
{
    public interface IHasCompatibleItems
    {
        CompatibleItems Items { get; }
        // ReSharper disable once EventNeverSubscribedTo.Global
        // ReSharper disable once EventNeverInvoked.Global
        event EventHandler? SelectedIndexChanged;
        int SelectedIndex { get; set; }
        void RefreshItems();
        public int GetItemsCount()=>Items.Count;
        public object? GetSelectedItem() => GetItem(SelectedIndex);
        public object? GetItem(int item)=> item < 0 || item >= GetItemsCount() ? null : Items[item];
    }
}
