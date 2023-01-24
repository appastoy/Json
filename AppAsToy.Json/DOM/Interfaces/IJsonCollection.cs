using System.Collections.Generic;

namespace AppAsToy.Json.DOM;
public interface IJsonCollection : IReadOnlyList<IJsonElement>
{
    new ArrayEnumerator<IJsonElement> GetEnumerator();
}
