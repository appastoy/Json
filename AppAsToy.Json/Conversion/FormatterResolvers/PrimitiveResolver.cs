using AppAsToy.Json.Conversion.Formatters;
using AppAsToy.Json.Conversion.Formatters.Collections;
using AppAsToy.Json.Conversion.Formatters.Primitives;
using System;
using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.FormatterResolvers;
internal sealed class PrimitiveResolver : IFormatterResolver
{
    private readonly Dictionary<Type, IFormatter> formatterMap = new()
    {
        // Primitive
        { typeof(string), new StringFormatter() },
        { typeof(bool), new BoolFormatter() },
        { typeof(char), new CharFormatter() },
        { typeof(sbyte), new SByteFormatter() },
        { typeof(short), new ShortFormatter() },
        { typeof(int), new IntFormatter() },
        { typeof(long), new LongFormatter() },
        { typeof(byte), new ByteFormatter() },
        { typeof(ushort), new UShortFormatter() },
        { typeof(uint), new UIntFormatter() },
        { typeof(ulong), new ULongFormatter() },
        { typeof(float), new FloatFormatter() },
        { typeof(double), new DoubleFormatter() },
        { typeof(decimal), new DecimalFormatter() },
        { typeof(DateTime), new DateTimeFormatter() },
        { typeof(DateTimeOffset), new DateTimeOffsetFormatter() },
        { typeof(TimeSpan), new TimeSpanFormatter() },
        { typeof(Guid), new GuidFormatter() },
        { typeof(byte[]), new ByteArrayFormatter() },

        // Array of Primitive
        { typeof(string[]), ArrayFormatter<string>.Shared },
        { typeof(bool[]), ArrayFormatter<bool>.Shared },
        { typeof(char[]), ArrayFormatter<char>.Shared },
        { typeof(sbyte[]), ArrayFormatter<sbyte>.Shared },
        { typeof(short[]), ArrayFormatter<short>.Shared },
        { typeof(int[]), ArrayFormatter<int>.Shared },
        { typeof(long[]), ArrayFormatter<long>.Shared },
        { typeof(byte[]), ArrayFormatter<byte>.Shared },
        { typeof(ushort[]), ArrayFormatter<ushort>.Shared },
        { typeof(uint[]), ArrayFormatter<uint>.Shared },
        { typeof(ulong[]), ArrayFormatter<ulong>.Shared },
        { typeof(float[]), ArrayFormatter<float>.Shared },
        { typeof(double[]), ArrayFormatter<double>.Shared },
        { typeof(decimal[]), ArrayFormatter<decimal>.Shared },
        { typeof(DateTime[]), ArrayFormatter<DateTime>.Shared },
        { typeof(DateTimeOffset[]), ArrayFormatter<DateTimeOffset>.Shared },
        { typeof(TimeSpan[]), ArrayFormatter<TimeSpan>.Shared },
        { typeof(Guid[]), ArrayFormatter<Guid>.Shared },
        { typeof(byte[][]), ArrayFormatter<byte[]>.Shared },

        // List of Primitive
        { typeof(List < string >), ListFormatter<string>.Shared },
        { typeof(List < bool >), ListFormatter<bool>.Shared },
        { typeof(List < char >), ListFormatter<char>.Shared },
        { typeof(List < sbyte >), ListFormatter<sbyte>.Shared },
        { typeof(List < short >), ListFormatter<short>.Shared },
        { typeof(List < int >), ListFormatter<int>.Shared },
        { typeof(List < long >), ListFormatter<long>.Shared },
        { typeof(List < byte >), ListFormatter<byte>.Shared },
        { typeof(List < ushort >), ListFormatter<ushort>.Shared },
        { typeof(List < uint >), ListFormatter<uint>.Shared },
        { typeof(List < ulong >), ListFormatter<ulong>.Shared },
        { typeof(List < float >), ListFormatter<float>.Shared },
        { typeof(List < double >), ListFormatter<double>.Shared },
        { typeof(List < decimal >), ListFormatter<decimal>.Shared },
        { typeof(List < DateTime >), ListFormatter<DateTime>.Shared },
        { typeof(List < DateTimeOffset >), ListFormatter<DateTimeOffset>.Shared },
        { typeof(List < TimeSpan >), ListFormatter<TimeSpan>.Shared },
        { typeof(List < Guid >), ListFormatter<Guid>.Shared },
        { typeof(List < byte[] >), ListFormatter<byte[]>.Shared },

        // String Dictionary of Primitive
        { typeof(Dictionary < string, string >), StringDictionaryFormatter<string>.Shared },
        { typeof(Dictionary < string, bool >), StringDictionaryFormatter<bool>.Shared },
        { typeof(Dictionary < string, char >), StringDictionaryFormatter<char>.Shared },
        { typeof(Dictionary < string, sbyte >), StringDictionaryFormatter<sbyte>.Shared },
        { typeof(Dictionary < string, short >), StringDictionaryFormatter<short>.Shared },
        { typeof(Dictionary < string, int >), StringDictionaryFormatter<int>.Shared },
        { typeof(Dictionary < string, long >), StringDictionaryFormatter<long>.Shared },
        { typeof(Dictionary < string, byte >), StringDictionaryFormatter<byte>.Shared },
        { typeof(Dictionary < string, ushort >), StringDictionaryFormatter<ushort>.Shared },
        { typeof(Dictionary < string, uint >), StringDictionaryFormatter<uint>.Shared },
        { typeof(Dictionary < string, ulong >), StringDictionaryFormatter<ulong>.Shared },
        { typeof(Dictionary < string, float >), StringDictionaryFormatter<float>.Shared },
        { typeof(Dictionary < string, double >), StringDictionaryFormatter<double>.Shared },
        { typeof(Dictionary < string, decimal >), StringDictionaryFormatter<decimal>.Shared },
        { typeof(Dictionary < string, DateTime >), StringDictionaryFormatter<DateTime>.Shared },
        { typeof(Dictionary < string, DateTimeOffset >), StringDictionaryFormatter<DateTimeOffset>.Shared },
        { typeof(Dictionary < string, TimeSpan >), StringDictionaryFormatter<TimeSpan>.Shared },
        { typeof(Dictionary < string, Guid >), StringDictionaryFormatter<Guid>.Shared },
        { typeof(Dictionary < string, byte[] >), StringDictionaryFormatter<byte[]>.Shared },

        // Nullable of Primitive
        { typeof(bool?), NullableFormatter<bool>.Shared },
        { typeof(char?), NullableFormatter<char>.Shared },
        { typeof(sbyte?), NullableFormatter<sbyte>.Shared },
        { typeof(short?), NullableFormatter<short>.Shared },
        { typeof(int?), NullableFormatter<int>.Shared },
        { typeof(long?), NullableFormatter<long>.Shared },
        { typeof(byte?), NullableFormatter<byte>.Shared },
        { typeof(ushort?), NullableFormatter<ushort>.Shared },
        { typeof(uint?), NullableFormatter<uint>.Shared },
        { typeof(ulong?), NullableFormatter<ulong>.Shared },
        { typeof(float?), NullableFormatter<float>.Shared },
        { typeof(double?), NullableFormatter<double>.Shared },
        { typeof(decimal?), NullableFormatter<decimal>.Shared },
        { typeof(DateTime?), NullableFormatter<DateTime>.Shared },
        { typeof(DateTimeOffset?), NullableFormatter<DateTimeOffset>.Shared },
        { typeof(TimeSpan?), NullableFormatter<TimeSpan>.Shared },
        { typeof(Guid?), NullableFormatter<Guid>.Shared },
    };

    public IFormatter<T>? Resolve<T>()
    {
        var type = typeof(T);
        if (type.IsEnum)
            return EnumFormatter<T>.Shared;

        return formatterMap.TryGetValue(type, out var formatter)
            ? (IFormatter<T>)formatter
            : null;
    }
}
