using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS8764

namespace AppAsToy.Json.DOM
{
    public sealed class JsonArray : JsonElement
    {
        public static IJsonElement Empty { get; } = new JsonArray();

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


        public override bool Contains(IJsonElement? item) => _items.Contains((item as JsonElement) ?? Null);
        public override int IndexOf(IJsonElement? item) => _items.IndexOf((item as JsonElement) ?? Null);
        public override int LastIndexOf(IJsonElement? item) => _items.LastIndexOf((item as JsonElement) ?? Null);
        public override int FindIndex(Predicate<IJsonElement> func) => _items.FindIndex(func);
        public override int FindLastIndex(Predicate<IJsonElement> func) => _items.FindLastIndex(func);
        public override JsonElement Find(Predicate<IJsonElement> func) => _items.Find(func);
        public override JsonElement FindLast(Predicate<IJsonElement> func) => _items.FindLast(func);
        public override void Add(JsonElement element) => _items.Add(element);
        public override void AddRange(IEnumerable<JsonElement> elements) => _items.AddRange(elements);
        public override void Insert(int index, JsonElement element) => _items.Insert(index, element);
        public override bool Remove(JsonElement element) => _items.Remove(element);
        public override void RemoveAt(int index) => _items.RemoveAt(index);
        public override void RemoveRange(int index, int count) => _items.RemoveRange(index, count);
        public override int RemoveAll(Predicate<IJsonElement> func) => _items.RemoveAll(func);
        public override void Clear() => _items.Clear();

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

        public override ArrayEnumerator<JsonElement> GetEnumerator() => new(_items, _items.Count);
        protected override ArrayEnumerator<IJsonElement> GetReadOnlyEnumerator() => new(_items, _items.Count);
        protected override IEnumerator<IJsonElement> GetDefaultEnumerator() => ((IEnumerable<JsonElement>)_items).GetEnumerator();
        protected override IEnumerator GetBaseEnumerator() => ((IEnumerable)_items).GetEnumerator();

        public override int GetHashCode()
        {
            return HashCode.Combine(_items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JsonArray(JsonElement?[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JsonArray() : new JsonArray(value);
        }
    }
}
