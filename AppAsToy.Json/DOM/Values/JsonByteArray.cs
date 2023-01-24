using System;
using System.Linq;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonByteArray : JsonElement
    {
        public static readonly JsonByteArray Empty = new JsonByteArray(Array.Empty<byte>());

        public readonly byte[] RawValue;

        public override JsonElementType Type => JsonElementType.ByteArray;
        public override byte[]? asByteArray => RawValue;
        public override byte[] toByteArray => RawValue;

        public JsonByteArray(byte[] byteArray)
        {
            RawValue = byteArray ?? throw new ArgumentNullException(nameof(byteArray));
        }

        public override string ToString(bool _) => $"\"{Convert.ToBase64String(RawValue)}\"";

        protected override bool IsEqual(JsonElement? element)
            => element is JsonByteArray ba && ba.RawValue == RawValue;

        public override bool Equals(byte[] other) => 
            other != null && 
            RawValue.Length == other.Length && 
            RawValue.SequenceEqual(other);

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
