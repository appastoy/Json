using AppAsToy.Json.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppAsToy.Json.Conversion.Formatters;
internal sealed class TypeFormatter<T> : SharedFormatter<T, TypeFormatter<T>>
{
    private readonly FieldInfo[] _fields;
    private readonly Dictionary<string, FieldInfo> _fieldMap;

    private readonly PropertyInfo[] _propertyGetters;
    private readonly Dictionary<string, PropertyInfo> _propertyGetterMap;

    private readonly PropertyInfo[] _propertySetters;
    private readonly Dictionary<string, PropertyInfo> _propertySetterMap;

    public TypeFormatter()
    {
        var type = typeof(T);
        _fields = type.EnumerateWithBaseTypesReverse()
            .SelectMany(t => t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            .ToArray();
        _fieldMap = _fields.ToDictionary(f => f.Name);

        var nonPublicProperties = type.EnumerateWithBaseTypesReverse()
            .SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            .Where(p => p.GetIndexParameters()?.Length == 0)
            .ToList();
        var publicProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        _propertyGetters = GetPropertyMethods(nonPublicProperties, publicProperties, (p, nonPublic) => p.GetGetMethod(nonPublic));
        _propertyGetterMap = _propertyGetters.ToDictionary(p => p.Name);

        _propertySetters = GetPropertyMethods(nonPublicProperties, publicProperties, (p, nonPublic) => p.GetSetMethod(nonPublic)); ;
        _propertySetterMap = _propertySetters.ToDictionary(p => p.Name);

        static PropertyInfo[] GetPropertyMethods(List<PropertyInfo> nonPublicProperties, PropertyInfo[] typeProperties, Func<PropertyInfo, bool, MethodInfo> propertyMethodFunc)
        {
            return nonPublicProperties.Where(p =>
            {
                var m = propertyMethodFunc(p, true);
                return m != null &&
                    (m.IsPrivate ||
                        !m.IsVirtual);
            })
            .Concat(typeProperties
                .Where(p =>
                {
                    var m = propertyMethodFunc(p, false) ?? propertyMethodFunc(p, true);
                    return m != null &&
                            !m.IsPrivate &&
                            m.IsVirtual &&
                            !m.IsFinal;
                }))
            .ToArray();
        }
    }


    public override void Read(ref JReader reader, out T value)
    {
        throw new NotImplementedException();
    }

    public override void Write(ref JWriter writer, T value)
    {
        throw new NotImplementedException();
    }
}
