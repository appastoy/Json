using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS0659
#pragma warning disable CS0661
#pragma warning disable CS8625

namespace AppAsToy.Json.DOM
{

    public abstract class JsonElement : IJsonElement, IList<JsonElement>, IDictionary<string, JsonElement>
    {
        public static JsonElement Null { get; } = JsonNull.Instance;
        public abstract JsonElementType Type { get; }
        public virtual int Count => 0;
        public bool IsReadOnly => false;
        public virtual ICollection<string> Keys => Array.Empty<string>();
        public virtual ICollection<JsonElement> Values => Array.Empty<JsonElement>();
        IEnumerable<string> IReadOnlyDictionary<string, JsonElement>.Keys => Keys;
        IEnumerable<JsonElement> IReadOnlyDictionary<string, JsonElement>.Values => Values;

        public virtual JsonElement this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public virtual JsonElement this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public virtual bool ContainsKey(string key) => false;
        public virtual bool TryGetValue(string key, out JsonElement value) { value = default; return false; }
        public virtual string ToString(bool writeIndented) => ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(bool value) => value ? JsonBool.True : JsonBool.False;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(string value) => value == null ? Null : new JsonString(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(double value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(float value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(sbyte value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(short value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(int value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(long value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(byte value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(ushort value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(uint value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(ulong value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(decimal value) => new JsonNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JsonElement(JsonElement?[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JsonArray() : new JsonArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JsonElement(JsonProperty[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JsonObject() : new JsonObject(value);
        }

        protected abstract bool IsEqual(JsonElement? element);
        public bool Equals(JsonElement? other) => IsEqual(other);
        public override bool Equals(object other) =>
            (other == null && Type == JsonElementType.Null) ||
            (other is JsonElement element && Equals(element));
        public virtual bool Equals(string? other) => false;
        public virtual bool Equals(bool other) => false;
        public virtual bool Equals(float other) => false;
        public virtual bool Equals(double other) => false;
        public virtual bool Equals(decimal other) => false;
        public virtual bool Equals(sbyte other) => false;
        public virtual bool Equals(short other) => false;
        public virtual bool Equals(int other) => false;
        public virtual bool Equals(long other) => false;
        public virtual bool Equals(byte other) => false;
        public virtual bool Equals(ushort other) => false;
        public virtual bool Equals(uint other) => false;
        public virtual bool Equals(ulong other) => false;

        public virtual ArrayEnumerator<JsonElement> GetEnumerator()
            => new ArrayEnumerator<JsonElement>(Array.Empty<JsonElement>());

        IEnumerator<JsonElement> IEnumerable<JsonElement>.GetEnumerator() => Enumerable.Empty<JsonElement>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Enumerable.Empty<JsonElement>().GetEnumerator();

        IEnumerator<KeyValuePair<string, JsonElement>> IEnumerable<KeyValuePair<string, JsonElement>>.GetEnumerator()
            => Enumerable.Empty<KeyValuePair<string, JsonElement>>().GetEnumerator();

        public static bool Equals(JsonElement? left, JsonElement? right)
        {
            var leftNull = ReferenceEquals(left, null) || left is JsonNull;
            var rightNull = ReferenceEquals(right, null) || right is JsonNull;

            if (leftNull != rightNull)
                return false;

            if (leftNull && rightNull)
                return true;

            return left!.Equals(right);
        }

        public virtual int IndexOf(JsonElement item) => throw new NotImplementedException();
        public virtual void Insert(int index, JsonElement item) => throw new NotImplementedException();
        public virtual void RemoveAt(int index) => throw new NotImplementedException();
        public virtual void Add(JsonElement item) => throw new NotImplementedException();
        public virtual void Clear() => throw new NotImplementedException();
        public virtual bool Contains(JsonElement item) => throw new NotImplementedException();
        public virtual void CopyTo(JsonElement[] array, int arrayIndex) => throw new NotImplementedException();
        public virtual bool Remove(JsonElement item) => throw new NotImplementedException();

        public virtual void Add(string key, JsonElement value) => throw new NotImplementedException();
        public virtual bool Remove(string key) => throw new NotImplementedException();
        protected virtual void Add(KeyValuePair<string, JsonElement> item) => throw new NotImplementedException();
        protected virtual bool Contains(KeyValuePair<string, JsonElement> item) => throw new NotImplementedException();
        protected virtual void CopyTo(KeyValuePair<string, JsonElement>[] array, int arrayIndex) => throw new NotImplementedException();
        protected virtual bool Remove(KeyValuePair<string, JsonElement> item) => throw new NotImplementedException();

        void ICollection<KeyValuePair<string, JsonElement>>.Add(KeyValuePair<string, JsonElement> item) => Add(item);
        bool ICollection<KeyValuePair<string, JsonElement>>.Contains(KeyValuePair<string, JsonElement> item) => Contains(item);
        void ICollection<KeyValuePair<string, JsonElement>>.CopyTo(KeyValuePair<string, JsonElement>[] array, int arrayIndex) => CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<string, JsonElement>>.Remove(KeyValuePair<string, JsonElement> item) => Remove(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, JsonElement? right) => Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, float right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, double right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, decimal right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, sbyte right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, short right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, int right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, long right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, byte right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, ushort right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, uint right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, ulong right) => left?.Equals(right) ?? false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, JsonElement? right) => !Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, float right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, double right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, decimal right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, sbyte right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, short right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, int right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, long right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, byte right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, ushort right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, uint right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, ulong right) => !(left?.Equals(right) ?? false);
    }
}
