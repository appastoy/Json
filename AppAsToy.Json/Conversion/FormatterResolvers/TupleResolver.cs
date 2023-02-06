using AppAsToy.Json.Conversion.Formatters.Shared;
using System;
using System.Runtime.CompilerServices;

namespace AppAsToy.Json.Conversion.FormatterResolvers;
internal sealed class TupleResolver : IFormatterResolver
{
   public IFormatter<T>? Resolve<T>()
    {
        var type = typeof(T);
        if (!type.IsGenericType)
            return null;

        if (!type.FullName.StartsWith("System.Tuple`") ||
            !typeof(ITuple).IsAssignableFrom(type))
            return null;

        var genericArguments = type.GetGenericArguments();
        var tupleFormatterType = genericArguments.Length switch
        {
            1 => typeof(TupleFormatter<>),
            2 => typeof(TupleFormatter<,>),
            3 => typeof(TupleFormatter<,,>),
            4 => typeof(TupleFormatter<,,,>),
            5 => typeof(TupleFormatter<,,,,>),
            6 => typeof(TupleFormatter<,,,,,>),
            7 => typeof(TupleFormatter<,,,,,,>),
            8 => typeof(TupleFormatter<,,,,,,,>),
            _ => throw new NotSupportedException($"Tuple Type is not supported. ({type.FullName})")
        };

        return (IFormatter<T>)Activator.CreateInstance(
            tupleFormatterType.MakeGenericType(genericArguments));
    }
}
