using System;
using System.Globalization;

namespace AppAsToy.Json.Conversion.Formatters;

internal abstract class NumberFormatter<T> : IFormatter<T>
    where T : struct, IConvertible, IEquatable<T>
{
    public abstract void Read(ref JReader reader, out T value);

    public void Write(ref JWriter writer, T value)
    {
        writer.Write(value.ToDouble(CultureInfo.InvariantCulture));
    }
}

internal abstract class NullableNumberFormatter<T> : IFormatter<T?>
    where T : struct, IConvertible, IEquatable<T>
{
    public abstract void Read(ref JReader reader, out T? value);

    public void Write(ref JWriter writer, T? value)
    {
        if (value.HasValue)
            writer.Write(value.Value.ToDouble(CultureInfo.InvariantCulture));
        else
            writer.WriteNull();
    }
}

internal sealed class SByteFormatter : NumberFormatter<sbyte> { public override void Read(ref JReader reader, out sbyte value) => value = (sbyte)reader.ReadNumber(); }
internal sealed class ShortFormatter : NumberFormatter<short> { public override void Read(ref JReader reader, out short value) => value = (short)reader.ReadNumber(); }
internal sealed class IntFormatter : NumberFormatter<int> { public override void Read(ref JReader reader, out int value) => value = (int)reader.ReadNumber(); }
internal sealed class LongFormatter : NumberFormatter<long> { public override void Read(ref JReader reader, out long value) => value = (long)reader.ReadNumber(); }
internal sealed class ByteFormatter : NumberFormatter<byte> { public override void Read(ref JReader reader, out byte value) => value = (byte)reader.ReadNumber(); }
internal sealed class UShortFormatter : NumberFormatter<ushort> { public override void Read(ref JReader reader, out ushort value) => value = (ushort)reader.ReadNumber(); }
internal sealed class UIntFormatter : NumberFormatter<uint> { public override void Read(ref JReader reader, out uint value) => value = (uint)reader.ReadNumber(); }
internal sealed class ULongFormatter : NumberFormatter<ulong> { public override void Read(ref JReader reader, out ulong value) => value = (ulong)reader.ReadNumber(); }
internal sealed class FloatFormatter : NumberFormatter<float> { public override void Read(ref JReader reader, out float value) => value = (float)reader.ReadNumber(); }
internal sealed class DoubleFormatter : NumberFormatter<double> { public override void Read(ref JReader reader, out double value) => value = (double)reader.ReadNumber(); }
internal sealed class DecimalFormatter : NumberFormatter<decimal> { public override void Read(ref JReader reader, out decimal value) => value = (decimal)reader.ReadNumber(); }

internal sealed class NullableSByteFormatter : NullableNumberFormatter<sbyte> { public override void Read(ref JReader reader, out sbyte? value) => value = (sbyte?)reader.ReadNumberOrNull(); }
internal sealed class NullableShortFormatter : NullableNumberFormatter<short> { public override void Read(ref JReader reader, out short? value) => value = (short?)reader.ReadNumberOrNull(); }
internal sealed class NullableIntFormatter : NullableNumberFormatter<int> { public override void Read(ref JReader reader, out int? value) => value = (int?)reader.ReadNumberOrNull(); }
internal sealed class NullableLongFormatter : NullableNumberFormatter<long> { public override void Read(ref JReader reader, out long? value) => value = (long?)reader.ReadNumberOrNull(); }
internal sealed class NullableByteFormatter : NullableNumberFormatter<byte> { public override void Read(ref JReader reader, out byte? value) => value = (byte?)reader.ReadNumberOrNull(); }
internal sealed class NullableUShortFormatter : NullableNumberFormatter<ushort> { public override void Read(ref JReader reader, out ushort? value) => value = (ushort?)reader.ReadNumberOrNull(); }
internal sealed class NullableUIntFormatter : NullableNumberFormatter<uint> { public override void Read(ref JReader reader, out uint? value) => value = (uint?)reader.ReadNumberOrNull(); }
internal sealed class NullableULongFormatter : NullableNumberFormatter<ulong> { public override void Read(ref JReader reader, out ulong? value) => value = (ulong?)reader.ReadNumberOrNull(); }
internal sealed class NullableFloatFormatter : NullableNumberFormatter<float> { public override void Read(ref JReader reader, out float? value) => value = (float?)reader.ReadNumberOrNull(); }
internal sealed class NullableDoubleFormatter : NullableNumberFormatter<double> { public override void Read(ref JReader reader, out double? value) => value = reader.ReadNumberOrNull(); }
internal sealed class NullableDecimalFormatter : NullableNumberFormatter<decimal> { public override void Read(ref JReader reader, out decimal? value) => value = (decimal?)reader.ReadNumberOrNull(); }