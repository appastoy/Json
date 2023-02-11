using AppAsToy.Json.Conversion.Formatters;
using AppAsToy.Json.Helpers;
using System;

namespace AppAsToy.Json.Conversion.FormatterResolvers;
internal sealed class NullableResolver : IFormatterResolver
{
    public IFormatter<T>? Resolve<T>()
    {
        var type = typeof(T);
        if (!type.IsValueType || 
            !type.IsGenericType ||
            type.GetGenericTypeDefinition() != typeof(Nullable<>))
            return null;

        return typeof(NullableFormatter<>).GetGenericSharedFormatter<T>(type.GetGenericArguments());
    }
}
