using System;

namespace AppAsToy.Json.Conversion.Formatters.Primitives;

internal sealed class DateTimeOffsetFormatter : IFormatter<DateTimeOffset>
{
    public void Read(ref JReader reader, out DateTimeOffset value)
    {
        value = DateTimeOffset.Parse(reader.ReadString());
    }

    public void Write(ref JWriter writer, DateTimeOffset value)
    {
        writer.Write(value.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K"));
    }
}
