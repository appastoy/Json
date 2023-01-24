using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonTimeSpan : JsonElement
    {
        public readonly TimeSpan RawValue;

        public override JsonElementType Type => JsonElementType.TimeSpan;
        public override TimeSpan? asTimeSpan => RawValue;
        public override TimeSpan toTimeSpan => RawValue;

        public JsonTimeSpan(TimeSpan timeSpan)
        {
            RawValue = timeSpan;
        }

        public override string ToString(bool _) => $"\"{RawValue.ToString(@"d\.hh\:mm\:ss")}\"";

        protected override bool IsEqual(JsonElement? element)
            => element is JsonTimeSpan ts && ts.RawValue == RawValue;

        public override bool Equals(TimeSpan other) => other == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
