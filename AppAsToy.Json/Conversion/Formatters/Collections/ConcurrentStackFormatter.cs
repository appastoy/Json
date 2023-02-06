using System.Collections.Concurrent;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ConcurrentStackFormatter<T> : CollectionFormatter<T, ConcurrentStack<T>, ConcurrentStackFormatter<T>>
{
    public override void Read(ref JReader reader, out ConcurrentStack<T>? value)
    {
        var list = ReadList(ref reader);
        if (list != null)
        {
            list.Reverse();
            value = new ConcurrentStack<T>(list);
        }
        else
        {
            value = null;
        }
    }
}