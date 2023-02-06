using System.Collections.Generic;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public abstract class GenericDictionaryFormatter<TKey, TValue, TDictionary, TFormatter> :
    SharedFormatter<TDictionary, TFormatter>
    where TDictionary : class, IDictionary<TKey, TValue>, new()
    where TFormatter : class, IFormatter<TDictionary>, new()
{
    const string _key = "@k";
    const string _value = "@v";

    public override void Read(ref JReader reader, out TDictionary? value)
    {
        if (!reader.ReadArray())
        {
            value = null;
            return;
        }

        value = new TDictionary();
        while (reader.MoveNextArrayItem())
        {
            if (!reader.ReadObject())
                throw new System.Exception("Can't read Dictionary property. property is not an JObject.");

            if (!reader.MoveNextObjectProperty())
                throw new System.Exception($"{_key} is not found.");

            var keyName = reader.ReadPropertyName();
            if (reader.ReadPropertyName() != _key)
                throw new System.Exception($"{_key} name is not match. ({keyName})");

            Formatter<TKey>.Shared.Read(ref reader, out var propertyKey);

            if (!reader.MoveNextObjectProperty())
                throw new System.Exception("Property value is not found.");

            var valueName = reader.ReadPropertyName();
            if (reader.ReadPropertyName() != _value)
                throw new System.Exception($"{_value} name is not match. ({valueName})");

            Formatter<TValue>.Shared.Read(ref reader, out var propertyValue);

            value.Add(propertyKey, propertyValue);

            if (reader.MoveNextObjectProperty())
                throw new System.Exception("Can't read Dictionary property. property should have only 2 properties.");
        }
    }

    public override void Write(ref JWriter writer, TDictionary? value)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        if (value.Count == 0)
        {
            writer.WriteEmptyArray();
            return;
        }

        bool first = false;
        writer.WriteArrayStart();
        foreach (var kv in value)
        {
            if (!first)
                first = true;
            else
                writer.WriteComma();

            writer.WriteObjectStart();
            writer.WritePropertyName(_key);
            Formatter<TKey>.Shared.Write(ref writer, kv.Key);
            writer.WriteComma();
            writer.WritePropertyName(_value);
            Formatter<TValue>.Shared.Write(ref writer, kv.Value);
            writer.WriteObjectEnd();

        }
        writer.WriteArrayEnd();
    }
}
