using System.Collections.Generic;

#pragma warning disable CS8765

namespace AppAsToy.Json.Conversion.Formatters.Collections;

public sealed class DictionaryFormatter<TKey, TValue> :
    GenericDictionaryFormatter<TKey, TValue, Dictionary<TKey, TValue>, DictionaryFormatter<TKey, TValue>>
{
    
}
