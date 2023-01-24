using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8764
#pragma warning disable CS8766

namespace AppAsToy.Json.DOM
{
    public sealed class JsonArray : JsonElement, IReadOnlyList<JsonElement>
    {
        public static JsonArray Empty { get; } = new JsonArray(Array.Empty<JsonElement>());

        private JsonElement[] _items;

        public override JsonElementType Type => JsonElementType.Array;

        public override JsonElement? this[int index]
        {
            get => _items[index];
            init
            {
                if (index < 0 || index > _items.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range.");

                if (index == _items.Length)
                    Array.Resize(ref _items, _items.Length + 1);
                _items[index] = value ?? Null;
            }
        }

        public override int Count => _items.Length;

        public JsonArray()
        {
            _items = Array.Empty<JsonElement>();
        }

        public JsonArray(params JsonElement?[] items)
        {
            _items = items.Select(item => item ?? Null).ToArray();
        }

        public JsonArray(IEnumerable<JsonElement?> items)
        {
            _items = items?.Select(item => item ?? Null).ToArray() 
                ?? throw new ArgumentNullException(nameof(items));
        }

        public override ArrayEnumerator<JsonElement> GetEnumerator()
        {
            return new ArrayEnumerator<JsonElement>(_items);
        }

        IEnumerator<JsonElement> IEnumerable<JsonElement>.GetEnumerator()
        {
            return ((IEnumerable<JsonElement>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
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
            return element is JsonArray array &&
                array.Count == _items.Length &&
                array._items.SequenceEqual(_items);
        }

        public static implicit operator JsonArray(JsonElement?[] elements) => new JsonArray(elements);
    }
}
