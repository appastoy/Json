using System;
using System.Buffers;

namespace AppAsToy.Json.Documents;

internal struct ItemPool<T> : IDisposable
{
    private BitPool _indexPool;
    private T[]? _items;

    public ref T this[int index]
    {
        get => ref _items![index];
    }

    public static ItemPool<T> Create() => new ItemPool<T>(1);

    private ItemPool(int initCapacity)
    {
        _items = ArrayPool<T>.Shared.Rent(initCapacity);
        _indexPool = new BitPool(_items.Length);
    }

    public int Rent()
    {
        if (_indexPool.IsFull)
        {
            Expand();
            _indexPool.Reserve(_items.Length);
        }

        return _indexPool.Rent();
    }

    public void Return(int index)
    {
        _indexPool.Return(index);
    }

    private void Expand()
    {
        var newLength = _items!.Length + (_items.Length >> 1);
        var newValues = ArrayPool<T>.Shared.Rent(newLength);
        Buffer.BlockCopy(_items, 0, newValues, 0, _items.Length);
        _items = newValues;
    }

    public void Dispose()
    {
        if (_items != null)
        {
            var values = _items;
            _items = null;
            ArrayPool<T>.Shared.Return(values);
        }
    }
}



