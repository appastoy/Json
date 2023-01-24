using System.Runtime.CompilerServices;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonNumber : JsonValue<double>
    {
        public static JsonNumber Zero { get; } = new JsonNumber(0.0d);
        public static JsonNumber MinValue { get; } = new JsonNumber(double.MinValue);
        public static JsonNumber MaxValue { get; } = new JsonNumber(double.MaxValue);

        public override JsonElementType Type => JsonElementType.Number;

        public override double? asDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => RawValue; }
        public override float? asFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)RawValue; }
        public override sbyte? asSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)RawValue; }
        public override short? asShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)RawValue; }
        public override int? asInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)RawValue; }
        public override long? asLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)RawValue; }
        public override byte? asByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)RawValue; }
        public override ushort? asUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)RawValue; }
        public override uint? asUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)RawValue; }
        public override ulong? asULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)RawValue; }
        public override decimal? asDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)RawValue; }

        public override double toDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => RawValue; }
        public override float toFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)RawValue; }
        public override sbyte toSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)RawValue; }
        public override short toShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)RawValue; }
        public override int toInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)RawValue; }
        public override long toLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)RawValue; }
        public override byte toByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)RawValue; }
        public override ushort toUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)RawValue; }
        public override uint toUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)RawValue; }
        public override ulong toULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)RawValue; }
        public override decimal toDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)RawValue; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(double value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(float value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(sbyte value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(short value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(int value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(long value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(byte value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(ushort value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(uint value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(ulong value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(decimal value) : base((double)value) { }

        public override string ToString(bool _) => RawValue.ToString();
        public string ToString(string format) => RawValue.ToString(format);

        public override bool Equals(float other) => toFloat == other;
        public override bool Equals(double other) => RawValue == other;
        public override bool Equals(decimal other) => toDecimal == other;
        public override bool Equals(sbyte other) => toSByte == other;
        public override bool Equals(short other) => toShort == other;
        public override bool Equals(int other) => toInt == other;
        public override bool Equals(long other) => toLong == other;
        public override bool Equals(byte other) => toByte == other;
        public override bool Equals(ushort other) => toUShort == other;
        public override bool Equals(uint other) => toUInt == other;
        public override bool Equals(ulong other) => toULong == other;
    }
}
