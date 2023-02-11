using AppAsToy.Json.Conversion.Formatters.Collections;
using AppAsToy.Json.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.FormatterResolvers;
internal sealed class CollectionResolver : IFormatterResolver
{
    public IFormatter<T>? Resolve<T>()
    {
        var type = typeof(T);
        if (type.IsArray)
            return typeof(ArrayFormatter<>).GetGenericSharedFormatter<T>(type.GetElementType());

        if (!type.IsGenericType)
            return null;

        var definitionType = type.GetGenericTypeDefinition();
        var genericArguments = type.GetGenericArguments();

        return TryGetCollectionFormatter<T>(definitionType, genericArguments)
            ?? TryGetConcurrentCollectionFormatter<T>(definitionType, genericArguments);
    }

    private IFormatter<T>? TryGetCollectionFormatter<T>(Type definitionType, Type[] genericArguments)
    {
        if (definitionType == typeof(List<>))
            return typeof(ListFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        if (definitionType == typeof(Dictionary<,>))
        {
            if (genericArguments[0] == typeof(string))
                return typeof(StringDictionaryFormatter<>).GetGenericSharedFormatter<T>(genericArguments[1]);
            else
                return typeof(DictionaryFormatter<,>).GetGenericSharedFormatter<T>(genericArguments);
        }

        if (definitionType == typeof(Stack<>))
            return typeof(StackFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        if (definitionType == typeof(Queue<>))
            return typeof(QueueFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        return null;
    }

    private IFormatter<T>? TryGetConcurrentCollectionFormatter<T>(Type definitionType, Type[] genericArguments)
    {
        if (definitionType == typeof(ConcurrentBag<>))
            return typeof(ConcurrentBagFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        if (definitionType == typeof(ConcurrentDictionary<,>))
        {
            if (genericArguments[0] == typeof(string))
                return typeof(ConcurrentStringDictionaryFormatter<>).GetGenericSharedFormatter<T>(genericArguments[1]);
            else
                return typeof(ConcurrentDictionaryFormatter<,>).GetGenericSharedFormatter<T>(genericArguments);
        }

        if (definitionType == typeof(ConcurrentStack<>))
            return typeof(StackFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        if (definitionType == typeof(ConcurrentQueue<>))
            return typeof(ConcurrentQueueFormatter<>).GetGenericSharedFormatter<T>(genericArguments);

        return null;
    }
}
