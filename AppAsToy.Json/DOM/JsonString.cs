using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonString : JsonElement, IEquatable<JsonString>
    {
        private static readonly Regex _escapeCharsRE = new Regex("[\"\\\\/\b\f\n\r\t]");

        public static JsonString Empty { get; } = new JsonString(string.Empty);

        public string Value { get; }

        public override JsonElementType Type => JsonElementType.String;

        public JsonString(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            if (Value.Length == 0)
                return "\"\"";

            var escapedString = _escapeCharsRE.Replace(Value, m =>
                m.Value[0] switch
                {
                    '\\' => "\\\\",
                    '/' => "\\/",
                    '\b' => "\\b",
                    '\f' => "\\f",
                    '\n' => "\\n",
                    '\r' => "\\r",
                    '\t' => "\\t",
                    _ => throw new NotImplementedException()
                });

            return $"\"{escapedString}\"";
        }

        public bool Equals(JsonString other) => Value == other.Value;
        public override bool Equals(object obj) => obj is JsonString str && str.Value == Value;
        public override int GetHashCode() => Value.GetHashCode();

        protected override bool IsEqual(JsonElement? element)
            => element is JsonString jsonString && jsonString.Value == Value;

        public override bool Equals(string? other) => other == Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonString(string value) 
            => value != null 
                ? value.Length > 0 
                    ? new JsonString(value) 
                    : Empty 
                : throw new ArgumentNullException(nameof(value));
    }
}
