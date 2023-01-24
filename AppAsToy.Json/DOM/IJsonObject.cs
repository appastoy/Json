using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonObject : IJsonElement, IReadOnlyDictionary<string, JsonElement>
{
    IReadOnlyList<JsonProperty> Properties { get; }

    void Add(string key, JsonElement value);
    bool Remove(string key);
    string ToString();
}