using System;

namespace AppAsToy.Json.DOM;

public abstract class JsonValue<T> : JsonElement
{
    public readonly T RawValue;

    public JsonValue(T rawValue) => RawValue = rawValue;

    protected override bool IsEqual(JsonElement? element)
        => element is JsonValue<T> value && value.Equals(RawValue!);

    public override int GetHashCode() => RawValue!.GetHashCode();
}