using System.Collections.Generic;

namespace AppAsToy.Json;
public interface IReadOnlyJCollection : IReadOnlyList<IReadOnlyJElement>
{
    new ArrayEnumerator<IReadOnlyJElement> GetEnumerator();
}
