namespace Rop.Winforms9.ListComboBox
{
    public class UpdateItemsEventArgs : EventArgs
    {
        public string KeyString { get; set; }
        public List<object> Items { get; set; }
        public UpdateItemsEventArgs(string keystring, IEnumerable<object>? items = null)
        {
            KeyString = keystring;
            Items = Items?.ToList<object>() ?? [];
        }
    }
}