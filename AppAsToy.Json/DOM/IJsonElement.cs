using System;
using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonElement :
    IReadOnlyList<IJsonElement>,
    IReadOnlyDictionary<string, IJsonElement>,
    IEquatable<IJsonElement>,
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
    JsonElementType Type { get; }
    IJsonArray? asArray { get; }
    IJsonObject? asObject { get; }
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

    IJsonArray toArray { get; }
    IJsonObject toObject { get; }
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

    bool IsNull => Type == JsonElementType.Null;
    bool IsBool => Type == JsonElementType.Bool;
    bool IsString => Type == JsonElementType.String;
    bool IsNumber => Type == JsonElementType.Number;
    bool IsArray => Type == JsonElementType.Array;
    bool IsObject => Type == JsonElementType.Object;

    string ToString(bool writeIndented);
}