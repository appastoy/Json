using System;

namespace AppAsToy.Json;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class JPropertyInfoAttribute : Attribute
{
    public string? Name { get; set; }
    public bool Oneline { get; set; }
}
