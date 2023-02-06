using System;

#pragma warning disable CS8767

namespace AppAsToy.Json.Conversion.Formatters.Primitives;
internal sealed class ByteArrayFormatter : IFormatter<byte[]>
{
    public void Read(ref JReader reader, out byte[]? value)
    {
        if (reader.TryReadNull())
        {
            value = null;
        }
        else
        {
            value = Convert.FromBase64String(reader.ReadString());
        }
    }

    public void Write(ref JWriter writer, byte[]? value)
    {
        if (value == null)
            writer.WriteNull();
        else
            writer.Write(Convert.ToBase64String(value));
    }
}
