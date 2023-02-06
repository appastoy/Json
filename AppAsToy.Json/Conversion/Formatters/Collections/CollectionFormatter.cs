using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public abstract class CollectionFormatter<T, TCollection, TFormatter> :
    SharedFormatter<TCollection, TFormatter>
    where TCollection : class, IReadOnlyCollection<T>
    where TFormatter : class, IFormatter<TCollection>, new()
{
    public override void Write(ref JWriter writer, TCollection? value)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        if (value.Count == 0)
        {
            writer.WriteEmptyArray();
            return;
        }

        var isFirst = true;
        writer.WriteArrayStart();
        foreach (var item in value)
        {
            if (isFirst)
                isFirst = false;
            else
                writer.WriteComma();
            Formatter<T>.Shared.Write(ref writer, item);
        }
        writer.WriteArrayEnd();
    }

    protected List<T>? ReadList(ref JReader reader)
    {
        if (!reader.ReadArray())
            return null;

        var list = new List<T>();
        while (reader.MoveNextArrayItem())
        {
            Formatter<T>.Shared.Read(ref reader, out var item);
            list.Add(item);
        }
        return list;
    }
}

