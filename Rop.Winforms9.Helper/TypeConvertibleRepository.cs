using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Rop.Winforms9.Helper;

public static class TypeConvertibleRepository
{
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo[]> _dicproperties= new();
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, ConstructorInfo> _dicconstructor= new();
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, string[]> _dicsortedproperties= new();

    public static string[] GetSortedPropertyNames(Type type)
    {
        if (_dicsortedproperties.TryGetValue(type.TypeHandle,out var props)) return props;
        //Comprueba si el tipo contiene el interface IParseableConvertible
        var sortedProperties = type.GetMethod("SortedProperties", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (sortedProperties is null) throw new ArgumentException("TypeNotImplementSortedProperties");
        props=sortedProperties.Invoke(null, null) as string[];
        _dicsortedproperties[type.TypeHandle]=props ?? throw new ArgumentException("ImplementSortedProperties returns null");
        return props;
    }
    public static string[] GetSortedPropertyNames<T>() where T : class, ITypeConvertible<T>
    {
        return GetSortedPropertyNames(typeof(T));
    }
    public static PropertyInfo[] GetProperties(Type type)
    {
        if (_dicproperties.TryGetValue(type.TypeHandle,out var props)) return props;
        var properties= GetSortedPropertyNames(type);
        var allproperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var res = new List<PropertyInfo>();
        foreach (var sortedProperty in properties)
        {
            var property = allproperties.FirstOrDefault(p => p.Name == sortedProperty);
            if (property == null) throw new ArgumentException($"PropertyNotFound: {sortedProperty}");
            res.Add(property);
        }
        props = res.ToArray();
        _dicproperties.TryAdd(type.TypeHandle, props);
        return props;
    }
    public static PropertyInfo[] GetProperties<T>() where T : class, ITypeConvertible<T>
    {
        return GetProperties(typeof(T));
    }
    public static Type[] GetTypes(Type type)
    {
        var props = GetProperties(type);
        return props.Select(p => p.PropertyType).ToArray();
    }
    public static Type[] GetTypes<T>() where T : class, ITypeConvertible<T>
    {
        return GetTypes(typeof(T));
    }
    
    public static ConstructorInfo GetConstructor(Type type)
    {
        if (_dicconstructor.TryGetValue(type.TypeHandle,out var cc)) return cc;
        var types= GetTypes(type);
        var constructors = type.GetConstructors();
        cc= constructors.FirstOrDefault(ci => ci.GetParameters().Select(p => p.ParameterType).SequenceEqual(types));
        if (cc == null) throw new ArgumentException("ConstructorNotFound");
        _dicconstructor[type.TypeHandle] = cc;
        return cc;
    }
    public static ConstructorInfo GetConstructor<T>() where T : class, ITypeConvertible<T>
    {
        return GetConstructor(typeof(T));
    }

    public static object?[] GetValues(Type type,object instance)
    {
        var properties = GetProperties(type);
        var values= properties.Select(p => p.GetValue(instance)).ToArray();
        return values;
    }
    public static object?[] GetValues<T>(T instance) where T : class, ITypeConvertible<T>
    {
        return GetValues(typeof(T),instance);
    }
    public static InstanceDescriptor GetInstanceDescriptor<T>(T value) where T : class, ITypeConvertible<T>
    {
        var values = GetValues(value);
        return new InstanceDescriptor(GetConstructor<T>(), values);
    }
    public static object?[] GetValues<T>(IDictionary propertyValues) where T : class, ITypeConvertible<T>
    {
        var properties = GetSortedPropertyNames<T>();
        var values = properties.Select(p => propertyValues[p]).ToArray();
        return values;
    }
    public static T CreateInstance<T>(object?[] values) where T : class, ITypeConvertible<T>
    {
        var constructor = GetConstructor<T>();
        var c = GetConstructor<T>();
        // construir la instancia con c y values
        var instance = (T)c.Invoke(values);
        return instance;
    }
    public static T CreateInstance<T>(IDictionary propertyValues) where T : class, ITypeConvertible<T>
    {
        var values = GetValues<T>(propertyValues);
        return CreateInstance<T>(values);
    }
}