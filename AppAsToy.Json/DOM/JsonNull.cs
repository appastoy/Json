namespace AppAsToy.Json.DOM
{
    internal sealed class JsonNull : JsonElement
    {
        internal static JsonNull Instance { get; } = new JsonNull();

        public override JsonElementType Type => JsonElementType.Null;

        private JsonNull() { }

        public override string ToString() => "null";

        protected override bool IsEqual(JsonElement? element) => object.ReferenceEquals(element, null) || element is JsonNull;
        public override bool Equals(string? other) => other == null;
    }
}
