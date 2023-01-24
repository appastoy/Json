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
    double? asDouble { get; }
    float? asFloat { get; }
    sbyte? asSByte { get; }
    short? asShort { get; }
    int? asInt { get; }
    long? asLong { get; }
    byte? asByte { get; }
    ushort? asUShort { get; }
    uint? asUInt { get; }
    ulong? asULong { get; }
    decimal? asDecimal { get; }
    string? asString { get; }
    bool? asBool { get; }
    DateTime? asDateTime { get; }
    DateTimeOffset? asDateTimeOffset { get; }
    TimeSpan? asTimeSpan { get; }
    Guid? asGuid { get; }
    byte[]? asByteArray { get; }

    double toDouble { get; }
    float toFloat { get; }
    sbyte toSByte { get; }
    short toShort { get; }
    int toInt { get; }
    long toLong { get; }
    byte toByte { get; }
    ushort toUShort { get; }
    uint toUInt { get; }
    ulong toULong { get; }
    decimal toDecimal { get; }
    string toString { get; }
    bool toBool { get; }
    DateTime toDateTime { get; }
    DateTimeOffset toDateTimeOffset { get; }
    TimeSpan toTimeSpan { get; }
    Guid toGuid { get; }
    byte[] toByteArray { get; }
}
