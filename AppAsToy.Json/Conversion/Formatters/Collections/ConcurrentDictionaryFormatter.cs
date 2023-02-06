using System.Collections.Concurrent;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ConcurrentDictionaryFormatter<TKey,TValue> : 
    GenericDictionaryFormatter<TKey, TValue, ConcurrentDictionary<TKey, TValue>, ConcurrentDictionaryFormatter<TKey, TValue>>
{
}