namespace AppAsToy.Json.DOM
{
    internal sealed class JsonNull : JsonElement
    {
        internal static JsonNull Shared { get; } = new JsonNull();

        public override JsonElementType Type => JsonElementType.Null;

        private JsonNull() { }

        public override string ToString(bool _) => "null";

        protected override bool IsEqual(JsonElement? element) => ReferenceEquals(element, null) || element is JsonNull;
        public override bool Equals(string other) => other == null;
        public override bool Equals(byte[] other) => other == null;
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
