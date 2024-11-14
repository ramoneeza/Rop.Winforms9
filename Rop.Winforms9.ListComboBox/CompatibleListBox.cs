using System.ComponentModel;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.Shared;

namespace Rop.Winforms9.ListComboBox
{
    /// <summary>
    /// Create Listbox with compatible Item Collection
    /// </summary>
    [DesignerCategory("Code")]
    [IncludeFrom(typeof(PartialControlWithOnLoad))]
    [IncludeFrom(typeof(PartialCanBeKeyValue))]
    public partial class CompatibleListBox : ListBox,IHasCompatibleItems,IControlWithOnLoad,ICanBeKeyValue
    {
        public CompatibleListBox()
        {
        }
        public new CompatibleItems Items
        {
            get
            {
                if ((_items == null) || (!ReferenceEquals(_items?.BaseItems, base.Items))) _items = new CompatibleItems(base.Items);
                return _items;
            }
        }
        [NonSerialized]
        private CompatibleItems? _items;

        void IHasCompatibleItems.RefreshItems()
        {
            base.RefreshItems();
        }
        public int GetItemsCount() => Items.Count;
        public object? GetSelectedItem()=> (SelectedIndex < 0 || SelectedIndex >= Items.Count) ? null : Items[SelectedIndex];
    }
}