using System;

namespace AppAsToy.Json;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class JObjectInfo : Attribute
{
    public bool IncludeTypeInfo { get; set; }
    public bool Oneline { get; set; }
}
