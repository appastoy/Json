using System;
using System.Collections.Generic;
using System.Linq;

namespace AppAsToy.Json.Helpers;
internal static class TypeHelper
{
    public static readonly IReadOnlyList<Type> AllTypes;
    public static readonly IReadOnlyList<Type> ConcreteClassTypes;

    static TypeHelper()
    {
        AllTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => !a.IsDynamic && !a.ReflectionOnly && !a.GetName().Name.StartsWith("System."))
            .SelectMany(a => a.GetTypes())
            .ToArray();
        ConcreteClassTypes = AllTypes
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
            .ToArray();
    }
}
