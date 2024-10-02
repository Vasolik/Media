using Vipl.Base.Extensions;

namespace Vipl.Base;

public sealed partial class ByteVector : IList<byte>
{

    /// <inheritdoc />
    public void Clear()
    {
        ResizeInternalWithJunk(0);
        Data.Clear();
    }

    /// <inheritdoc />
    public void Add(byte item)
    {
        Insert(Count, item);
    }

    /// <inheritdoc />
    public bool Remove(byte item)
    {
        var firstIndex = Data.IndexOf(item);
        if (firstIndex == -1)
            return false;
        RemoveAt(firstIndex);
        
        return true;
    }

    /// <inheritdoc />
    public void CopyTo(byte[] array, int arrayIndex)
    {
        
        Data.CopyTo(new Span<byte>(array)[arrayIndex..]);
    }
    
    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        Data[(index + 1)..].CopyTo(Data[index..]);
    }

    /// <inheritdoc />
    public void Insert(int index, byte item)
    {
        ResizeInternalWithJunk(_size + 1);
        Data[index..].CopyTo(Data[(index + 1)..]);
        Data[index] = item;
    }

    /// <inheritdoc />
    public int IndexOf(byte item)
    {
        return Data.IndexOf(item);
    }

    /// <inheritdoc />
    public bool IsReadOnly => false;
    
    /// <inheritdoc />
    public byte this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }
    /// <summary>
    /// Gets or sets the total number of elements the internal data structure can hold without resizing.
    /// </summary>
    /// <returns>The number of elements that the <see cref="ByteVector"/> can contain before resizing is required.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is set to a value that is less than Count.</exception>
    /// <exception cref="OutOfMemoryException">There is not enough memory available on the system</exception>
    public int Capacity
    {
        get => _capacity;
        set => ChangeCapacity(value);
    }

    
    /// <inheritdoc />
    public IEnumerator<byte> GetEnumerator()
    {
        for(var i = 0; i < Count; i++)
            yield return Data[i];
    }
}