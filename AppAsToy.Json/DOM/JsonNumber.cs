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

        public override double? AsDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _value; }
        public override float? AsFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)_value; }
        public override sbyte? AsSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)_value; }
        public override short? AsShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)_value; }
        public override int? AsInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)_value; }
        public override long? AsLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)_value; }
        public override byte? AsByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)_value; }
        public override ushort? AsUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)_value; }
        public override uint? AsUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)_value; }
        public override ulong? AsULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)_value; }
        public override decimal? AsDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)_value; }

        public override double Double { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _value; }
        public override float Float { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)_value; }
        public override sbyte SByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)_value; }
        public override short Short { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)_value; }
        public override int Int { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)_value; }
        public override long Long { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)_value; }
        public override byte Byte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)_value; }
        public override ushort UShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)_value; }
        public override uint UInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)_value; }
        public override ulong ULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)_value; }
        public override decimal Decimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)_value; }

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
            => element is JsonNumber jsonNumber && jsonNumber.AsDouble == AsDouble;

        public override bool Equals(float other) => AsFloat == other;
        public override bool Equals(double other) => AsDouble == other;
        public override bool Equals(decimal other) => AsDecimal == other;
        public override bool Equals(sbyte other) => AsSByte == other;
        public override bool Equals(short other) => AsShort == other;
        public override bool Equals(int other) => AsInt == other;
        public override bool Equals(long other) => AsLong == other;
        public override bool Equals(byte other) => AsByte == other;
        public override bool Equals(ushort other) => AsUShort == other;
        public override bool Equals(uint other) => AsUInt == other;
        public override bool Equals(ulong other) => AsULong == other;
    }
}
