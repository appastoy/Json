using System;

namespace AppAsToy.Json.Conversion.Formatters;

#pragma warning disable CS8767

internal sealed class StringFormatter : IFormatter<string>
{
    public void Read(ref JReader reader, out string? value)
    {
        value = reader.ReadStringOrNull();
    }

    public void Write(ref JWriter writer, string? value)
    {
        if (value == null)
            writer.WriteNull();
        else
            writer.Write(value);
    }
}

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

internal sealed class NullableCharFormatter : IFormatter<char?>
{
    public void Read(ref JReader reader, out char? value)
    {
        var stringValue = reader.ReadStringOrNull();
        if (stringValue == null)
            value = null;
        else
            value = stringValue.Length > 0 
                ? stringValue[0] 
                : throw new InvalidOperationException("String value should not be empty in char formatter.");
    }

    public void Write(ref JWriter writer, char? value)
    {
        if (value.HasValue)
            writer.Write(value.ToString());
        else
            writer.WriteNull();
    }
}