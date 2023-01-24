using System.Collections.Generic;

namespace AppAsToy.Json.Documents;

/// <summary>
/// Json Document
/// </summary>
public sealed class JDocument
{
    internal ItemPool<List<JRef>> _arrayPool;
    internal ItemPool<JObjectData> _objectPool;
    internal ItemPool<string> _stringPool;
    internal ItemPool<double> _numberPool;
}
