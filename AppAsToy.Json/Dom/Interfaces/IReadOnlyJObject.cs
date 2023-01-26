using System.Collections.Generic;

namespace AppAsToy.Json;
public interface IReadOnlyJObject : IReadOnlyJCollection
{
    IReadOnlyJElement this[string name] { get; }
    IEnumerable<string> PropertyNames { get; }
    IEnumerable<IReadOnlyJElement> PropertyValues { get; }
    bool ContainsName(string name);
    bool TryGetValue(string name, out IReadOnlyJElement value);
}
