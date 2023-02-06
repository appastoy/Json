#pragma warning disable CS8765


namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ArrayFormatter<T> : CollectionFormatter<T, T[], ArrayFormatter<T>>
{
    public override void Read(ref JReader reader, out T[]? value)
        => value = ReadList(ref reader)?.ToArray();
}

