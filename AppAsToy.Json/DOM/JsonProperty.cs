using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonProperty : JsonElement
    {
        private JsonElement _value;

        public override JsonElementType Type => JsonElementType.Property;

        public override string Key { get; }

        public override JsonElement Value
        {
            get => _value;
            set => _value = value ?? Null;
        }

        public JsonProperty(string key, JsonElement? value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            _value = value ?? Null;
        }

        public override string ToString(bool writeIndented)
        {
            return new JsonElementSerializer(writeIndented).Serialize(this);
        }

        protected override bool IsEqual(JsonElement? element) =>
            element is JsonProperty property &&
            Key == property.Key &&
            Value.Equals(property.Value);

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }
    }
}