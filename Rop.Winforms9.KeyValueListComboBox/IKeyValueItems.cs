using System.ComponentModel;
using Rop.Helper;
using Rop.Winforms9.ListComboBox;

namespace Rop.Winforms9.KeyValueListComboBox
{
    public interface IKeyValueItems:IHasCompatibleItems,ICanBeKeyValue
    {
        string SelectedValue { get; }
    }
}