using System;
using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters;
internal sealed class EnumFormatter<T> : SharedFormatter<T, EnumFormatter<T>>
{
    private readonly Dictionary<T, string> _valueNameMap;
    private readonly Dictionary<string, T> _nameValueMap;

    public EnumFormatter()
    {
        var enums = Enum.GetValues(typeof(T));
        _valueNameMap = new(enums.Length);
        _nameValueMap = new(enums.Length);
        for (var i = 0;i < enums.Length; i++)
        {
            var enumObject = enums.GetValue(i);
            var enumString = enumObject.ToString();
            var enumValue = (T)enumObject;
            _valueNameMap.Add(enumValue, enumString);
            _nameValueMap.Add(enumString, enumValue);
        }
    }

    public override void Read(ref JReader reader, out T value)
    {
        var enumString = reader.ReadStringOrNull();
        if (enumString == null)
            throw new InvalidOperationException($"Enum string value should not be null in enum formatter.");

        if (!_nameValueMap.TryGetValue(enumString, out value))
            throw new InvalidOperationException($"Enum string value is not found from enum string in enum formatter. (\"{enumString}\")");
    }

    public override void Write(ref JWriter writer, T value)
    {
        if (!_valueNameMap.TryGetValue(value, out var enumString))
            throw new InvalidOperationException($"Enum value is not valid. ({value.ToString()})");
        writer.Write(enumString);
    }
}
