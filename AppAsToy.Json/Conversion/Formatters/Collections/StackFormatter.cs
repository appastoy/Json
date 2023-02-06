#pragma warning disable CS8765

using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class StackFormatter<T> : CollectionFormatter<T, Stack<T>, StackFormatter<T>>
{
    public override void Read(ref JReader reader, out Stack<T>? value)
    {
        var list = ReadList(ref reader);
        if (list != null)
        {
            list.Reverse();
            value = new Stack<T>(list);
        }
        else
        {
            value = null;
        }
    }
}

