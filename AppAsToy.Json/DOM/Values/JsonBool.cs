namespace AppAsToy.Json.DOM
{
    public sealed class JsonBool : JsonElement
    {
        public static JsonBool True { get; } = new JsonBool(true);
        public static JsonBool False { get; } = new JsonBool(false);

        public override JsonElementType Type => JsonElementType.Bool;

        public readonly bool RawValue;

        public override bool? asBool => RawValue;
        public override bool toBool => RawValue;

        private JsonBool(bool value) => RawValue = value;

        public override string ToString(bool _) => RawValue ? "true" : "false";

        protected override bool IsEqual(JsonElement? element) 
            => element is JsonBool jsonBool && jsonBool.RawValue == RawValue;

        public override bool Equals(bool other) => RawValue == other;
        public override int GetHashCode()
        {
            return RawValue.GetHashCode();
        }
    }
}
