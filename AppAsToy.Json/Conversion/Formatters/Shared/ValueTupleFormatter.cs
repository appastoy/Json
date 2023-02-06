using System;
using System.Runtime.CompilerServices;

namespace AppAsToy.Json.Conversion.Formatters.Shared;

internal abstract class ValueTupleFormatterBase<TValueTuple, TFormmater> : SharedFormatter<TValueTuple, TFormmater>
    where TValueTuple : struct, ITuple
    where TFormmater : class, IFormatter<TValueTuple>, new()
{
    public T GetItem<T>(ref JReader reader)
    {
        if (!reader.MoveNextArrayItem())
            throw new Exception($"{typeof(TValueTuple).Name} item not found");

        Formatter<T>.Shared.Read(ref reader, out var item);
        return item;
    }
    public void CheckEnd(ref JReader reader)
    {
        if (reader.MoveNextArrayItem())
            throw new Exception($"{typeof(TValueTuple).Name} has too many items.");
    }
}

internal sealed class ValueTupleFormatter<T> : ValueTupleFormatterBase<ValueTuple<T>, ValueTupleFormatter<T>>
{

    public override void Read(ref JReader reader, out ValueTuple<T> value)
    {
        reader.ReadArray();
        value = new(GetItem<T>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T> value)
    {
        writer.WriteArrayStart();
        Formatter<T>.Shared.Write(ref writer, value.Item1);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2> : ValueTupleFormatterBase<ValueTuple<T1, T2>, ValueTupleFormatter<T1, T2>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3>, ValueTupleFormatter<T1, T2, T3>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3, T4> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3, T4>, ValueTupleFormatter<T1, T2, T3, T4>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3, T4> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader),
                    GetItem<T4>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3, T4> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        Formatter<T4>.Shared.Write(ref writer, value.Item4);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3, T4, T5> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3, T4, T5>, ValueTupleFormatter<T1, T2, T3, T4, T5>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3, T4, T5> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader),
                    GetItem<T4>(ref reader),
                    GetItem<T5>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3, T4, T5> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        Formatter<T4>.Shared.Write(ref writer, value.Item4);
        Formatter<T5>.Shared.Write(ref writer, value.Item5);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3, T4, T5, T6>, ValueTupleFormatter<T1, T2, T3, T4, T5, T6>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3, T4, T5, T6> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader),
                    GetItem<T4>(ref reader),
                    GetItem<T5>(ref reader),
                    GetItem<T6>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        Formatter<T4>.Shared.Write(ref writer, value.Item4);
        Formatter<T5>.Shared.Write(ref writer, value.Item5);
        Formatter<T6>.Shared.Write(ref writer, value.Item6);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3, T4, T5, T6, T7>, ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7>>
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader),
                    GetItem<T4>(ref reader),
                    GetItem<T5>(ref reader),
                    GetItem<T6>(ref reader),
                    GetItem<T7>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        Formatter<T4>.Shared.Write(ref writer, value.Item4);
        Formatter<T5>.Shared.Write(ref writer, value.Item5);
        Formatter<T6>.Shared.Write(ref writer, value.Item6);
        Formatter<T7>.Shared.Write(ref writer, value.Item7);
        writer.WriteArrayEnd();
    }
}

internal sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7, TRest> : ValueTupleFormatterBase<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>, ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7, TRest>>
    where TRest : struct
{
    public override void Read(ref JReader reader, out ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value)
    {
        reader.ReadArray();
        value = new(GetItem<T1>(ref reader),
                    GetItem<T2>(ref reader),
                    GetItem<T3>(ref reader),
                    GetItem<T4>(ref reader),
                    GetItem<T5>(ref reader),
                    GetItem<T6>(ref reader),
                    GetItem<T7>(ref reader),
                    GetItem<TRest>(ref reader));
        CheckEnd(ref reader);
    }

    public override void Write(ref JWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value)
    {
        writer.WriteArrayStart();
        Formatter<T1>.Shared.Write(ref writer, value.Item1);
        Formatter<T2>.Shared.Write(ref writer, value.Item2);
        Formatter<T3>.Shared.Write(ref writer, value.Item3);
        Formatter<T4>.Shared.Write(ref writer, value.Item4);
        Formatter<T5>.Shared.Write(ref writer, value.Item5);
        Formatter<T6>.Shared.Write(ref writer, value.Item6);
        Formatter<T7>.Shared.Write(ref writer, value.Item7);
        Formatter<TRest>.Shared.Write(ref writer, value.Rest);
        writer.WriteArrayEnd();
    }
}