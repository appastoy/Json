using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8764
#pragma warning disable CS8766

namespace AppAsToy.Json.DOM
{
    public sealed class JsonObject : JsonElement, IReadOnlyDictionary<string, JsonElement>
    {
        public static JsonObject Empty { get; } = new JsonObject(Array.Empty<JsonProperty>());

        private JsonProperty[] _properties;
        private Dictionary<string, JsonElement> _propertyMap;

        public override JsonElementType Type => JsonElementType.Object;

        public IReadOnlyList<JsonProperty> Properties => _properties;

        public override JsonElement? this[string name]
        {
            get => _propertyMap.TryGetValue(name, out var value) ? value : Null;
            init
            {
                if (_propertyMap.ContainsKey(name))
                    throw new ArgumentException($"{name} key already exists.");

                _propertyMap.Add(name, value ?? Null);
                Array.Resize(ref _properties, _properties.Length + 1);
                _properties[^1] = new JsonProperty(name, value ?? Null);
            }
        }

        public override int Count => _properties.Length;

        public override IEnumerable<string> Keys => _propertyMap.Keys;

        public override IEnumerable<JsonElement> Values => _propertyMap.Values;

        public JsonObject()
        {
            _properties = Array.Empty<JsonProperty>();
            _propertyMap = new Dictionary<string, JsonElement>();
        }

        public JsonObject(params JsonProperty[] properties)
        {
            _properties = properties.Select(p => new JsonProperty(p.Name, p.Value ?? Null)).ToArray();
            _propertyMap = _properties.ToDictionary(p => p.Name, p => p.Value);
        }

        public JsonObject(IEnumerable<JsonProperty> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            _properties = properties.ToArray();
            _propertyMap = _properties.ToDictionary(p => p.Name, p => p.Value);
        }

        public override bool ContainsKey(string key)
        {
            return _propertyMap.ContainsKey(key);
        }

        public override bool TryGetValue(string key, out JsonElement value)
        {
            return _propertyMap.TryGetValue(key, out value);
        }

        IEnumerator<KeyValuePair<string, JsonElement>> IEnumerable<KeyValuePair<string, JsonElement>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, JsonElement>>)_propertyMap).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public override string ToString(bool writeIndented)
        {
            return new JsonSerializer(writeIndented).Serialize(this);
        }

        protected override bool IsEqual(JsonElement? element)
        {
            return element is JsonObject @object &&
                @object.Count == _properties.Length &&
                @object._properties.SequenceEqual(_properties);
        }

        public static implicit operator JsonObject(JsonProperty[] properties) => new JsonObject(properties);
    }
}
