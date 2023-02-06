using System.Collections.Generic;

namespace AppAsToy.Json.Conversion.Formatters.Collections;


public sealed class StringDictionaryFormatter<T> : 
    GenericStringDictionaryFormatter<T, Dictionary<string, T>, StringDictionaryFormatter<T>>
{
}
