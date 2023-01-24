using System;

namespace AppAsToy.Json.Documents;

internal readonly struct JObjectData
{
    internal readonly SequentialList<JPropertyData> _propertyList;

    public int Count => _propertyList.Count;

    public JRef this[string key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var listIndex = FindIndex(key);
            if (listIndex < 0)
                throw new ArgumentException($"key is not found. ({key})");

            return _propertyList[listIndex].Value;
        }
        set
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var listIndex = FindIndex(key);
            if (listIndex < 0)
                _propertyList.Add(new JPropertyData(key, value));
            else
                _propertyList[listIndex].Value = value;
        }
    }

    public bool ContainsKey(string key)
    {
        return FindIndex(key) > 0;
    }

    public bool TryGetValue(string key, out JRef value)
    {
        var listIndex = FindIndex(key);
        if (listIndex < 0)
        {
            value = default;
            return false;
        }
        value = _propertyList[listIndex].Value;
        return true;
    }

    public JObjectData(int capacity)
    {
        _propertyList = new SequentialList<JPropertyData>(capacity);
    }

    private int FindIndex(string key)
    {
        return _propertyList.FindIndex((ref JPropertyData property) => property.Key == key);
    }
}
