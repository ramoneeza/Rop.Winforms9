using System.ComponentModel;
using System.Reflection;
using Rop.IncludeFrom.Annotations;
using Rop.Winforms9.Shared;

namespace Rop.Winforms9.ListComboBox;

[DesignerCategory("Code")]
[IncludeFrom(typeof(PartialControlWithOnLoad))]
[IncludeFrom(typeof(PartialCanBeKeyValue))]
public partial class CompatibleComboBox : ComboBox, IHasCompatibleItems,IControlWithOnLoad,ICanBeKeyValue
{
    [NonSerialized]
    private CompatibleItems _items=default!;
    public CompatibleComboBox() : base()
    {
        var f = 
            typeof(ComboBox).GetField("_itemsCollection", BindingFlags.Instance | BindingFlags.NonPublic)
            ??
            typeof(ComboBox).GetField("itemsCollection", BindingFlags.Instance | BindingFlags.NonPublic);
        if (f is null) throw new MissingFieldException("ComboBox", "_itemsCollection or itemsCollection");
        // ReSharper disable once VirtualMemberCallInConstructor
        f.SetValue(this, CreateItemCollection());
    }
    protected virtual ComboBox.ObjectCollection CreateItemCollection()
    {
        var oc = base.Items;
        _items = new CompatibleItems(oc);
        return oc;
    }
    public new CompatibleItems Items
    {
        get
        {
            var baseitems = base.Items;
            if (!ReferenceEquals(_items.BaseItems, baseitems)) _items = new CompatibleItems(baseitems);
            return _items;
        }
    }
    void IHasCompatibleItems.RefreshItems()
    {
        base.RefreshItems();
    }
    public int GetItemsCount()=> Items.Count;
    public object? GetSelectedItem()=>(SelectedIndex<0||SelectedIndex >= Items.Count) ? null : Items[SelectedIndex];
}