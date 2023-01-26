using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS8764

namespace AppAsToy.Json
{
    public sealed class JObject : JElement
    {
        public static IReadOnlyJElement Empty => new JObject();

        internal List<JProperty> _properties;
        private Dictionary<string, JElement> _propertyMap;

        public override JElementType Type => JElementType.Object;

        public override JElement? this[string key]
        {
            get => _propertyMap.TryGetValue(key, out var value) ? value : Null;
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var index = _properties.FindIndex(p => p.PropertyName == key);
                if (index < 0)
                    _properties.Add(new JProperty(key, value ?? Null));
                else
                    _properties[index] = new JProperty(key, value ?? Null);
                _propertyMap[key] = value ?? Null;
            }
        }
        public override int Count => _properties.Count;
        public override IEnumerable<string> PropertyNames => _propertyMap.Keys;
        public override IEnumerable<JElement> PropertyValues => _propertyMap.Values;

        public JObject()
        {
            _properties = new List<JProperty>();
            _propertyMap = new Dictionary<string, JElement>();
        }

        public JObject(params JProperty[] properties)
        {
            _properties = properties.Select(p => new JProperty(p.PropertyName, p.PropertyValue ?? Null)).ToList();
            _propertyMap = _properties.ToDictionary(p => p.PropertyName, p => p.PropertyValue);
        }

        public JObject(IEnumerable<JProperty> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            _properties = properties.ToList();
            _propertyMap = _properties.ToDictionary(p => p.PropertyName, p => p.PropertyValue);
        }

        public override bool ContainsName(string name) => _propertyMap.ContainsKey(name);
        public override bool TryGetValue(string name, out JElement value) => _propertyMap.TryGetValue(name, out value);
        public override bool TryAdd(string name, JElement value)
        {
            if (_propertyMap.TryAdd(name, value))
            {
                _properties.Add(new JProperty(name, value));
                return true;
            }
            return false;
        }
        public override void Add(string name, JElement value)
        {
            _propertyMap.Add(name, value);
            _properties.Add(new JProperty(name, value));
        }

        public override bool Remove(string name)
        {
            if (_propertyMap.Remove(name))
            {
                _properties.RemoveAt(_properties.FindIndex(item => item.PropertyName == name));
                return true;
            }
            return false;
        }

        public override void Clear()
        {
            _properties.Clear();
            _propertyMap.Clear();
        }

        public override string Serialize(bool writeIndent = true, string? numberFormat = null, bool escapeUnicode = false)
        {
            return new JElementSerializer(writeIndent, numberFormat, escapeUnicode).Serialize(this);
        }

        protected override bool IsEqual(JElement? element)
        {
            return element is JObject @object &&
                @object.Count == _properties.Count &&
                @object._properties.SequenceEqual(_properties);
        }
        
        public override ArrayEnumerator<JElement> GetEnumerator() => new(_properties, _properties.Count);
        protected override ArrayEnumerator<IReadOnlyJElement> GetReadOnlyEnumerator() => new(_properties, _properties.Count);
        protected override IEnumerator<IReadOnlyJElement> GetDefaultEnumerator() => ((IEnumerable<JProperty>)_properties).GetEnumerator();
        protected override IEnumerator GetBaseEnumerator() => ((IEnumerable)_properties).GetEnumerator();

        public override int GetHashCode()
        {
            return HashCode.Combine(_properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JObject(JProperty[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JObject() : new JObject(value);
        }
    }
}
