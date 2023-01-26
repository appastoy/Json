using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS8764

namespace AppAsToy.Json
{
    public sealed class JArray : JElement
    {
        public static IReadOnlyJElement Empty { get; } = new JArray();

        private readonly List<JElement> _items;

        public override JElementType Type => JElementType.Array;

        public override JElement? this[int index]
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

        public JArray()
        {
            _items = new List<JElement>();
        }

        public JArray(params JElement?[] items)
        {
            _items = items.Select(item => item ?? Null).ToList();
        }

        public JArray(IEnumerable<JElement?> items)
        {
            _items = items?.Select(item => item ?? Null).ToList()
                ?? throw new ArgumentNullException(nameof(items));
        }

        public override bool Contains(IReadOnlyJElement? item) => _items.Contains((item as JElement) ?? Null);
        public override int IndexOf(IReadOnlyJElement? item) => _items.IndexOf((item as JElement) ?? Null);
        public override int LastIndexOf(IReadOnlyJElement? item) => _items.LastIndexOf((item as JElement) ?? Null);
        public override int FindIndex(Predicate<IReadOnlyJElement> func) => _items.FindIndex(func);
        public override int FindLastIndex(Predicate<IReadOnlyJElement> func) => _items.FindLastIndex(func);
        public override JElement Find(Predicate<IReadOnlyJElement> func) => _items.Find(func);
        public override JElement FindLast(Predicate<IReadOnlyJElement> func) => _items.FindLast(func);
        public override void Add(JElement element) => _items.Add(element);
        public override void AddRange(IEnumerable<JElement> elements) => _items.AddRange(elements);
        public override void Insert(int index, JElement element) => _items.Insert(index, element);
        public override bool Remove(JElement element) => _items.Remove(element);
        public override void RemoveAt(int index) => _items.RemoveAt(index);
        public override void RemoveRange(int index, int count) => _items.RemoveRange(index, count);
        public override int RemoveAll(Predicate<IReadOnlyJElement> func) => _items.RemoveAll(func);
        public override void Clear() => _items.Clear();

        public override string Serialize(bool writeIndent = true, string? numberFormat = null, bool escapeUnicode = false)
        {
            return new JElementSerializer(writeIndent, numberFormat, escapeUnicode).Serialize(this);
        }

        protected override bool IsEqual(JElement? element)
        {
            return element is JArray array &&
                array.Count == _items.Count &&
                array._items.SequenceEqual(_items);
        }

        public override ArrayEnumerator<JElement> GetEnumerator() => new(_items, _items.Count);
        protected override ArrayEnumerator<IReadOnlyJElement> GetReadOnlyEnumerator() => new(_items, _items.Count);
        protected override IEnumerator<IReadOnlyJElement> GetDefaultEnumerator() => ((IEnumerable<JElement>)_items).GetEnumerator();
        protected override IEnumerator GetBaseEnumerator() => ((IEnumerable)_items).GetEnumerator();

        public override int GetHashCode()
        {
            return HashCode.Combine(_items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JArray(JElement?[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JArray() : new JArray(value);
        }
    }
}
