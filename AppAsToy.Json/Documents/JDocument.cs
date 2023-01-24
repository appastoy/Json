using System.Collections.Generic;
using System.Linq;

namespace AppAsToy.Json.Documents;

/// <summary>
/// Json Document
/// </summary>
public sealed class JDocument
{
    
}

public enum JType
{
    Null, // 0
    True, // 1
    False, // 2
    StringZero, // 3
    NumberZero, // 4
    NumberOne, // 5
    NumberHalf, // 6
    NumberMinusOne, // 7
    NumberMinusHalf, // 8
    ArrayEmpty,// 9
    ObjectEmpty, // 10
    ObjectProperty, // 11
    String, // 12
    Number, // 13
    Array, // 14
    Object, // 15
}

public interface IJElement
{
    JType Type { get; }
}

public readonly struct JRef
{
    private readonly uint _refValue;
    public JType Type => (JType)((_refValue & 0x_F000_0000U) >> 28);
    public int Index => (int)(_refValue & 0x_0FFF_FFFFU);
    internal JRef(uint refValue) => _refValue = refValue;
}

public readonly struct JElement : IJElement
{
    private readonly JDocument _document;
    private readonly JRef _ref;

    public JType Type => _ref.Type;

    internal JElement(JDocument document, JRef @ref)
    {
        _document = document;
        _ref = @ref;
    }
}

public struct JArray : IJElement
{
    private readonly JDocument _document;
    private List<JRef> _items;

    public JType Type => _items?.Count == 0 ? JType.ArrayEmpty : JType.Array;

    internal JArray(JDocument document, List<JRef> items)
    {
        _document = document;
        _items = items;
    }
}

internal struct JProperty : IJElement
{
    public readonly string Key;
    public JRef Value;

    public JType Type => JType.ObjectProperty;

    internal JProperty(string key, JRef value)
    {
        Key = key;
        Value = value;
    }
}

public struct JObject : IJElement
{
    private readonly JDocument _document;
    private List<JProperty>? _items;
    private Dictionary<string, JRef>? _itemMap;

    public JType Type => _items?.Count == 0 ? JType.ObjectEmpty : JType.Object;

    internal JObject(JDocument document, List<JProperty>? items)
    {
        _document = document;
        _items = items;
        _itemMap = items?.ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}