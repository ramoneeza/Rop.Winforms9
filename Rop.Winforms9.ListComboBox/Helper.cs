using System.Collections;
using Rop.Helper;

namespace Rop.Winforms9.ListComboBox
{
    public static partial class Helper
	{
        public static CompatibleItems GetItemsCollection(this IHasCompatibleItems lstcontrol) => lstcontrol.Items;
        public static IList GetItemsCollection(this ListControl lstcontrol)
        {
            return lstcontrol switch
            {
                IHasCompatibleItems hci => hci.Items,
                ComboBox cb => cb.Items,
                ListBox lb => lb.Items,
                _ => throw new ArgumentException("Not ListBox or ComboBox")
            };
        }
        public static ListControl CastListControl(this IHasCompatibleItems lstcontrol)
        {
            return lstcontrol switch
            {
                ComboBox cb => cb,
                ListBox lb => lb,
                _ => throw new ArgumentException("Not ListBox or ComboBox")
            };
        }
        public static object[] ToObjectArray<T>(this IEnumerable<T> elements) => elements.Cast<object>().ToArray();

        public static void AddRange<T>(this ComboBox.ObjectCollection col, IEnumerable<T> elements) => _addRange(col, elements);
        public static void AddRange<T>(this ListBox.ObjectCollection col, IEnumerable<T> elements) => _addRange(col, elements);
        public static void AddRange<T>(this IHasCompatibleItems col, IEnumerable<T> elements) => _addRange(col, elements);
        public static void AddRange<T>(this CompatibleItems col, IEnumerable<T> elements) => _addRange(col, elements);
        public static void AddRange<T>(this ListControl col, IEnumerable<T> elements) => _addRange(col, elements);
        
        public static void AddRange<T>(this ComboBox.ObjectCollection col, params T[] elements) => _addRange(col, elements);
        public static void AddRange<T>(this ListBox.ObjectCollection col, params T[] elements) => _addRange(col, elements);
        public static void AddRange<T>(this IHasCompatibleItems col, params T[] elements) => _addRange(col, elements);
        public static void AddRange<T>(this CompatibleItems col, params T[] elements) => _addRange(col, elements);
        public static void AddRange<T>(this ListControl col, params T[] elements) => _addRange(col, elements);
        
        
        private static void _addRange<T>(object col, IEnumerable<T> elements)
        {
            object[] items;
            switch (elements)
            {
               case IEnumerable<(string, string)> atstr:
                   items = atstr.Select(t => new FooKeyValue(t)).ToObjectArray();
                   break;
               case IEnumerable<(int, string)> atstr:
                   items = atstr.Select(t => new FooIntKeyValue(t.Item1,t.Item2)).ToObjectArray();
                   break;
               default:
                   items = elements.ToObjectArray();
                   break;
            }

            switch (col)
            {
                case ComboBox cb:
                    cb.Items.AddRange(items);
                    break;
                case ListBox lb:
                    lb.Items.AddRange(items);
                    break;
                case IHasCompatibleItems hci:
                    hci.Items.AddRange(items);
                    break;
                case ComboBox.ObjectCollection cbo:
                    cbo.AddRange(items);
                    break;
                case ListBox.ObjectCollection lbo:
                    lbo.AddRange(items);
                    break;
                case CompatibleItems ci:
                    ci.AddRange(items);
                    break;
                default:
                    throw new ArgumentException("Not ListBox or ComboBox");
            }
        }
        
        public static IEnumerable<T> OfType<T>(ComboBox.ObjectCollection col) => _ofType<T>(col);
        public static IEnumerable<T> OfType<T>(ListBox.ObjectCollection col) => _ofType<T>(col);
        public static IEnumerable<T> OfType<T>(CompatibleItems col) => _ofType<T>(col);
        private static IEnumerable<T> _ofType<T>(object col)
        {
            switch (col)
            {
                case ComboBox.ObjectCollection cb:
                    return cb.OfType<T>();
                case ListBox.ObjectCollection lb:
                    return lb.OfType<T>();
                case CompatibleItems ci:
                    return ci.OfType<T>();
                default:
                    throw new ArgumentException("Not ListBox or ComboBox");
            }
        }
        public static int GetItemsCount(this ListControl lstcontrol)
        {
            var items = GetItemsCollection(lstcontrol);
            return items.Count;
        }
        //public static int GetItemsCount(this IHasCompatibleItems lstcontrol) =>GetItemsCount(lstcontrol.CastListControl());

        public static object? GetItemAt(this ListControl lstcontrol, int index)
        {
            var items = GetItemsCollection(lstcontrol);
            if (index < 0 || index >= items.Count) return null;
            return items[index];
        }
        //public static object GetItemAt(this IHasCompatibleItems lstcontrol, int index) =>
        //    GetItemAt(lstcontrol.CastListControl(), index);


        public static IKeyValue? GetKeyValueItem(this ListControl lstcontrol, int index)
		{
			return GetItemAt(lstcontrol,index) as IKeyValue;
        }
        public static IIntKeyValue? GetIntKeyValueItem(this ListControl lstcontrol, int index)
        {
            return GetItemAt(lstcontrol,index) as IIntKeyValue;
        }
        
        public static int GetSelectedIndex(this ListControl lstcontrol)
        {
            return lstcontrol.SelectedIndex;
        }

        //public static int GetSelectedIndex(this IHasCompatibleItems lstcontrol) => lstcontrol.SelectedIndex;


        public static IKeyValue? GetKeyValueSelectedItem(this ListControl lstcontrol)
        {
            return GetKeyValueItem(lstcontrol,GetSelectedIndex(lstcontrol));
        }
        public static string GetKeySelectedItem(this ListControl lstcontrol)
        {
            return GetKeyValueSelectedItem(lstcontrol)?.GetKey() ?? "";
        }
        public static IIntKeyValue? GetIntKeyValueSelectedItem(this ListControl lstcontrol)
        {
            return GetIntKeyValueItem(lstcontrol,GetSelectedIndex(lstcontrol));
        }
        public static int GetIntKeySelectedItem(this ListControl lstcontrol)
        {
            return GetIntKeyValueItem(lstcontrol,GetSelectedIndex(lstcontrol))?.GetIntKey()??-1;
        }
        
        public static int FindKeyValueIndex(this ListControl lstcontrol, string key,bool nocase=false)
		{
			var items = GetItemsCollection(lstcontrol);
			var strc=nocase?StringComparison.Ordinal:StringComparison.OrdinalIgnoreCase;
			for (var f = 0; f < items.Count; f++)
			{
                if (items[f] is not IKeyValue i) continue;
                if (i.GetKey().Equals(key,strc)) return f;
			}
			return -1;
		}
        public static int FindIntKeyValueIndex(this ListControl lstcontrol, int key)
        {
            var items = GetItemsCollection(lstcontrol);
            for (var f = 0; f < items.Count; f++)
            {
                if (items[f] is not IIntKeyValue i) continue;
                if (i.GetIntKey().Equals(key)) return f;
            }
            return -1;
        }
		public static IKeyValue? FindByKey(this ListControl lstcontrol, string key,bool nocase=false)
		{
			var items = GetItemsCollection(lstcontrol);
            var strc = nocase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            foreach (var t in items)
            {
                if (t is IKeyValue i && i.GetKey().Equals(key, strc)) return i;
            }
			return null;
		}
        public static IIntKeyValue? FindByIntKey(this ListControl lstcontrol, int key)
        {
            var items = GetItemsCollection(lstcontrol);
            foreach (var t in items)
            {
                if (t is IIntKeyValue i && i.GetIntKey() == key) return i;
            }
            return null;
        }
        public static void SetSelectedIndex(this ListControl lstcontrol, int newindex)
        {
            lstcontrol.SelectedIndex=newindex;
        }
        public static int SelectKey(this ListControl lstcontrol, string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				SetSelectedIndex(lstcontrol,-1);
				return -1;
			}
			var i = FindKeyValueIndex(lstcontrol,key);
			SetSelectedIndex(lstcontrol,i);
			return i;
		}
        public static int SelectIntKey(this ListControl lstcontrol, int key)
        {
            if (key<0)
            {
                SetSelectedIndex(lstcontrol,-1);
                return -1;
            }
            var i = FindIntKeyValueIndex(lstcontrol,key);
            SetSelectedIndex(lstcontrol,i);
            return i;
        }
        
	}
}