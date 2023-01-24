using System;
using System.Linq;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonByteArray : JsonValue<byte[]>
    {
        public static readonly JsonByteArray Empty = new(Array.Empty<byte>());
        public override JsonElementType Type => JsonElementType.ByteArray;
        public override byte[]? asByteArray => RawValue;
        public override byte[] toByteArray => RawValue;

        public JsonByteArray(byte[] byteArray) : base(byteArray ?? throw new ArgumentNullException(nameof(byteArray))) { }

        public override string ToString(bool _) => $"\"{Convert.ToBase64String(RawValue)}\"";

        public override bool Equals(byte[] other) => 
            other != null && 
            RawValue.Length == other.Length && 
            RawValue.SequenceEqual(other);
    }
}
