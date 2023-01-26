using System.Runtime.CompilerServices;

namespace AppAsToy.Json
{
    public sealed class JNumber : JValue<double>
    {
        public static JNumber Zero { get; } = new JNumber(0.0d);
        public static JNumber MinValue { get; } = new JNumber(double.MinValue);
        public static JNumber MaxValue { get; } = new JNumber(double.MaxValue);

        public override JElementType Type => JElementType.Number;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(double value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(float value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(sbyte value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(short value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(int value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(long value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(byte value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(ushort value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(uint value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(ulong value) : base(value) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JNumber(decimal value) : base((double)value) { }

        public override string Serialize(bool _, string? format = null, bool ___ = false) 
            => format != null 
            ? RawValue.ToString(format) 
            : RawValue.ToString();

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
