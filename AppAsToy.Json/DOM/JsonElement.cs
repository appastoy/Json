using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS0659
#pragma warning disable CS0661
#pragma warning disable CS8625

namespace AppAsToy.Json.DOM
{
    public abstract class JsonElement : IJsonElement
    {
        public static JsonElement Null { get; } = JsonNull.Shared;

        #region Common Interfaces

        public abstract JsonElementType Type { get; }
        
        public bool isNull => Type == JsonElementType.Null;
        public bool isBool => Type == JsonElementType.Bool;
        public bool isString => Type == JsonElementType.String;
        public bool isNumber => Type == JsonElementType.Number;
        public bool isDateTime => Type == JsonElementType.DateTime;
        public bool isDateTimeOffset => Type == JsonElementType.DateTimeOffset;
        public bool isTimeSpan => Type == JsonElementType.TimeSpan;
        public bool isGuid => Type == JsonElementType.Guid;
        public bool isByteArray => Type == JsonElementType.ByteArray;
        public bool isArray => Type == JsonElementType.Array;
        public bool isObject => Type == JsonElementType.Object;
        public bool isProperty => Type == JsonElementType.Property;
        public abstract string ToString(bool writeIndented);
        public override string ToString() => ToString(true);

        #endregion Common Interfaces

        #region Collection Interfaces

        public virtual void Clear() => throw new NotImplementedException();
        public virtual ArrayEnumerator<JsonElement> GetEnumerator() => new(Array.Empty<JsonElement>(), 0);
        protected virtual ArrayEnumerator<IJsonElement> GetReadOnlyEnumerator() => new(Array.Empty<IJsonElement>(), 0);
        protected virtual IEnumerator<IJsonElement> GetDefaultEnumerator() => ((IEnumerable<IJsonElement>)Array.Empty<IJsonElement>()).GetEnumerator();
        protected virtual IEnumerator GetBaseEnumerator() => Array.Empty<IJsonElement>().GetEnumerator();
        ArrayEnumerator<IJsonElement> IJsonCollection.GetEnumerator() => GetReadOnlyEnumerator();
        IEnumerator<IJsonElement> IEnumerable<IJsonElement>.GetEnumerator() => GetDefaultEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetBaseEnumerator();


        #endregion Collection Interfaces

        #region Array Interfaces

        public virtual int Count => 0;
        public virtual JsonElement this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual bool Contains(IJsonElement? item) => throw new NotImplementedException();
        public virtual int IndexOf(IJsonElement? element) => throw new NotImplementedException();
        public virtual int LastIndexOf(IJsonElement? element) => throw new NotImplementedException();
        public virtual int FindIndex(Predicate<IJsonElement> func) => throw new NotImplementedException();
        public virtual int FindLastIndex(Predicate<IJsonElement> func) => throw new NotImplementedException();
        public virtual JsonElement Find(Predicate<IJsonElement> func) => throw new NotImplementedException();
        public virtual JsonElement FindLast(Predicate<IJsonElement> func) => throw new NotImplementedException();
        IJsonElement IReadOnlyList<IJsonElement>.this[int index] => this[index];
        IJsonElement IJsonArray.FindFirst(Predicate<IJsonElement> func) => Find(func);
        IJsonElement IJsonArray.FindLast(Predicate<IJsonElement> func) => FindLast(func);

        public virtual void Add(JsonElement element) => throw new NotImplementedException();
        public virtual void AddRange(IEnumerable<JsonElement> element) => throw new NotImplementedException();
        public virtual void Insert(int index, JsonElement element) => throw new NotImplementedException();
        public virtual bool Remove(JsonElement element) => throw new NotImplementedException();
        public virtual void RemoveAt(int index) => throw new NotImplementedException();
        public virtual void RemoveRange(int index, int count) => throw new NotImplementedException();
        public virtual int RemoveAll(Predicate<IJsonElement> func) => throw new NotImplementedException();

        #endregion Array Interfaces

        #region Object Interfaces

        public virtual JsonElement this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual string Key => throw new NotImplementedException();
        public virtual JsonElement Value
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual IEnumerable<string> Keys => Array.Empty<string>();
        public virtual IEnumerable<JsonElement> Values => Array.Empty<JsonElement>();
        public virtual bool ContainsKey(string key) => throw new NotImplementedException();
        public virtual bool TryGetValue(string key, out JsonElement value) => throw new NotImplementedException();
        IJsonElement IJsonObject.this[string key] => this[key];
        IJsonElement IJsonProperty.Value => Value;
        IEnumerable<IJsonElement> IJsonObject.Values => Values;
        bool IJsonObject.TryGetValue(string key, out IJsonElement value)
        {
            if (TryGetValue(key, out var typedValue))
            {
                value = typedValue;
                return true;
            }

            value = default;
            return false;
        }

        public virtual bool TryAdd(string key, JsonElement value) => throw new NotImplementedException();
        public virtual void Add(string key, JsonElement value) => throw new NotImplementedException();
        public virtual bool Remove(string key) => throw new NotImplementedException();

        #endregion Object Interfaces

        #region Casting Interfaces

        public virtual double? asDouble => null;
        public virtual float? asFloat => null;
        public virtual sbyte? asSByte => null;
        public virtual short? asShort => null;
        public virtual int? asInt => null;
        public virtual long? asLong => null;
        public virtual byte? asByte => null;
        public virtual ushort? asUShort => null;
        public virtual uint? asUInt => null;
        public virtual ulong? asULong => null;
        public virtual decimal? asDecimal => null;
        public virtual string? asString => null;
        public virtual bool? asBool => null;
        public virtual DateTime? asDateTime => null;
        public virtual DateTimeOffset? asDateTimeOffset => null;
        public virtual TimeSpan? asTimeSpan => null;
        public virtual Guid? asGuid => null;
        public virtual byte[]? asByteArray => null;

        public virtual double toDouble => throw new NotImplementedException();
        public virtual float toFloat => throw new NotImplementedException();
        public virtual sbyte toSByte => throw new NotImplementedException();
        public virtual short toShort => throw new NotImplementedException();
        public virtual int toInt => throw new NotImplementedException();
        public virtual long toLong => throw new NotImplementedException();
        public virtual byte toByte => throw new NotImplementedException();
        public virtual ushort toUShort => throw new NotImplementedException();
        public virtual uint toUInt => throw new NotImplementedException();
        public virtual ulong toULong => throw new NotImplementedException();
        public virtual decimal toDecimal => throw new NotImplementedException();
        public virtual string toString => throw new NotImplementedException();
        public virtual bool toBool => throw new NotImplementedException();
        public virtual DateTime toDateTime => throw new NotImplementedException();
        public virtual DateTimeOffset toDateTimeOffset => throw new NotImplementedException();
        public virtual TimeSpan toTimeSpan => throw new NotImplementedException();
        public virtual Guid toGuid => throw new NotImplementedException();
        public virtual byte[] toByteArray => throw new NotImplementedException();

        #endregion Casting Interfaces

        #region IEquatable Interfaces

        bool IEquatable<IJsonElement>.Equals(IJsonElement other) => Equals(other as JsonElement);
        public virtual bool Equals(string   other) => false;
        public virtual bool Equals(bool     other) => false;
        public virtual bool Equals(float    other) => false;
        public virtual bool Equals(double   other) => false;
        public virtual bool Equals(decimal  other) => false;
        public virtual bool Equals(sbyte    other) => false;
        public virtual bool Equals(short    other) => false;
        public virtual bool Equals(int      other) => false;
        public virtual bool Equals(long     other) => false;
        public virtual bool Equals(byte     other) => false;
        public virtual bool Equals(ushort   other) => false;
        public virtual bool Equals(uint     other) => false;
        public virtual bool Equals(ulong    other) => false;
        public virtual bool Equals(DateTime other) => false;
        public virtual bool Equals(DateTimeOffset other) => false;
        public virtual bool Equals(TimeSpan other) => false;
        public virtual bool Equals(Guid other) => false;
        public virtual bool Equals(byte[] other) => false;

        public bool Equals(JsonElement? other) => IsEqual(other);
        protected abstract bool IsEqual(JsonElement? element);
        public override bool Equals(object other)
        {
            if (other == null)
                return Type == JsonElementType.Null;

            return (other is JsonElement element && IsEqual(element)) ||
                (other is string  _string  && Equals(_string )) ||
                (other is bool    _bool    && Equals(_bool   )) ||
                (other is float   _float   && Equals(_float  )) ||
                (other is double  _double  && Equals(_double )) ||
                (other is decimal _decimal && Equals(_decimal)) ||
                (other is sbyte   _sbyte   && Equals(_sbyte  )) ||
                (other is short   _short   && Equals(_short  )) ||
                (other is int     _int     && Equals(_int    )) ||
                (other is long    _long    && Equals(_long   )) ||
                (other is byte    _byte    && Equals(_byte   )) ||
                (other is ushort  _ushort  && Equals(_ushort )) ||
                (other is uint    _uint    && Equals(_uint   )) ||
                (other is ulong   _ulong   && Equals(_ulong  )) ||
                (other is DateTime       _dateTime && Equals(_dateTime)) ||
                (other is DateTimeOffset _dateTimeOffset && Equals(_dateTimeOffset)) ||
                (other is TimeSpan       _timeSpan && Equals(_timeSpan)) ||
                (other is Guid           _guid && Equals(_guid)) ||
                (other is byte[]         _byteArray && Equals(_byteArray));
        }
            

        #endregion IEquatable Interfaces

        #region Casting Operators

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(DateTime value) => new JsonDateTime(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(DateTimeOffset value) => new JsonDateTimeOffset(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(TimeSpan value) => new JsonTimeSpan(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(Guid value) => new JsonGuid(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JsonElement(byte[] value) => new JsonByteArray(value);
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

        #endregion Casting Operators

        #region Equality Operators
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, DateTime right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, DateTimeOffset right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, TimeSpan right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, Guid right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JsonElement? left, byte[] right) => left?.Equals(right) ?? false;


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
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, DateTime right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, DateTimeOffset right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, TimeSpan right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, Guid right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JsonElement? left, byte[] right) => !(left?.Equals(right) ?? false);

        private static bool Equals(JsonElement? left, JsonElement? right)
        {
            var leftNull = left is null || left is JsonNull;
            var rightNull = right is null || right is JsonNull;

            if (leftNull != rightNull)
                return false;

            if (leftNull && rightNull)
                return true;

            return left!.Equals(right);
        }

        #endregion Equality Operators
    }
}
