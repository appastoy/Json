using System.Collections.Generic;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public abstract class GenericStringDictionaryFormatter<TValue, TDictionary, TFormatter> :
    SharedFormatter<TDictionary, TFormatter>
    where TDictionary : class, IDictionary<string, TValue>, new()
    where TFormatter : class, IFormatter<TDictionary>, new()
{
    public override void Read(ref JReader reader, out TDictionary? value)
    {
        if (!reader.ReadObject())
        {
            value = null;
            return;
        }

        value = new TDictionary();
        while (reader.MoveNextObjectProperty())
        {
            var propertyName = reader.ReadPropertyName();
            Formatter<TValue>.Shared.Read(ref reader, out var propertyValue);
            value.Add(propertyName, propertyValue);
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
            writer.WriteEmptyObject();
            return;
        }

        bool first = false;
        writer.WriteObjectStart();
        foreach (var kv in value)
        {
            if (!first)
                first = true;
            else
                writer.WriteComma();
            writer.WritePropertyName(kv.Key);
            Formatter<TValue>.Shared.Write(ref writer, kv.Value);
        }
        writer.WriteObjectEnd();
    }
}
