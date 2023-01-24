using System.Runtime.CompilerServices;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonNumber : JsonElement
    {
        public static JsonNumber Zero { get; } = new JsonNumber(0.0d);
        public static JsonNumber MinValue { get; } = new JsonNumber(double.MinValue);
        public static JsonNumber MaxValue { get; } = new JsonNumber(double.MaxValue);

        private readonly double _value;

        public override JsonElementType Type => JsonElementType.Number;

        public override double? asDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _value; }
        public override float? asFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)_value; }
        public override sbyte? asSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)_value; }
        public override short? asShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)_value; }
        public override int? asInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)_value; }
        public override long? asLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)_value; }
        public override byte? asByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)_value; }
        public override ushort? asUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)_value; }
        public override uint? asUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)_value; }
        public override ulong? asULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)_value; }
        public override decimal? asDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)_value; }

        public override double toDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _value; }
        public override float toFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)_value; }
        public override sbyte toSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)_value; }
        public override short toShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)_value; }
        public override int toInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)_value; }
        public override long toLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)_value; }
        public override byte toByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)_value; }
        public override ushort toUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)_value; }
        public override uint toUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)_value; }
        public override ulong toULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)_value; }
        public override decimal toDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)_value; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(double value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(float value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(sbyte value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(short value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(int value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(long value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(byte value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(ushort value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(uint value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(ulong value) => _value = value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JsonNumber(decimal value) => _value = (double)value;

        public override string ToString() => _value.ToString();
        public string ToString(string format) => _value.ToString(format);

        protected override bool IsEqual(JsonElement? element)
            => element is JsonNumber jsonNumber && jsonNumber.asDouble == asDouble;

        public override bool Equals(float other) => asFloat == other;
        public override bool Equals(double other) => asDouble == other;
        public override bool Equals(decimal other) => asDecimal == other;
        public override bool Equals(sbyte other) => asSByte == other;
        public override bool Equals(short other) => asShort == other;
        public override bool Equals(int other) => asInt == other;
        public override bool Equals(long other) => asLong == other;
        public override bool Equals(byte other) => asByte == other;
        public override bool Equals(ushort other) => asUShort == other;
        public override bool Equals(uint other) => asUInt == other;
        public override bool Equals(ulong other) => asULong == other;
    }
}
