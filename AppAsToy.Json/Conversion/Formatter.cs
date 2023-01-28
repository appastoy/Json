using AppAsToy.Json.Helpers;
using System;
using System.Linq;

namespace AppAsToy.Json.Conversion;
internal static class Formatter<T>
{
    public static readonly IFormatter<T> Shared;

    static Formatter()
    {
        var formatterType = TypeHelper
            .ConcreteClassTypes
            .FirstOrDefault(t => typeof(IFormatter<T>).IsAssignableFrom(t));
        if (formatterType == null)
            throw new InvalidOperationException($"IFormatter<{typeof(T).Name}> type can't find.");

        Shared = (IFormatter<T>)Activator.CreateInstance(formatterType);
    }
}
