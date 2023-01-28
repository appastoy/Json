﻿namespace AppAsToy.Json.Conversion.Formatters;
internal sealed class NullableFormatter<T> : 
    SharedFormatter<T?, NullableFormatter<T>>,
    IFormatter<T?>
    where T : struct
{
    public void Read(ref JReader reader, out T? value)
    {
        if (reader.TryReadNull())
        {
            value = null;
        }
        else
        {
            Formatter<T>.Shared.Read(ref reader, out var notNullValue);
            value = notNullValue;
        }
    }

    public void Write(ref JWriter writer, T? value)
    {
        if (value.HasValue)
            Formatter<T>.Shared.Write(ref writer, value.Value);
        else
            writer.WriteNull();
    }
}
