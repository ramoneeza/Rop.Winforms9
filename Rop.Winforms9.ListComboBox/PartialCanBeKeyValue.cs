using Rop.Helper;
using Rop.IncludeFrom.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rop.Winforms9.ListComboBox;

[DesignerCategory("Code")]
internal partial class DummyIHasCompatibleItems : Control, IHasCompatibleItems
{
    
    public event EventHandler? SelectedIndexChanged;
    public CompatibleItems Items => default!;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex { get; set; }
    public void RefreshItems() { }
    public virtual void OnDrawItem() { }

    public void BeginUpdate()
    {
    }
    public void EndUpdate()
    {
    }

}
[DesignerCategory("Code")]
[DummyPartial]
internal partial class PartialCanBeKeyValue : DummyIHasCompatibleItems,ICanBeKeyValue
{
    public event EventHandler<UpdateItemsEventArgs>? UpdateItems;
    public event EventHandler<UpdateItemsEventArgs>? UpdatePreItems;
    public event EventHandler<UpdateItemsEventArgs>? UpdatePostItems;
    public event EventHandler<OrderItemsEventArgs>? UpdateOrderItems;
    private volatile bool _cancel;
    public void CancelUpdate() => _cancel = true;
    public bool Cancelled => _cancel;
    public virtual void DoUpdateItems()
    {
        if (StopUpdates) return;
        OnUpdateItems();
    }
    public virtual async Task DoUpdateItemsAsync()
    {
        if (StopUpdates) return;
        await OnUpdateItemsAsync();
    }
    public virtual void DoOrderItems()
    {
        if (StopUpdates) return;
        OnUpdateOrderItems();
    }
    public virtual void OnUpdateOrderItems()
    {
        if (UpdateOrderItems == null) return;
        BeginUpdate();
        var a = SelectedKeyString;
        var items = Items.Where(o => !_preitems.Contains(o) && !_postitems.Contains(o)).ToList();
        Items.Clear();
        var arg = new OrderItemsEventArgs(items);
        OnUpdateOrderItems(arg);
        Items.AddRange(_preitems);
        Items.AddRange(arg.Items);
        Items.AddRange(_postitems);
        SelectedKeyString = a;
        EndUpdate();
        Invalidate();
    }
    public virtual void OnUpdateOrderItems(OrderItemsEventArgs arg)
    {
        UpdateOrderItems?.Invoke(this, arg);
    }
    private List<object> _preitems = new List<object>();
    private List<object> _postitems = new List<object>();
    private static bool _isRealDesignerMode(Control? ctrl)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
        while (ctrl != null)
        {
            if (ctrl.Site is { DesignMode: true }) return true;
            ctrl = ctrl.Parent;
        }
        return System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
    }
    public virtual void OnUpdateItems()
    {
        if (_isRealDesignerMode(this)) return;
        var a = SelectedKeyString;
        Items.Clear();
        var apre = new UpdateItemsEventArgs(a);
        OnUpdatePreItems(apre);
        _preitems = apre.Items;
        a = apre.KeyString;
        var args = new UpdateItemsEventArgs(a);
        OnUpdateItems(args);
        if (UpdateOrderItems != null)
        {
            var argu = new OrderItemsEventArgs(args.Items);
            UpdateOrderItems?.Invoke(this, argu);
            args.Items = (List<object>)argu.Items;
        }
        a = apre.KeyString;
        var argpost = new UpdateItemsEventArgs(a);
        OnUpdatePostItems(argpost);
        _postitems = argpost.Items;
        a = argpost.KeyString;
        Items.AddRange(_preitems);
        Items.AddRange(args.Items);
        Items.AddRange(_postitems);
        SelectedKeyString = a;
        RefreshItems();
    }
    public virtual async Task OnUpdateItemsAsync()
    {
        if (_isRealDesignerMode(this)) return;
        var a = SelectedKeyString;
        Items.Clear();
        var apre = new UpdateItemsEventArgs(a);
        await Task.Run(() => OnUpdatePreItems(apre));
        _preitems = apre.Items;
        a = apre.KeyString;
        var args = new UpdateItemsEventArgs(a);
        await Task.Run(() => OnUpdateItems(args));
        if (UpdateOrderItems != null)
        {
            var argu = new OrderItemsEventArgs(args.Items);
            UpdateOrderItems?.Invoke(this, argu);
            args.Items = (List<object>)argu.Items;
        }
        a = apre.KeyString;
        var argpost = new UpdateItemsEventArgs(a);
        await Task.Run(() => OnUpdatePostItems(argpost));
        _postitems = argpost.Items;
        BeginUpdate();
        a = argpost.KeyString;
        Items.AddRange(_preitems);
        Items.AddRange(args.Items);
        Items.AddRange(_postitems);
        SelectedKeyString = a;
        EndUpdate();
        RefreshItems();
    }
    public virtual void OnUpdateItems(UpdateItemsEventArgs args)
    {
        UpdateItems?.Invoke(this, args);
    }
    public virtual void OnUpdatePostItems(UpdateItemsEventArgs e)
    {
        UpdatePostItems?.Invoke(this, e);
    }
    public virtual void OnUpdatePreItems(UpdateItemsEventArgs e)
    {
        UpdatePreItems?.Invoke(this, e);
    }
    
    [Browsable(true)]
    [DefaultValue(false)]
    public bool NullKeyIsIndex0 { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool StopUpdates { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedKey
    {
        get => SelectedIndex < 0 ? "" : (Items[SelectedIndex] as IKeyValue)?.GetKey() ?? "";
        set
        {
            var i = FindKeyIndex(value);
            if (i >= 0)
                SelectedIndex = i;
            else
                _setNullIndex();
        }
    }
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedKeyString
    {
        get
        {
            if (SelectedIndex < 0) return "";
            var o = Items[SelectedIndex];
            switch (o)
            {
                case IKeyValue kv:
                    return kv.GetKey();
                case string s:
                    return $"!{s}";
                case null:
                    return "";
                default:
                    return $"!#{o?.GetHashCode()}";
            }
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _setNullIndex();
                return;
            }

            if (value.StartsWith("!#"))
            {
                var h =int.Parse(value.Substring(2));
                var i=FindHashCode(h);
                if (i >= 0)
                    SelectedIndex = i;
                else
                    _setNullIndex();
                return;
            }
            if (value[0] != '!')
            {
                var i = FindKeyIndex(value);
                if (i >= 0)
                    SelectedIndex = i;
                else
                    _setNullIndex();
            }
            else
            {
                var i = FindStringIndex(value);
                if (i >= 0)
                    SelectedIndex = i;
                else
                    _setNullIndex();
            }
        }
    }

    public int FindHashCode(int i)
    {
        for (var f = 0; f < Items.Count; f++)
        {
            if (Items[f]?.GetHashCode()==i) return f;
        }
        return -1;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIntKey
    {
        get => int.TryParse(SelectedKey, out int r) ? r : -1;
        set
        {
            if (value < 0)
                _setNullIndex();
            else
                SelectedKey = value.ToString();
        }
    }

//    [Browsable(false)]
//    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
//    public new string SelectedValue
//#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
//    {
//        get
//        {
//            if (NoSelectedItem) return "";
//            var v = Items[SelectedIndex];
//            return v is IKeyValue kv ? kv.GetValue() : v?.ToString() ?? "";
//        }
//    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IKeyValue? SelectedKeyValue
    {
        get
        {
            if (NoSelectedItem) return null;
            return Items[SelectedIndex] as IKeyValue;
        }
    }

    public int FindKeyIndex(string key)
    {
        if (string.IsNullOrEmpty(key)) return -1;
        for (var f = 0; f < Items.Count; f++)
        {
            if (Items[f] is IKeyValue item && item.GetKey() == key) return f;
        }

        return -1;
    }
    public int FindStringIndex(string key)
    {
        if (string.IsNullOrEmpty(key)) return -1;
        if (key[0] == '!') key = key.Substring(1);
        for (var f = 0; f < Items.Count; f++)
        {
            if (Items[f] is string item && item.Equals(key, StringComparison.CurrentCultureIgnoreCase)) return f;
        }
        return -1;
    }
    public bool KeyExists(string key) => FindKeyIndex(key) >= 0;
    public IKeyValue? FindKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return null;
        return Items.OfType<IKeyValue>().FirstOrDefault(item => item.GetKey() == key);
    }

    public IKeyValue? FindIntKey(int key)
    {
        if (key < 0) return null;
        return FindKey(key.ToString());
    }

    public int FindValueIndex(string value)
    {
        for (var f = 0; f < Items.Count; f++)
        {
            if (Items[f] is IKeyValue item && item.GetValue() == value) return f;
        }
        return -1;
    }

    public IKeyValue? FindValue(string value) => Items.OfType<IKeyValue>()
        .FirstOrDefault(item => item.GetValue() == value);

    [Browsable(false)]
    public bool NoSelectedItem => SelectedIndex < 0;
    public bool SelectedItemIsString(out string cad)
    {
        if (NoSelectedItem)
        {
            cad = "";
            return false;
        }

        cad = Items[SelectedIndex] as string ?? "";
        return !string.IsNullOrEmpty(cad);
    }

    public bool SelectedItemIsString() => SelectedItemIsString(out string _);

    public bool SelectedItemIsString(string cad) =>
        SelectedItemIsString(out string c) && c.Equals(cad, StringComparison.CurrentCultureIgnoreCase);

    public bool SelectedItemIsTag(out string tag)
    {
        tag = SelectedItemIsString(out string cad) && cad.StartsWith("<") ? cad : "";
        return !string.IsNullOrEmpty(tag);
    }

    public bool SelectedItemIsTag(string tag) => tag.StartsWith("<") && SelectedItemIsString(tag);

    public bool SelectedItemIsTag() => SelectedItemIsTag(out string _);

    public bool SelectedItemIsKeyValue(out IKeyValue? kv)
    {
        kv = SelectedKeyValue;
        return kv != null;
    }

    private void _setNullIndex()
    {
        if (Items.Count == 0)
            SelectedIndex = -1;
        else
            SelectedIndex = NullKeyIsIndex0 ? 0 : -1;
    }
    public void FixKey(string key)
    {
        SelectedKey = key;
        Enabled = false;
    }

    public void FixKey(int key)
    {
        SelectedIntKey = key;
        Enabled = false;
    }

    public void UnFixKey()
    {
        Enabled = true;
    }
}