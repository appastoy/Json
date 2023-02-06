#pragma warning disable CS8765

using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class QueueFormatter<T> : CollectionFormatter<T, Queue<T>, QueueFormatter<T>>
{
    public override void Read(ref JReader reader, out Queue<T>? value)
    {
        var list = ReadList(ref reader);
        if (list != null)
            value = new Queue<T>(list);
        else
            value = null;
    }
}

