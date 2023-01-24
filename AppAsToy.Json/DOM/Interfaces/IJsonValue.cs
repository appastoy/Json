using System;

namespace AppAsToy.Json.DOM;
public interface IJsonValue :
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
    DateTime? AsDateTime { get; }
    DateTimeOffset? AsDateTimeOffset { get; }
    TimeSpan? AsTimeSpan { get; }
    Guid? AsGuid { get; }
    byte[]? AsByteArray { get; }

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
    DateTime ToDateTime { get; }
    DateTimeOffset ToDateTimeOffset { get; }
    TimeSpan ToTimeSpan { get; }
    Guid ToGuid { get; }
    byte[] ToByteArray { get; }
}
