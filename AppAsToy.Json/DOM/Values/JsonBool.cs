namespace AppAsToy.Json.DOM
{
    public sealed class JsonBool : JsonValue<bool>
    {
        public static JsonBool True { get; } = new(true);
        public static JsonBool False { get; } = new(false);

        public override JsonElementType Type => JsonElementType.Bool;

        public override bool? AsBool => RawValue;
        public override bool ToBool => RawValue;

        private JsonBool(bool value) : base(value) { }

        public override string ToString(bool _) => RawValue ? "true" : "false";

        public override bool Equals(bool other) => RawValue == other;
    }
}
