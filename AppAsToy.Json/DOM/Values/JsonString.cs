using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonString : JsonValue<string>
    {
        private static readonly Regex _escapeCharsRE = new("[\"\\\\/\b\f\n\r\t]");

        public static JsonString Empty { get; } = new JsonString(string.Empty);

        public override JsonElementType Type => JsonElementType.String;
        public override string? AsString => RawValue;
        public override DateTime? AsDateTime => DateTime.TryParse(RawValue, out var result) ? result : null;
        public override DateTimeOffset? AsDateTimeOffset => DateTime.TryParse(RawValue, out var result) ? result : null;
        public override TimeSpan? AsTimeSpan => TimeSpan.TryParse(RawValue, out var result) ? result : null;
        public override Guid? AsGuid => Guid.TryParse(RawValue, out var result) ? result : null;
        public override byte[]? AsByteArray
        {
            get
            {
                if (RawValue.Length == 0)
                    return Array.Empty<byte>();

                Span<byte> bytes = stackalloc byte[(RawValue.Length >> 2) * 3];
                if (!Convert.TryFromBase64String(RawValue, bytes, out var size))
                    return null;

                return bytes.Length == size 
                    ? bytes.ToArray() 
                    : bytes.Slice(0, size).ToArray();
            }
        }

        public override string ToStringValue => RawValue;
        public override DateTime ToDateTime => AsDateTime ?? throw new InvalidCastException();
        public override DateTimeOffset ToDateTimeOffset => AsDateTimeOffset ?? throw new InvalidCastException();
        public override TimeSpan ToTimeSpan => AsTimeSpan ?? throw new InvalidCastException();
        public override Guid ToGuid => AsGuid ?? throw new InvalidCastException();
        public override byte[] ToByteArray => AsByteArray ?? throw new InvalidCastException();


        public JsonString(string value) : base(value ?? throw new ArgumentNullException(nameof(value))) { }
        public static JsonString FromByteArray(byte[] value) => new(Convert.ToBase64String(value ?? throw new ArgumentNullException(nameof(value))));
        public static JsonString FromDateTime(DateTime value) => new(value.ToString(@"yyyy\-MM\-dd HH\:mm\:ss"));
        public static JsonString FromDateTimeOffset(DateTimeOffset value) => new(value.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K"));
        public static JsonString FromTimeSpan(TimeSpan value) => new(value.ToString(@"d\.hh\:mm\:ss"));
        public static JsonString FromGuid(Guid value) => new(value.ToString());

        public override string ToString(bool _) => ConvertToJson(RawValue);
        public static string ConvertToJson(string value)
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
                    _ => m.Value[0].ToString()
                }); ;

            return $"\"{escapedString}\"";
        }
        public override bool Equals(string other) => other == RawValue;
        public override bool Equals(DateTime other) => AsDateTime?.Equals(other) ?? false;
        public override bool Equals(DateTimeOffset other) => AsDateTimeOffset?.Equals(other) ?? false;
        public override bool Equals(TimeSpan other) => AsTimeSpan?.Equals(other) ?? false;
        public override bool Equals(Guid other) => AsGuid?.Equals(other) ?? false;
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
