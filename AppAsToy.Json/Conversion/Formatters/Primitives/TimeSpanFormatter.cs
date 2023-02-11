using System;

namespace AppAsToy.Json.Conversion.Formatters.Primitives;

internal sealed class TimeSpanFormatter : IFormatter<TimeSpan>
{
    public void Read(ref JReader reader, out TimeSpan value)
    {
        value = TimeSpan.Parse(reader.ReadString());
    }

    public void Write(ref JWriter writer, TimeSpan value)
    {
        writer.Write(value.ToString(@"d\.hh\:mm\:ss"));
    }
}