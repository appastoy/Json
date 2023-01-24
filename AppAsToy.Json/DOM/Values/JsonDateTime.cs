using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonDateTime : JsonValue<DateTime>
    {
        public static JsonDateTime Now => new(DateTime.Now);
        public static JsonDateTime UtcNow => new(DateTime.UtcNow);

        public override JsonElementType Type => JsonElementType.DateTime;
        public override DateTime? asDateTime => RawValue;
        public override DateTime toDateTime => RawValue;

        public JsonDateTime(DateTime dateTime) : base(dateTime) { }
        public override string ToString(bool _) => $"\"{RawValue.ToString("s")}\"";
        public override bool Equals(DateTime other) => other == RawValue;
    }
}
