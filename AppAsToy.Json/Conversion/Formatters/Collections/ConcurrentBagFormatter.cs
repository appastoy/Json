using System.Collections.Concurrent;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ConcurrentBagFormatter<T> : CollectionFormatter<T, ConcurrentBag<T>, ConcurrentBagFormatter<T>>
{
    public override void Read(ref JReader reader, out ConcurrentBag<T>? value)
    {
        var list = ReadList(ref reader);
        if (list != null)
            value = new ConcurrentBag<T>(list);
        else
            value = null;
    }
}
