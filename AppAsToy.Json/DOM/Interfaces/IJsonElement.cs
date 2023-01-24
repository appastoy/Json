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
    bool isNull { get; }
    bool isBool { get; }
    bool isString { get; }
    bool isNumber { get; }
    bool isDateTime { get; }
    bool isDateTimeOffset { get; }
    bool isTimeSpan { get; }
    bool isGuid { get; }
    bool isByteArray { get; }
    bool isArray { get; }
    bool isObject { get; }
    bool isProperty { get; }
}