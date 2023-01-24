using System;
using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonElement :
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
    JsonElementType Type { get; }
    new ArrayEnumerator<JsonElement> GetEnumerator();
    string ToString(bool writeIndented);
}