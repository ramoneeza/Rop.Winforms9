using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Rop.Winforms9.Decorators
{
    public class EnumComboboxView<T> where T:struct,Enum
    {
        public ComboBox ComboBox { get; }
        public IReadOnlyList<string> DisplayValues { get; }

        public EnumComboboxView(ComboBox comboBox)
        {
            ComboBox = comboBox;
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            var values = Enum.GetValues<T>();
            DisplayValues = values.Select(InitialGetDisplayValue).ToArray();
            ComboBox.DataSource = values;
            ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            ComboBox.DrawItem += ComboBox_drawItem;
            return;

            //Locals
            string InitialGetDisplayValue(T value)
            {
                var m =
                    typeof(T).GetMember(value.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName() ??
                    value.ToString();
                return m;
            }
        }

        private void ComboBox_drawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            string nombre;
            var item = ComboBox.Items[e.Index];
            nombre = item switch
            {
                T value => DisplayValues[Convert.ToInt32(value)],
                string s => s,
                _ => item?.ToString() ?? ""
            };
            e.DrawBackground();
            using SolidBrush brush = new SolidBrush(e.ForeColor);
            e.Graphics.DrawString(nombre, e.Font??ComboBox.Font, brush, e.Bounds);
            e.DrawFocusRectangle();
            
        }
        public T? GetValue()
        {
            if (ComboBox.SelectedIndex < 0 || ComboBox.Items[ComboBox.SelectedIndex] is not T enumvalue) return null;
            return enumvalue;
        }
        public void SetValue(T? value)
        {
            if (value == null)
            {
                ComboBox.SelectedIndex = -1;
                return;
            }
            ComboBox.SelectedIndex = Convert.ToInt32(value);
        }
    }
}
