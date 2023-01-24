using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonDateTime : JsonElement
    {
        public static JsonDateTime Now => new JsonDateTime(DateTime.Now);
        public static JsonDateTime UtcNow => new JsonDateTime(DateTime.UtcNow);

        public readonly DateTime RawValue;

        public override JsonElementType Type => JsonElementType.DateTime;
        public override DateTime? asDateTime => RawValue;
        public override DateTime toDateTime => RawValue;

        public JsonDateTime(DateTime dateTime)
        {
            RawValue = dateTime;
        }

        public override string ToString(bool _) => $"\"{RawValue.ToString("s")}\"";

        protected override bool IsEqual(JsonElement? element)
            => element is JsonDateTime dt && dt.RawValue == RawValue;

        public override bool Equals(DateTime other) => other == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
