using System.ComponentModel;
using Rop.Helper;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.KeyValueListComboBox
{
    public interface IKeyValueItems:IHasCompatibleItems
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
        string SelectedValue { get; }
        IKeyValue? SelectedKeyValue { get; }
        int FindKeyIndex(string key);
        int FindStringIndex(string key);
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