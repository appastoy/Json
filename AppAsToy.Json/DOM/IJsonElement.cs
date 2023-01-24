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
    IJsonArray? AsArray { get; }
    IJsonObject? AsObject { get; }
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

    IJsonArray Array { get; }
    IJsonObject Object { get; }
    double Double { get; }
    float Float { get; }
    sbyte SByte { get; }
    short Short { get; }
    int Int { get; }
    long Long { get; }
    byte Byte { get; }
    ushort UShort { get; }
    uint UInt { get; }
    ulong ULong { get; }
    decimal Decimal { get; }
    string String { get; }
    bool Bool { get; }

    bool IsNull => Type == JsonElementType.Null;
    bool IsBool => Type == JsonElementType.Bool;
    bool IsString => Type == JsonElementType.String;
    bool IsNumber => Type == JsonElementType.Number;
    bool IsArray => Type == JsonElementType.Array;
    bool IsObject => Type == JsonElementType.Object;

    string ToString(bool writeIndented);
}