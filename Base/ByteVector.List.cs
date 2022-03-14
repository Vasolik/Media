using Vipl.Base.Extensions;

namespace Vipl.Base;

public sealed partial class ByteVector : IList<byte>
{

    /// <inheritdoc />
    public void Clear()
    {
        Array.Clear(_data.GetInternalArray(), 0, Count);
        _data.ResizeWithJunkInternal(0);
    }

    /// <inheritdoc />
    public void Add(byte item)
    {
        _data.Add(item);
    }

    /// <inheritdoc />
    public bool Remove(byte item)
    {
        return _data.Remove(item);
    }

    /// <inheritdoc />
    public void CopyTo(byte[] array, int arrayIndex)
    {
        _data.CopyTo(array, arrayIndex);
    }
    
    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        _data.RemoveAt(index);
    }

    /// <inheritdoc />
    public void Insert(int index, byte item)
    {

        _data.Insert(index, item);
    }

    /// <inheritdoc />
    public int IndexOf(byte item)
    {
        return _data.IndexOf(item);
    }

    /// <inheritdoc />
    public bool IsReadOnly => false;
    
    /// <inheritdoc />
    public byte this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }
    /// <summary>
    /// Gets or sets the total number of elements the internal data structure can hold without resizing.
    /// </summary>
    /// <returns>The number of elements that the <see cref="ByteVector"/> can contain before resizing is required.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is set to a value that is less than Count.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available on the system</exception>
    public int Capacity
    {
        get => _data.Capacity;
        set => _data.Capacity = value;
    }
    /// <inheritdoc />
    public IEnumerator<byte> GetEnumerator()
    {
        return _data.GetEnumerator();
    }
}