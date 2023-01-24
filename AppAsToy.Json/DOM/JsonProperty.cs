using System;

namespace AppAsToy.Json.DOM
{
    public readonly struct JsonProperty : IEquatable<JsonProperty>
    {
        public string Name { get; }

        public JsonElement Value { get; }

        public JsonProperty(string name, JsonElement? value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? JsonElement.Null;
        }

        public bool Equals(JsonProperty other)
            => Name == other.Name && Value.Equals(other.Value);
    }
}
