using System;

namespace AppAsToy.Json;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class JIgnoreAttribute : Attribute
{
}
