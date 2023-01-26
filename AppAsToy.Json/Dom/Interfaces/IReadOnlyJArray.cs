using System;

namespace AppAsToy.Json;
public interface IReadOnlyJArray : IReadOnlyJCollection
{
    bool Contains(IReadOnlyJElement? element);
    int IndexOf(IReadOnlyJElement? element);
    int LastIndexOf(IReadOnlyJElement? element);
    int FindIndex(Predicate<IReadOnlyJElement> func);
    int FindLastIndex(Predicate<IReadOnlyJElement> func);
    IReadOnlyJElement FindFirst(Predicate<IReadOnlyJElement> func) => FindFirst(func);
    IReadOnlyJElement FindLast(Predicate<IReadOnlyJElement> func) => FindLast(func);
}
