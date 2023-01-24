using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonDateTimeOffset : JsonElement
    {
        public static JsonDateTimeOffset Now => new JsonDateTimeOffset(DateTimeOffset.Now);
        public static JsonDateTimeOffset UtcNow => new JsonDateTimeOffset(DateTimeOffset.UtcNow);

        public readonly DateTimeOffset RawValue;

        public override JsonElementType Type => JsonElementType.DateTimeOffset;
        public override DateTimeOffset? asDateTimeOffset => RawValue;
        public override DateTimeOffset toDateTimeOffset => RawValue;

        public JsonDateTimeOffset(DateTimeOffset dateTime)
        {
            RawValue = dateTime;
        }

        public override string ToString(bool _) => $"\"{RawValue.ToString("yyyy-MM-ddTHH:mm:ssK")}\"";

        protected override bool IsEqual(JsonElement? element)
            => element is JsonDateTimeOffset dto && dto.RawValue == RawValue;

        public override bool Equals(DateTimeOffset other) => other == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
