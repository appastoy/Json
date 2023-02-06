using System;

namespace AppAsToy.Json.Conversion.Formatters.Primitives;
internal sealed class GuidFormatter : IFormatter<Guid>
{
    public void Read(ref JReader reader, out Guid value)
    {
        value = Guid.Parse(reader.ReadString());
    }

    public void Write(ref JWriter writer, Guid value)
    {
        writer.Write(value.ToString("N"));
    }
}
