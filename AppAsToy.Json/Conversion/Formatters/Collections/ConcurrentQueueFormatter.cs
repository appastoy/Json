using System.Collections.Concurrent;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ConcurrentQueueFormatter<T> : CollectionFormatter<T, ConcurrentQueue<T>, ConcurrentQueueFormatter<T>>
{
    public override void Read(ref JReader reader, out ConcurrentQueue<T>? value)
    {
        var list = ReadList(ref reader);
        if (list != null)
            value = new ConcurrentQueue<T>(list);
        else
            value = null;
    }
}
