using System;

namespace AppAsToy.Json;
public interface IReadOnlyJElement : 
    IEquatable<IReadOnlyJElement>, 
    IReadOnlyJArray,
    IReadOnlyJObject,
    IReadOnlyJProperty,
    IJCastable
{
    JElementType Type { get; }
    string Serialize(bool writeIndent = true, string? numberFormat = null, bool escapeUnicode = false);
    bool IsNull { get; }
    bool IsBool { get; }
    bool IsString { get; }
    bool IsNumber { get; }
    bool IsArray { get; }
    bool IsObject { get; }
    bool IsProperty { get; }
}