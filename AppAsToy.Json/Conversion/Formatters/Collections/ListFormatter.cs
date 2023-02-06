using System.Collections.Generic;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ListFormatter<T> : CollectionFormatter<T, List<T>, ListFormatter<T>>
{
    public override void Read(ref JReader reader, out List<T>? value)
        => value = ReadList(ref reader);
}

