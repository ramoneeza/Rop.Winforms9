using Rop.Helper;
using System.ComponentModel;

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

    public interface ICanBeKeyValue
    {
        event EventHandler<UpdateItemsEventArgs>? UpdateItems;
        event EventHandler<UpdateItemsEventArgs>? UpdatePreItems;
        event EventHandler<UpdateItemsEventArgs>? UpdatePostItems;
        event EventHandler<OrderItemsEventArgs>? UpdateOrderItems;
        void DoUpdateItems();
        Task DoUpdateItemsAsync();
        void DoOrderItems();
        bool NullKeyIsIndex0 { get; set; }
        string SelectedKey { get; set; }
        bool StopUpdates { get; set; }
        string SelectedKeyString { get; set; }
        int SelectedIntKey { get; set; }
        IKeyValue? SelectedKeyValue { get; }
        int FindKeyIndex(string key);
        int FindStringIndex(string key);
        int FindHashCode(int hashcode);
        bool KeyExists(string key);
        IKeyValue? FindKey(string key);
        IKeyValue? FindIntKey(int key);
        int FindValueIndex(string value);
        IKeyValue? FindValue(string value);
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        bool NoSelectedItem { get; }
        bool SelectedItemIsString(out string? cad);
        bool SelectedItemIsTag(string tag = "<");
        bool SelectedItemIsString(string cad);
        bool SelectedItemIsKeyValue(out IKeyValue? kv);
        void FixKey(string key);
        void FixKey(int key);
        void UnFixKey();
        void CancelUpdate();
    }
}
