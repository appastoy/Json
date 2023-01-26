using System;
using System.Globalization;

namespace AppAsToy.Json;
public interface IJCastable :
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
    IEquatable<ulong>,
    IEquatable<bool>,
    IEquatable<string>,
    IEquatable<DateTime>,
    IEquatable<DateTimeOffset>,
    IEquatable<TimeSpan>,
    IEquatable<Guid>,
    IEquatable<byte[]>
{
    double? AsDouble { get; }
    float? AsFloat { get; }
    sbyte? AsSByte { get; }
    short? AsShort { get; }
    int? AsInt { get; }
    long? AsLong { get; }
    byte? AsByte { get; }
    ushort? AsUShort { get; }
    uint? AsUInt { get; }
    ulong? AsULong { get; }
    decimal? AsDecimal { get; }
    string? AsString { get; }
    bool? AsBool { get; }
    byte[]? AsByteArray { get; }
    DateTime? AsDateTime(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None);
    DateTimeOffset? AsDateTimeOffset(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None);
    TimeSpan? AsTimeSpan(string? format = null, IFormatProvider? formatProvider = null, TimeSpanStyles timeSpanStyles = TimeSpanStyles.None);
    Guid? AsGuid(string? format = null);
    

    double ToDouble { get; }
    float ToFloat { get; }
    sbyte ToSByte { get; }
    short ToShort { get; }
    int ToInt { get; }
    long ToLong { get; }
    byte ToByte { get; }
    ushort ToUShort { get; }
    uint ToUInt { get; }
    ulong ToULong { get; }
    decimal ToDecimal { get; }
    string ToStringValue { get; }
    bool ToBool { get; }
    byte[] ToByteArray { get; }
    DateTime ToDateTime(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None);
    DateTimeOffset ToDateTimeOffset(string? format = null, IFormatProvider? formatProvider = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None);
    TimeSpan ToTimeSpan(string? format = null, IFormatProvider? formatProvider = null, TimeSpanStyles timeSpanStyles = TimeSpanStyles.None);
    Guid ToGuid(string? format = null);
}
