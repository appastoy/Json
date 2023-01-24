using System;

namespace AppAsToy.Json.DOM;
public interface IJsonArray : IJsonCollection
{
    bool Contains(IJsonElement? element);
    int IndexOf(IJsonElement? element);
    int LastIndexOf(IJsonElement? element);
    int FindIndex(Predicate<IJsonElement> func);
    int FindLastIndex(Predicate<IJsonElement> func);
    IJsonElement FindFirst(Predicate<IJsonElement> func) => FindFirst(func);
    IJsonElement FindLast(Predicate<IJsonElement> func) => FindLast(func);
}
