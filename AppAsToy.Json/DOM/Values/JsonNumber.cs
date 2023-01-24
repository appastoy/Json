using System.Runtime.CompilerServices;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonNumber : JsonValue<double>
    {
        public static JsonNumber Zero { get; } = new JsonNumber(0.0d);
        public static JsonNumber MinValue { get; } = new JsonNumber(double.MinValue);
        public static JsonNumber MaxValue { get; } = new JsonNumber(double.MaxValue);

        public override JsonElementType Type => JsonElementType.Number;

        public override double? AsDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => RawValue; }
        public override float? AsFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)RawValue; }
        public override sbyte? AsSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)RawValue; }
        public override short? AsShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)RawValue; }
        public override int? AsInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)RawValue; }
        public override long? AsLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)RawValue; }
        public override byte? AsByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)RawValue; }
        public override ushort? AsUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)RawValue; }
        public override uint? AsUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)RawValue; }
        public override ulong? AsULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)RawValue; }
        public override decimal? AsDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)RawValue; }

        public override double ToDouble { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => RawValue; }
        public override float ToFloat { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)RawValue; }
        public override sbyte ToSByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (sbyte)RawValue; }
        public override short ToShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (short)RawValue; }
        public override int ToInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (int)RawValue; }
        public override long ToLong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (long)RawValue; }
        public override byte ToByte { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (byte)RawValue; }
        public override ushort ToUShort { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ushort)RawValue; }
        public override uint ToUInt { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (uint)RawValue; }
        public override ulong ToULong { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (ulong)RawValue; }
        public override decimal ToDecimal { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (decimal)RawValue; }

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

        public override bool Equals(float other) => ToFloat == other;
        public override bool Equals(double other) => RawValue == other;
        public override bool Equals(decimal other) => ToDecimal == other;
        public override bool Equals(sbyte other) => ToSByte == other;
        public override bool Equals(short other) => ToShort == other;
        public override bool Equals(int other) => ToInt == other;
        public override bool Equals(long other) => ToLong == other;
        public override bool Equals(byte other) => ToByte == other;
        public override bool Equals(ushort other) => ToUShort == other;
        public override bool Equals(uint other) => ToUInt == other;
        public override bool Equals(ulong other) => ToULong == other;
    }
}
