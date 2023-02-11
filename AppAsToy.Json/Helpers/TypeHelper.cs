using AppAsToy.Json.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public static IFormatter<T>? GetSharedFormatter<T>(this Type sharedFormatterType)
    {
        return sharedFormatterType.GetField("Shared", BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public)
            ?.GetValue(null) as IFormatter<T>;
    }

    public static IFormatter<T>? GetGenericSharedFormatter<T>(this Type genericTypeDefinition, params Type[] genericArguments)
    {
        return genericTypeDefinition.MakeGenericType(genericArguments).GetSharedFormatter<T>();
    }
}
