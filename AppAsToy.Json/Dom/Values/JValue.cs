using System;

namespace AppAsToy.Json;

public abstract class JValue<T> : JElement
{
    public readonly T RawValue;

    public JValue(T rawValue) => RawValue = rawValue;

    protected override bool IsEqual(JElement? element)
        => element is JValue<T> value && value.Equals(RawValue!);

    public override int GetHashCode() => RawValue!.GetHashCode();
}