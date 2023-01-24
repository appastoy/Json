using System;

namespace AppAsToy.Json.Documents;

internal struct ItemPool<T>
{
    private BitPool _indexPool;
    private T[]? _items;

    public ref T this[int index]
    {
        get => ref _items![index];
    }

    public static ItemPool<T> Create() => new ItemPool<T>(16);

    private ItemPool(int initCapacity)
    {
        _items = new T[initCapacity];
        _indexPool = new BitPool(_items.Length);
    }

    public int Rent()
    {
        if (_items == null)
            throw new ObjectDisposedException(GetType().Name);

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
        Array.Resize(ref _items, _items!.Length + (_items.Length >> 1));
    }
}



