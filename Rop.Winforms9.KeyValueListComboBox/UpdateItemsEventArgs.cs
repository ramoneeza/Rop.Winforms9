namespace Rop.Winforms9.KeyValueListComboBox
{
    public class UpdateItemsEventArgs : EventArgs
    {
        public string KeyString { get; set; }
        public List<object> Items { get; set; }
        public UpdateItemsEventArgs(string keystring, IEnumerable<object>? items = null)
        {
            KeyString = keystring;
            Items = new List<object>();
            if (items != null) Items.AddRange(items);
        }
    }
}