using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonDateTimeOffset : JsonValue<DateTimeOffset>
    {
        public static JsonDateTimeOffset Now => new(DateTimeOffset.Now);
        public static JsonDateTimeOffset UtcNow => new(DateTimeOffset.UtcNow);

        public override JsonElementType Type => JsonElementType.DateTimeOffset;
        public override DateTimeOffset? asDateTimeOffset => RawValue;
        public override DateTimeOffset toDateTimeOffset => RawValue;

        public JsonDateTimeOffset(DateTimeOffset dateTime) : base(dateTime) { }
        public override string ToString(bool _) => $"\"{RawValue.ToString("yyyy-MM-ddTHH:mm:ssK")}\"";
        public override bool Equals(DateTimeOffset other) => other == RawValue;
    }
}
