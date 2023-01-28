using System.Collections.Generic;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters;

public sealed class ListFormatter<T> : CollectionFormatter<T, List<T>, ListFormatter<T>> { public override void Read(ref JReader reader, out List<T>? value) => value = ReadList(ref reader); }
public sealed class ArrayFormatter<T> : CollectionFormatter<T, T[], ArrayFormatter<T>> { public override void Read(ref JReader reader, out T[]? value) => value = ReadList(ref reader)?.ToArray(); }

public abstract class CollectionFormatter<T, TList, TFormatter> :
    SharedFormatter<TList, TFormatter>,
    IFormatter<TList>
    where TList : class, IList<T>
    where TFormatter : class, IFormatter<TList>, new()
{
    public abstract void Read(ref JReader reader, out TList value);

    public void Write(ref JWriter writer, TList? value)
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

        writer.WriteArrayStart();
        Formatter<T>.Shared.Write(ref writer, value[0]);
        for (var i = 1; i < value.Count; i++)
        {
            writer.WriteComma();
            Formatter<T>.Shared.Write(ref writer, value[i]);
        }
        writer.WriteArrayEnd();
    }

    protected List<T>? ReadList(ref JReader reader)
    {
        if (!reader.ReadArray())
            return null;

        var list = new List<T>();
        while (reader.CanReadNextArrayItem())
        {
            Formatter<T>.Shared.Read(ref reader, out var item);
            list.Add(item);
        }
        return list;
    }
}

