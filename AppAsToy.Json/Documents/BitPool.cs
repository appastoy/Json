using System;

namespace AppAsToy.Json.Documents;

internal struct BitPool
{
    private static readonly byte[] _preCalculatedBitIndices16;

    static BitPool()
    {
        _preCalculatedBitIndices16 = CalculateBitIndices16();
    }

    private static byte[] CalculateBitIndices16()
    {
        var indices16 = new byte[0xFFFF];
        for (ushort i = 0; i < 0xFFFF; i++)
        {
            var bit = i;
            byte index = 0;
            if ((bit & 0xFF) == 0xFF)
            {
                bit >>= 8;
                index += 8;
            }
            if ((bit & 0b_1111) == 0b_1111)
            {
                bit >>= 4;
                index += 4;
            }
            if ((bit & 0b_11) == 0b_11)
            {
                bit >>= 2;
                index += 2;
            }

            if ((bit & 0b_1) == 0b_1)
                index += 1;

            indices16[i] = index;
        }

        return indices16;
    }

    private ulong[] _bits;
    private int _rentCount;

    public int Capacity
    {
        get => _bits == null ? 0 : (_bits.Length << 5);
    }

    public bool IsFull
    {
        get => _rentCount >= Capacity;
    }

    public BitPool(int initialCapacity)
    {
        _rentCount = 0;
        _bits = InitializeBits(initialCapacity);
    }

    public void Reserve(int capacity)
    {
        if (capacity <= 0)
            return;

        if (_bits != null)
        {
            var currentCapacity = Capacity;
            if (capacity <= currentCapacity)
                return;

            var newBitLength = CalculateBitLength(currentCapacity - capacity);
            Array.Resize(ref _bits, _bits.Length + newBitLength);
        }
        else
        {
            _bits = InitializeBits(capacity);
        }
    }

    private static ulong[] InitializeBits(int capacity)
    {
        int bitLength = CalculateBitLength(capacity);
        return new ulong[bitLength];
    }

    private static int CalculateBitLength(int capacity)
    {
        var bitLength = capacity >> 6;
        if (bitLength == 0)
            bitLength = 1;
        return bitLength;
    }

    public int Rent()
    {
        if (!TryFindFreeBitIndex(out var arrayIndex, out var bitIndex))
        {
            Reserve(Capacity + 64);
            arrayIndex = _bits.Length - 1;
            bitIndex = 0; 
        }

        _bits[arrayIndex] &= ~(1UL << bitIndex);
        _rentCount += 1;

        return (arrayIndex << 6) + bitIndex;
    }

    public void Return(int index)
    {
        var arrayIndex = index >> 6;
        if (arrayIndex < 0 ||
            arrayIndex >= _bits.Length)
            throw new IndexOutOfRangeException($"index out of range. ({index})");

        var bitMask = 1UL << (index & 0b_11_1111);
        if ((_bits[arrayIndex] & bitMask) == 0)
            throw new ArgumentException($"index is not rented. ({index})");

        _bits[arrayIndex] |= bitMask;
        _rentCount -= 1;
    }

    private bool TryFindFreeBitIndex(out int arrayIndex, out int bitIndex)
    {
        for (int i = 0; i < _bits.Length; ++i)
        {
            var index = 0;
            var bit = _bits[i];
            if (bit == 0xFFFFFFFFFFFFFFFF)
                continue;

            if ((bit & 0xFFFFFFFF) == 0xFFFFFFFF)
            {
                bit >>= 32;
                index += 32;
            }
            if ((bit & 0xFFFF) == 0xFFFF)
            {
                bit >>= 16;
                index += 16;
            }
            if ((bit & 0xFFFF) != 0xFFFF)
            {
                index += _preCalculatedBitIndices16[bit];
                arrayIndex = i;
                bitIndex = index;
                return true;
            }
        }

        arrayIndex = default;
        bitIndex = default;
        return false;
    }
}



