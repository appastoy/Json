using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonTimeSpan : JsonValue<TimeSpan>
    {
        public static readonly JsonTimeSpan Zero = new(TimeSpan.Zero);

        public override JsonElementType Type => JsonElementType.TimeSpan;
        public override TimeSpan? asTimeSpan => RawValue;
        public override TimeSpan toTimeSpan => RawValue;

        public JsonTimeSpan(TimeSpan timeSpan) : base(timeSpan) { }

        public override string ToString(bool _) => $"\"{RawValue.ToString(@"d\.hh\:mm\:ss")}\"";

        public override bool Equals(TimeSpan other) => other == RawValue;
    }
}
