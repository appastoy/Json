using System.Collections.Generic;

namespace AppAsToy.Json.DOM;

public interface IJsonArray : IJsonElement, IReadOnlyList<IJsonElement>
{
    new ArrayEnumerator<IJsonElement> GetEnumerator();
    string ToString();
}