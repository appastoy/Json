using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonObject : IJsonCollection
{
    IJsonElement this[string key] { get; }
    IEnumerable<string> Keys { get; }
    IEnumerable<IJsonElement> Values { get; }
    bool ContainsKey(string key);
    bool TryGetValue(string key, out IJsonElement value);
}
