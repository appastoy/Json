using System.Collections.Generic;

namespace AppAsToy.Json.DOM;

public interface IJsonArray : IJsonElement, IReadOnlyList<JsonElement>
{
    void Add(JsonElement item);
    void Clear();
    bool Contains(JsonElement item);
    void CopyTo(JsonElement[] array, int arrayIndex);
    int IndexOf(JsonElement item);
    void Insert(int index, JsonElement item);
    bool Remove(JsonElement item);
    void RemoveAt(int index);
}