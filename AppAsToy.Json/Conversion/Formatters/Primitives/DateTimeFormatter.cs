using System;

namespace AppAsToy.Json.Conversion.Formatters.Primitives;

internal sealed class DateTimeFormatter : IFormatter<DateTime>
{
    public void Read(ref JReader reader, out DateTime value)
    {
        value = DateTime.Parse(reader.ReadString());
    }

    public void Write(ref JWriter writer, DateTime value)
    {
        writer.Write(value.ToString(@"yyyy\-MM\-dd HH\:mm\:ss"));
    }
}
