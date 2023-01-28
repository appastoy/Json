using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters;
internal sealed class DictionaryFormatter<T> : 
    SharedFormatter<Dictionary<string, T>, DictionaryFormatter<T>>,
    IFormatter<Dictionary<string, T>>
{
    public void Read(ref JReader reader, out Dictionary<string, T>? value)
    {
        if (!reader.ReadObject())
        {
            value = null;
            return;
        }

        value = new Dictionary<string, T>();
        while (reader.CanReadNextObjectItem())
        {
            var propertyName = reader.ReadPropertyName();
            Formatter<T>.Shared.Read(ref reader, out var propertyValue);
            value.Add(propertyName, propertyValue);
        }
    }

    public void Write(ref JWriter writer, Dictionary<string, T>? value)
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
            Formatter<T>.Shared.Write(ref writer, kv.Value);
        }
        writer.WriteObjectEnd();
    }
}
