using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonObject : IJsonElement, IReadOnlyDictionary<string, IJsonElement>
{
    new ArrayEnumerator<JsonProperty> GetEnumerator();
    string ToString();
}