using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8764
#pragma warning disable CS8766

namespace AppAsToy.Json.DOM
{
    public sealed class JsonArray : JsonElement, IJsonArray, IList<JsonElement>
    {
        public static IJsonArray Empty { get; } = new JsonArray();

        private readonly List<JsonElement> _items;

        public override JsonElementType Type => JsonElementType.Array;

        public override JsonElement? this[int index]
        {
            get => _items[index];
            set
            {
                if (index < 0 || index > _items.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range.");

                if (index == _items.Count)
                    _items.Add(value ?? Null);
                else
                    _items[index] = value ?? Null;
            }
        }

        public override int Count => _items.Count;

        public override JsonArray? AsArray => this;
        public override JsonArray Array => this;


        public JsonArray()
        {
            _items = new List<JsonElement>();
        }

        public JsonArray(params JsonElement?[] items)
        {
            _items = items.Select(item => item ?? Null).ToList();
        }

        public JsonArray(IEnumerable<JsonElement?> items)
        {
            _items = items?.Select(item => item ?? Null).ToList()
                ?? throw new ArgumentNullException(nameof(items));
        }

        public override int IndexOf(JsonElement item) => _items.IndexOf(item);
        public override void Insert(int index, JsonElement item) => _items.Insert(index, item);
        public override void RemoveAt(int index) => _items.RemoveAt(index);
        public override void Add(JsonElement item) => _items.Add(item);
        public override void Clear() => _items.Clear();
        public override bool Contains(JsonElement item) => _items.Contains(item);
        public override void CopyTo(JsonElement[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);
        public override bool Remove(JsonElement item) => _items.Remove(item);


        public ArrayEnumerator<JsonElement> GetEnumerator() => new ArrayEnumerator<JsonElement>(_items);

        ArrayEnumerator<IJsonElement> IJsonArray.GetEnumerator() => new ArrayEnumerator<IJsonElement>(_items);

        protected override IEnumerator<JsonElement> GetReferenceEnumerator()
        {
            return ((IEnumerable<JsonElement>)_items).GetEnumerator();
        }

        protected override IEnumerator GetBaseEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }

        public override string ToString() => ToString(true);

        public override string ToString(bool writeIndented)
        {
            return new JsonElementSerializer(writeIndented).Serialize(this);
        }

        protected override bool IsEqual(JsonElement? element)
        {
            return element is JsonArray array &&
                array.Count == _items.Count &&
                array._items.SequenceEqual(_items);
        }

        public static implicit operator JsonArray(JsonElement?[] elements) => new JsonArray(elements);
    }
}
