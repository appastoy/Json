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

    public abstract class JsonElement :
        IReadOnlyList<JsonElement>,
        IReadOnlyDictionary<string, JsonElement>,
        IEquatable<JsonElement>,
        IEquatable<string>,
        IEquatable<bool>,
        IEquatable<float>,
        IEquatable<double>,
        IEquatable<decimal>,
        IEquatable<sbyte>,
        IEquatable<short>,
        IEquatable<int>,
        IEquatable<long>,
        IEquatable<byte>,
        IEquatable<ushort>,
        IEquatable<uint>,
        IEquatable<ulong>
    {
        public static JsonElement Null { get; } = JsonNull.Instance;
        public abstract JsonElementType Type { get; }

        public virtual int Count => 0;
        public virtual IEnumerable<string> Keys => Enumerable.Empty<string>();
        public virtual IEnumerable<JsonElement> Values => Enumerable.Empty<JsonElement>();
        public virtual JsonElement this[string key]
        {
            get => throw new NotImplementedException();
            init => throw new NotImplementedException();
        }

        public virtual JsonElement this[int index]
        {
            get => throw new NotImplementedException();
            init => throw new NotImplementedException();
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

            return value.Length == 0 ? JsonArray.Empty : new JsonArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JsonElement(JsonProperty[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? JsonObject.Empty : new JsonObject(value);
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
