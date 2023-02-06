using System;

namespace AppAsToy.Json.Conversion.Formatters.Primitives;

internal sealed class CharFormatter : IFormatter<char>
{
    public void Read(ref JReader reader, out char value)
    {
        var stringValue = reader.ReadString();
        value = stringValue.Length > 0
            ? stringValue[0]
            : throw new InvalidOperationException("String value should not be empty in char formatter.");
    }

    public void Write(ref JWriter writer, char value)
    {
        writer.Write(value.ToString());
    }
}