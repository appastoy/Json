using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS8764

namespace AppAsToy.Json.DOM
{
    public sealed class JsonObject : JsonElement
    {
        public static IJsonElement Empty { get; } = new JsonObject();

        internal List<JsonProperty> _properties;
        private Dictionary<string, JsonElement> _propertyMap;

        public override JsonElementType Type => JsonElementType.Object;

        public override JsonElement? this[string key]
        {
            get => _propertyMap.TryGetValue(key, out var value) ? value : Null;
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var index = _properties.FindIndex(p => p.Key == key);
                if (index < 0)
                    _properties.Add(new JsonProperty(key, value ?? Null));
                else
                    _properties[index] = new JsonProperty(key, value ?? Null);
                _propertyMap[key] = value ?? Null;
            }
        }
        public override int Count => _properties.Count;
        public override IEnumerable<string> Keys => _propertyMap.Keys;
        public override IEnumerable<JsonElement> Values => _propertyMap.Values;

        public JsonObject()
        {
            _properties = new List<JsonProperty>();
            _propertyMap = new Dictionary<string, JsonElement>();
        }

        public JsonObject(params JsonProperty[] properties)
        {
            _properties = properties.Select(p => new JsonProperty(p.Key, p.Value ?? Null)).ToList();
            _propertyMap = _properties.ToDictionary(p => p.Key, p => p.Value);
        }

        public JsonObject(IEnumerable<JsonProperty> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            _properties = properties.ToList();
            _propertyMap = _properties.ToDictionary(p => p.Key, p => p.Value);
        }

        public override bool ContainsKey(string key) => _propertyMap.ContainsKey(key);
        public override bool TryGetValue(string key, out JsonElement value) => _propertyMap.TryGetValue(key, out value);
        public override bool TryAdd(string key, JsonElement value)
        {
            if (_propertyMap.TryAdd(key, value))
            {
                _properties.Add(new JsonProperty(key, value));
                return true;
            }
            return false;
        }
        public override void Add(string key, JsonElement value)
        {
            _propertyMap.Add(key, value);
            _properties.Add(new JsonProperty(key, value));
        }

        public override bool Remove(string key)
        {
            if (_propertyMap.Remove(key))
            {
                _properties.RemoveAt(_properties.FindIndex(item => item.Key == key));
                return true;
            }
            return false;
        }

        public override void Clear()
        {
            _properties.Clear();
            _propertyMap.Clear();
        }

        public override string ToString(bool writeIndented)
        {
            return new JsonElementSerializer(writeIndented).Serialize(this);
        }

        protected override bool IsEqual(JsonElement? element)
        {
            return element is JsonObject @object &&
                @object.Count == _properties.Count &&
                @object._properties.SequenceEqual(_properties);
        }
        
        public override ArrayEnumerator<JsonElement> GetEnumerator() => new(_properties, _properties.Count);
        protected override ArrayEnumerator<IJsonElement> GetReadOnlyEnumerator() => new(_properties, _properties.Count);
        protected override IEnumerator<IJsonElement> GetDefaultEnumerator() => ((IEnumerable<JsonProperty>)_properties).GetEnumerator();
        protected override IEnumerator GetBaseEnumerator() => ((IEnumerable)_properties).GetEnumerator();

        public override int GetHashCode()
        {
            return HashCode.Combine(_properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JsonObject(JsonProperty[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JsonObject() : new JsonObject(value);
        }
    }
}
