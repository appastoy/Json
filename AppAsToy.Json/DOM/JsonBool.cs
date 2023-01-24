namespace AppAsToy.Json.DOM
{
    public sealed class JsonBool : JsonElement
    {
        public static JsonBool True { get; } = new JsonBool(true);
        public static JsonBool False { get; } = new JsonBool(false);

        public bool Value { get; }

        public override JsonElementType Type => JsonElementType.Bool;

        private JsonBool(bool value) => Value = value;

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        protected override bool IsEqual(JsonElement? element) 
            => element is JsonBool jsonBool && jsonBool.Value == Value;

        public override bool Equals(bool other) => Value == other;
    }
}
