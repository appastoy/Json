using System.Collections.Concurrent;

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class ConcurrentStringDictionaryFormatter<T> :
    GenericStringDictionaryFormatter<T, ConcurrentDictionary<string, T>, ConcurrentStringDictionaryFormatter<T>>
{
}
