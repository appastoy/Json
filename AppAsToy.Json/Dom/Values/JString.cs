using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppAsToy.Json
{
    public sealed class JString : JValue<string>
    {
        private static readonly Regex _escapeCharsRE = new("[\"\\\\/\b\f\n\r\t]|[^\x00-\x7f]");

        public const string DefaultDateTimeFormat = @"yyyy\-MM\-dd HH\:mm\:ss";
        public const string DefaultDateTimeOffsetFormat = @"yyyy\-MM\-dd HH\:mm\:ss K";
        public const string DefaultTimeSpanFormat = @"d\.hh\:mm\:ss";
        public const string DefaultGuidFormat = "N";
        public static IFormatProvider DefaultFormatProvider => CultureInfo.InvariantCulture;

        public static JString Empty { get; } = new(string.Empty);

        public static JString FromDateTime(DateTime value, string? format = null, IFormatProvider? formatProvider = null)
            => new(value.ToString(format ?? DefaultDateTimeFormat, formatProvider ?? DefaultFormatProvider));
        public static JString FromDateTimeOffset(DateTimeOffset value, string? format = null, IFormatProvider? formatProvider = null)
            => new(value.ToString(format ?? DefaultDateTimeOffsetFormat, formatProvider ?? DefaultFormatProvider));
        public static JString FromTimeSpan(TimeSpan value, string? format = null, IFormatProvider? formatProvider = null)
            => new(value.ToString(format ?? DefaultTimeSpanFormat, formatProvider ?? DefaultFormatProvider));
        public static JString FromGuid(Guid value, string? format = null, IFormatProvider? formatProvider = null)
            => new(value.ToString(format ?? DefaultGuidFormat, formatProvider ?? DefaultFormatProvider));
        public static JString FromByteArray(byte[] value) => new(Convert.ToBase64String(value ?? throw new ArgumentNullException(nameof(value))));

        public override JElementType Type => JElementType.String;
        public override string? AsString => RawValue;
        public override DateTime? AsDateTime(
            string? format = null, 
            IFormatProvider? formatProvider = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) 
            => format != null 
            ? DateTime.TryParseExact(
                RawValue, 
                format,
                formatProvider ?? DefaultFormatProvider,
                dateTimeStyles,
                out var result) 
                ? result 
                : null
            : DateTime.TryParse(
                RawValue,
                formatProvider ?? DefaultFormatProvider,
                dateTimeStyles,
                out var result2)
                ? result2
                : null;

        public override DateTimeOffset? AsDateTimeOffset(
            string? format = null,
            IFormatProvider? formatProvider = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) 
            => format != null 
            ? DateTimeOffset.TryParseExact(
                RawValue,
                format, 
                formatProvider ?? DefaultFormatProvider,
                dateTimeStyles,
                out var result) 
                ? result 
                : null
            : DateTimeOffset.TryParse(
                RawValue,
                formatProvider ?? DefaultFormatProvider,
                dateTimeStyles,
                out var result2)
                ? result2
                : null;

        public override TimeSpan? AsTimeSpan(
            string? format = null, 
            IFormatProvider? formatProvider = null, 
            TimeSpanStyles timeSpanStyles = TimeSpanStyles.None) 
            => format != null
            ? TimeSpan.TryParseExact(
                RawValue,
                format,
                formatProvider ?? DefaultFormatProvider,
                timeSpanStyles,
                out var result)
                ? result
                : null
            : TimeSpan.TryParse(
                RawValue, 
                formatProvider ?? DefaultFormatProvider,
                out var result2) 
                ? result2 
                : null;

        public override Guid? AsGuid(string? format = null)
            => format != null
            ? Guid.TryParseExact(RawValue, format, out var result) ? result : null
            : Guid.TryParse(RawValue, out var result2) ? result2 : null;

        public override byte[]? AsByteArray
        {
            get
            {
                if (RawValue.Length == 0)
                    return Array.Empty<byte>();

                Span<byte> bytes = stackalloc byte[((RawValue.Length+3) >> 2) * 3];
                if (!Convert.TryFromBase64String(RawValue, bytes, out var size))
                    return null;

                return bytes.Length == size 
                    ? bytes.ToArray() 
                    : bytes.Slice(0, size).ToArray();
            }
        }

        public override string ToStringValue => RawValue;
        public override DateTime ToDateTime(
            string? format = null,
            IFormatProvider? formatProvider = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) 
            => AsDateTime(format, formatProvider, dateTimeStyles) ?? throw new InvalidCastException();

        public override DateTimeOffset ToDateTimeOffset(
            string? format = null,
            IFormatProvider? formatProvider = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
            => AsDateTimeOffset(format, formatProvider, dateTimeStyles) ?? throw new InvalidCastException();

        public override TimeSpan ToTimeSpan(
            string? format = null,
            IFormatProvider? formatProvider = null,
            TimeSpanStyles timeSpanStyles = TimeSpanStyles.None) 
            => AsTimeSpan(format, formatProvider, timeSpanStyles) ?? throw new InvalidCastException();

        public override Guid ToGuid(string? format = null) => AsGuid(format) ?? throw new InvalidCastException();
        public override byte[] ToByteArray => AsByteArray ?? throw new InvalidCastException();

        public JString(string value) : base(value ?? throw new ArgumentNullException(nameof(value))) { }

        public override string Serialize(bool _, string? __, bool escapeUnicode = false) => ConvertToJson(RawValue, escapeUnicode);
        public static string ConvertToJson(string value, bool escapeUnicode)
        {
            if (value.Length == 0)
                return "\"\"";

            var escapedString = _escapeCharsRE.Replace(value, m =>
                    m.Value[0] switch
                    {
                        '"' => "\\\"",
                        '\\' => "\\\\",
                        '/' => "\\/",
                        '\b' => "\\b",
                        '\f' => "\\f",
                        '\n' => "\\n",
                        '\r' => "\\r",
                        '\t' => "\\t",
                        char ch when ch > 0x7f => escapeUnicode ? $"\\u{((int)ch).ToString("X4")}" : m.Value,
                        _ => throw new InvalidOperationException()
                    });

            return $"\"{escapedString}\"";
        }
        public override bool Equals(string other) => other == RawValue;
        public override bool Equals(DateTime other) => AsDateTime()?.Equals(other) ?? false;
        public override bool Equals(DateTimeOffset other) => AsDateTimeOffset()?.Equals(other) ?? false;
        public override bool Equals(TimeSpan other) => AsTimeSpan()?.Equals(other) ?? false;
        public override bool Equals(Guid other) => AsGuid()?.Equals(other) ?? false;
        public override bool Equals(byte[] other)
        {
            var myBytes = AsByteArray;
            if (myBytes == null)
                return false;

            return myBytes.Length == other.Length &&
                myBytes.SequenceEqual(other);
        }
    }
}
