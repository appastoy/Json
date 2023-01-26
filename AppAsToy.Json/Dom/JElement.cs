using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

#pragma warning disable CS0659
#pragma warning disable CS0661
#pragma warning disable CS8625

namespace AppAsToy.Json
{
    public abstract class JElement : IReadOnlyJElement
    {
        #region Static Stuffs

        public static JElement Null => JNull.Shared;

        public static bool TryParse(string json, out JElement element)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));

            try 
            {
                element = Parse(json);
                return true;
            }
            catch
            {
                element = default;
                return false;
            }
        }

        public static JElement Parse(string json)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));

            if (json.Length == 0)
                throw new ArgumentException("json is empty.");

            return new JElementParser(json).Parse();
        }

        #endregion Static Stuffs

        #region Common Interfaces

        public abstract JElementType Type { get; }
        
        public bool IsNull => Type == JElementType.Null;
        public bool IsBool => Type == JElementType.Bool;
        public bool IsString => Type == JElementType.String;
        public bool IsNumber => Type == JElementType.Number;
        public bool IsArray => Type == JElementType.Array;
        public bool IsObject => Type == JElementType.Object;
        public bool IsProperty => Type == JElementType.Property;
        public abstract string Serialize(bool writeIndent = true, string? numberFormat = null, bool escapeUnicode = false);
        public override string ToString() => Serialize(true);
        public IReadOnlyJElement ToReadOnly() => this;

        #endregion Common Interfaces

        #region Collection Interfaces

        public virtual void Clear() => throw new NotImplementedException();
        public virtual ArrayEnumerator<JElement> GetEnumerator() => new(Array.Empty<JElement>(), 0);
        protected virtual ArrayEnumerator<IReadOnlyJElement> GetReadOnlyEnumerator() => new(Array.Empty<IReadOnlyJElement>(), 0);
        protected virtual IEnumerator<IReadOnlyJElement> GetDefaultEnumerator() => ((IEnumerable<IReadOnlyJElement>)Array.Empty<IReadOnlyJElement>()).GetEnumerator();
        protected virtual IEnumerator GetBaseEnumerator() => Array.Empty<IReadOnlyJElement>().GetEnumerator();
        ArrayEnumerator<IReadOnlyJElement> IReadOnlyJCollection.GetEnumerator() => GetReadOnlyEnumerator();
        IEnumerator<IReadOnlyJElement> IEnumerable<IReadOnlyJElement>.GetEnumerator() => GetDefaultEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetBaseEnumerator();

        #endregion Collection Interfaces

        #region Array Interfaces

        public virtual int Count => 0;
        public virtual JElement this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual bool Contains(IReadOnlyJElement? item) => throw new NotImplementedException();
        public virtual int IndexOf(IReadOnlyJElement? element) => throw new NotImplementedException();
        public virtual int LastIndexOf(IReadOnlyJElement? element) => throw new NotImplementedException();
        public virtual int FindIndex(Predicate<IReadOnlyJElement> func) => throw new NotImplementedException();
        public virtual int FindLastIndex(Predicate<IReadOnlyJElement> func) => throw new NotImplementedException();
        public virtual JElement Find(Predicate<IReadOnlyJElement> func) => throw new NotImplementedException();
        public virtual JElement FindLast(Predicate<IReadOnlyJElement> func) => throw new NotImplementedException();
        IReadOnlyJElement IReadOnlyList<IReadOnlyJElement>.this[int index] => this[index];
        IReadOnlyJElement IReadOnlyJArray.FindFirst(Predicate<IReadOnlyJElement> func) => Find(func);
        IReadOnlyJElement IReadOnlyJArray.FindLast(Predicate<IReadOnlyJElement> func) => FindLast(func);

        public virtual void Add(JElement element) => throw new NotImplementedException();
        public virtual void AddRange(IEnumerable<JElement> element) => throw new NotImplementedException();
        public virtual void Insert(int index, JElement element) => throw new NotImplementedException();
        public virtual bool Remove(JElement element) => throw new NotImplementedException();
        public virtual void RemoveAt(int index) => throw new NotImplementedException();
        public virtual void RemoveRange(int index, int count) => throw new NotImplementedException();
        public virtual int RemoveAll(Predicate<IReadOnlyJElement> func) => throw new NotImplementedException();

        #endregion Array Interfaces

        #region Object Interfaces

        public virtual JElement this[string name]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual string PropertyName => throw new NotImplementedException();
        public virtual JElement PropertyValue
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public virtual IEnumerable<string> PropertyNames => Array.Empty<string>();
        public virtual IEnumerable<JElement> PropertyValues => Array.Empty<JElement>();
        public virtual bool ContainsName(string name) => throw new NotImplementedException();
        public virtual bool TryGetValue(string name, out JElement value) => throw new NotImplementedException();
        IReadOnlyJElement IReadOnlyJObject.this[string name] => this[name];
        IReadOnlyJElement IReadOnlyJProperty.PropertyValue => PropertyValue;
        IEnumerable<IReadOnlyJElement> IReadOnlyJObject.PropertyValues => PropertyValues;
        bool IReadOnlyJObject.TryGetValue(string name, out IReadOnlyJElement value)
        {
            if (TryGetValue(name, out var typedValue))
            {
                value = typedValue;
                return true;
            }

            value = default;
            return false;
        }

        public virtual bool TryAdd(string name, JElement value) => throw new NotImplementedException();
        public virtual void Add(string name, JElement value) => throw new NotImplementedException();
        public virtual bool Remove(string name) => throw new NotImplementedException();

        #endregion Object Interfaces

        #region Casting Interfaces

        public virtual double? AsDouble => null;
        public virtual float? AsFloat => null;
        public virtual sbyte? AsSByte => null;
        public virtual short? AsShort => null;
        public virtual int? AsInt => null;
        public virtual long? AsLong => null;
        public virtual byte? AsByte => null;
        public virtual ushort? AsUShort => null;
        public virtual uint? AsUInt => null;
        public virtual ulong? AsULong => null;
        public virtual decimal? AsDecimal => null;
        public virtual string? AsString => null;
        public virtual bool? AsBool => null;
        public virtual byte[]? AsByteArray => null;
        public virtual DateTime? AsDateTime(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None) => null;
        public virtual DateTimeOffset? AsDateTimeOffset(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None) => null;
        public virtual TimeSpan? AsTimeSpan(string? format = null, IFormatProvider? formatProvider = null, TimeSpanStyles timeSpanStyles = TimeSpanStyles.None) => null;
        public virtual Guid? AsGuid(string? format = null) => null;
        

        public virtual double ToDouble => throw new NotImplementedException();
        public virtual float ToFloat => throw new NotImplementedException();
        public virtual sbyte ToSByte => throw new NotImplementedException();
        public virtual short ToShort => throw new NotImplementedException();
        public virtual int ToInt => throw new NotImplementedException();
        public virtual long ToLong => throw new NotImplementedException();
        public virtual byte ToByte => throw new NotImplementedException();
        public virtual ushort ToUShort => throw new NotImplementedException();
        public virtual uint ToUInt => throw new NotImplementedException();
        public virtual ulong ToULong => throw new NotImplementedException();
        public virtual decimal ToDecimal => throw new NotImplementedException();
        public virtual string ToStringValue => throw new NotImplementedException();
        public virtual bool ToBool => throw new NotImplementedException();
        public virtual byte[] ToByteArray => throw new NotImplementedException();
        public virtual DateTime ToDateTime(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None) => throw new NotImplementedException();
        public virtual DateTimeOffset ToDateTimeOffset(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None) => throw new NotImplementedException();
        public virtual TimeSpan ToTimeSpan(string? format = null, IFormatProvider? formatProvider = null, TimeSpanStyles timeSpanStyles = TimeSpanStyles.None) => throw new NotImplementedException();
        public virtual Guid ToGuid(string? format = null) => throw new NotImplementedException();

        #endregion Casting Interfaces

        #region IEquatable Interfaces

        bool IEquatable<IReadOnlyJElement>.Equals(IReadOnlyJElement other) => Equals(other as JElement);
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

        public bool Equals(JElement? other) => IsEqual(other);
        protected abstract bool IsEqual(JElement? element);
        public override bool Equals(object other)
        {
            if (other == null)
                return Type == JElementType.Null;

            return (other is JElement element && IsEqual(element)) ||
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(bool value) => value ? JBool.True : JBool.False;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(string value) => value == null ? Null : new JString(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(double value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(float value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(sbyte value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(short value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(int value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(long value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(byte value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(ushort value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(uint value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(ulong value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(decimal value) => new JNumber(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(DateTime value) => JString.FromDateTime(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(DateTimeOffset value) => JString.FromDateTimeOffset(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(TimeSpan value) => JString.FromTimeSpan(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(Guid value) => JString.FromGuid(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator JElement(byte[] value) => JString.FromByteArray(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JElement(JElement?[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JArray() : new JArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator JElement(JProperty[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Length == 0 ? new JObject() : new JObject(value);
        }

        #endregion Casting Operators

        #region Equality Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, JElement? right) => Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, float right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, double right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, decimal right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, sbyte right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, short right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, int right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, long right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, byte right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, ushort right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, uint right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, ulong right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, DateTime right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, DateTimeOffset right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, TimeSpan right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, Guid right) => left?.Equals(right) ?? false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(JElement? left, byte[] right) => left?.Equals(right) ?? false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, JElement? right) => !Equals(left, right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, float right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, double right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, decimal right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, sbyte right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, short right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, int right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, long right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, byte right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, ushort right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, uint right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, ulong right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, DateTime right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, DateTimeOffset right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, TimeSpan right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, Guid right) => !(left?.Equals(right) ?? false);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(JElement? left, byte[] right) => !(left?.Equals(right) ?? false);

        private static bool Equals(JElement? left, JElement? right)
        {
            var leftNull = left is null || left is JNull;
            var rightNull = right is null || right is JNull;

            if (leftNull != rightNull)
                return false;

            if (leftNull && rightNull)
                return true;

            return left!.Equals(right);
        }

        #endregion Equality Operators
    }
}
