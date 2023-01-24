using System;
using System.Collections;
using System.Collections.Generic;

namespace AppAsToy.Json.Documents;

internal delegate bool RefPredicate<T>(ref T? item);

internal sealed class SequentialList<T> : IEnumerable<T>
{
    private T?[] _items;
    private int _count;

    public ref T this[int index]
    {
        get => ref _items[index]!;
    }

    public int Count => _count;

    public SequentialList() : this(16)
    {
    }

    public SequentialList(int capacity)
    {
        _items = new T[capacity];
        _count = 0;
    }

    public void Add(T value)
    {
        if (_items.Length == _count)
            Expand();

        _items[_count++] = value;
    }

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
            Add(item);
    }

    public int FindIndex(RefPredicate<T> predicate)
    {
        for (int i = 0; i < _count; i++)
        {
            if (predicate.Invoke(ref _items[i]))
                return i;
        }
        return -1;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index), index, "index out of range.");

        for (int i = index + 1; i < _count; i++)
            _items[i - 1] = _items[i];
        
        _count -= 1;
        _items[_count] = default;
    }

    public bool RemoveFirst(RefPredicate<T> predicate)
    {
        var index = FindIndex(predicate);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }
        return false;
    }

    public void Clear(bool clearValuesToDefault = false)
    {
        _count = 0;
        if (clearValuesToDefault)
        {
            for (int i = 0; i < _count; i++)
                _items[i] = default;
        }
    }

    private void Expand()
    {
        Array.Resize(ref _items, _count + (_count >> 1));
    }

    public ArrayEnumerator<T> GetEnumerator()
    {
        return new ArrayEnumerator<T>(_items!, _count);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)_items!).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
