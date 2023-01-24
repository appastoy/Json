using System;
using System.Text.RegularExpressions;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonString : JsonElement
    {
        private static readonly Regex _escapeCharsRE = new("[\"\\\\/\b\f\n\r\t]");

        public static JsonString Empty { get; } = new JsonString(string.Empty);

        public readonly string RawValue;

        public override JsonElementType Type => JsonElementType.String;
        public override string? asString => RawValue;
        public override string toString => RawValue;

        public JsonString(string value)
        {
            RawValue = value ?? throw new ArgumentNullException(nameof(value));
        }

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

        protected override bool IsEqual(JsonElement? element)
            => element is JsonString jsonString && jsonString.RawValue == RawValue;

        public override bool Equals(string? other) => other == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
