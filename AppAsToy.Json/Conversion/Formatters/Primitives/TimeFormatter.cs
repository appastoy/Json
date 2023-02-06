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