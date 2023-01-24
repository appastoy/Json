using System;

namespace AppAsToy.Json.DOM;
public interface IJsonElement : 
    IEquatable<IJsonElement>, 
    IJsonArray,
    IJsonObject,
    IJsonProperty,
    IJsonValue
{
    JsonElementType Type { get; }
    string ToString(bool writeIndented);
    bool IsNull { get; }
    bool IsBool { get; }
    bool IsString { get; }
    bool IsNumber { get; }
    bool IsArray { get; }
    bool IsObject { get; }
    bool IsProperty { get; }
}