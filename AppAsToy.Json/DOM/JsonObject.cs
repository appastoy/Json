using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8764
#pragma warning disable CS8766

namespace AppAsToy.Json.DOM
{
    public sealed class JsonObject : JsonElement, IJsonObject
    {
        private List<JsonProperty> _properties;
        private Dictionary<string, JsonElement> _propertyMap;

        public override JsonElementType Type => JsonElementType.Object;

        public IReadOnlyList<JsonProperty> Properties => _properties;

        public override JsonElement? this[string name]
        {
            get => _propertyMap.TryGetValue(name, out var value) ? value : Null;
            set
            {
                var index = _properties.FindIndex(p => p.Name == name);
                if (index < 0)
                    _properties.Add(new JsonProperty(name, value ?? Null));
                else
                    _properties[index] = new JsonProperty(name, value ?? Null);
                _propertyMap[name] = value ?? Null;
            }
        }

        public override int Count => _properties.Count;

        public override ICollection<string> Keys => _propertyMap.Keys;

        public override ICollection<JsonElement> Values => _propertyMap.Values;

        public JsonObject()
        {
            _properties = new List<JsonProperty>();
            _propertyMap = new Dictionary<string, JsonElement>();
        }

        public JsonObject(params JsonProperty[] properties)
        {
            _properties = properties.Select(p => new JsonProperty(p.Name, p.Value ?? Null)).ToList();
            _propertyMap = _properties.ToDictionary(p => p.Name, p => p.Value);
        }

        public JsonObject(IEnumerable<JsonProperty> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            _properties = properties.ToList();
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

        public override void Add(string key, JsonElement value)
        {
            if (_propertyMap.ContainsKey(key))
                throw new ArgumentException($"key({key}) is already exists.");

            _propertyMap.Add(key, value ?? Null);
            _properties.Add(new JsonProperty(key, value ?? Null));
        }
        public override bool Remove(string key)
        {
            if (_propertyMap.Remove(key))
            {
                _properties.RemoveAll(p => p.Name == key);
                return true;
            }
            return false;
        }
        protected override void Add(KeyValuePair<string, JsonElement> item)
        {
            Add(item.Key, item.Value);
        }
        protected override bool Contains(KeyValuePair<string, JsonElement> item)
        {
            return TryGetValue(item.Key, out var foundItem) && item.Value == foundItem;
        }
        protected override void CopyTo(KeyValuePair<string, JsonElement>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, JsonElement>>)_propertyMap).CopyTo(array, arrayIndex);
        }
        protected override bool Remove(KeyValuePair<string, JsonElement> item)
        {
            if (((ICollection<KeyValuePair<string, JsonElement>>)_propertyMap).Remove(item))
            {
                _properties.RemoveAll(p => p.Name == item.Key);
                return true;
            }
            return false;
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
                @object.Count == _properties.Count &&
                @object._properties.SequenceEqual(_properties);
        }

        public static implicit operator JsonObject(JsonProperty[] properties) => new JsonObject(properties);
    }
}
