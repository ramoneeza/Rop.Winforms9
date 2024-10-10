using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Rop.Winforms9.KeyValueListComboBox.Converters
{
    public class ColorSetConverter : TypeConverter
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
        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {

            if (value is string strValue)
            {
                var text = strValue.Trim();
                if (text.Length == 0)
                    return null;
                else
                    return ColorSet.FromString(text);
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// 
        /// 
        ///      Converts the given object to another type.  The most common types to convert
        ///      are to and from a string object.  The default implementation will make a call 
        ///      to ToString on the object if the object is valid and if the destination
        ///      type is string.  If this cannot convert to the desitnation type, this will 
        ///      throw a NotSupportedException. 
        /// 
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }
            if (value is ColorSet pt)
            {
                if (destinationType == typeof(string))
                {
                    return pt.ToString();
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    ConstructorInfo? ctor = typeof(ColorSet).GetConstructor(new Type[] { typeof(Color), typeof(Color), typeof(Color) });
                    if (ctor != null)
                    {
                        return new InstanceDescriptor(ctor, new object[] { pt.BackColor, pt.ForeColor, pt.KeyColor });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        [SuppressMessage("Microsoft.Security", "CA2102:CatchNonClsCompliantExceptionsInGeneralHandlers")]
        public override object CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException(nameof(propertyValues));
            }
            object? x0 = propertyValues["BackColor"];
            object? x1 = propertyValues["ForeColor"];
            object? x2 = propertyValues["KeyColor"];
            if (!(x0 is Color c0) || !(x1 is Color c1) || !(x2 is Color c2))
            {
                throw new ArgumentException("PropertyValueInvalidEntry");
            }
            return new ColorSet(c0, c1, c2);
        }
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ColorSet), attributes);
            return props.Sort(new string[] { "BackColor", "ForeColor", "KeyColor" });
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
        {
            return true;
        }
    }
}