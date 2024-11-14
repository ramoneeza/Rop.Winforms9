using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Rop.Winforms9.Helper;

public abstract class TypeConverter<T> : TypeConverter where T:class,ITypeConvertible<T>
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string)) return true;
        return base.CanConvertFrom(context, sourceType);
    }
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(InstanceDescriptor)) return true;
        return base.CanConvertTo(context, destinationType);
    }
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {

        if (value is string strValue)
        {
            var text = strValue.Trim();
            return T.Parse(text);
        }
        return base.ConvertFrom(context, culture, value);
    }
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is T pt)
        {
            if (destinationType == typeof(string)) return pt.ToParsableString();
            if (destinationType == typeof(InstanceDescriptor)) return TypeConvertibleRepository.GetInstanceDescriptor(pt);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
    public override object CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
    {
        try
        {
            return TypeConvertibleRepository.CreateInstance<T>(propertyValues);
        }
        catch(Exception ex)
        {
            throw new ArgumentException("PropertyValueInvalidEntry", ex);
        }
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context) => true;
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T), attributes);
        return props.Sort(T.SortedProperties());
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;
}