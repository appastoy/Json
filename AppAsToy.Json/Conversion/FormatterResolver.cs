using AppAsToy.Json.Conversion.FormatterResolvers;
using System;

namespace AppAsToy.Json.Conversion;
public static class FormatterResolver
{
    private static readonly IFormatterResolver[] _formatterProviders = new IFormatterResolver[]
    {
        new PrimitiveResolver(),
        new CollectionResolver(),
        new TupleResolver(),
    };

    public static IFormatter<T> Resolve<T>()
    {
        foreach (var provider in _formatterProviders)
        {
            var formatter = provider.Resolve<T>();
            if (formatter != null)
                return formatter;
        }

        throw new NotSupportedException($"{typeof(T).Name} type can't serialize or deserialize.");
    }
}
