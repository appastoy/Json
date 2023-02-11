using AppAsToy.Json.Conversion.Formatters;
using AppAsToy.Json.Helpers;
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

        if (!typeof(ITuple).IsAssignableFrom(type))
            return null;

        var genericArguments = type.GetGenericArguments();
        if (type.FullName.StartsWith("System.Tuple`"))
        {
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

            return tupleFormatterType.MakeGenericType(genericArguments).GetSharedFormatter<T>();
        }
        else if (type.FullName.StartsWith("System.ValueTuple`"))
        {
            var tupleFormatterType = genericArguments.Length switch
            {
                1 => typeof(ValueTupleFormatter<>),
                2 => typeof(ValueTupleFormatter<,>),
                3 => typeof(ValueTupleFormatter<,,>),
                4 => typeof(ValueTupleFormatter<,,,>),
                5 => typeof(ValueTupleFormatter<,,,,>),
                6 => typeof(ValueTupleFormatter<,,,,,>),
                7 => typeof(ValueTupleFormatter<,,,,,,>),
                8 => typeof(ValueTupleFormatter<,,,,,,,>),
                _ => throw new NotSupportedException($"ValueTuple Type is not supported. ({type.FullName})")
            };

            return tupleFormatterType.MakeGenericType(genericArguments).GetSharedFormatter<T>();
        }

        throw new NotSupportedException($"Unknown Tuple type is not supported. ({type.FullName})");
    }
}
