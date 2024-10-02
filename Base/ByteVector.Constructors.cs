using Vipl.Base.Extensions;

namespace Vipl.Base;

public sealed partial class ByteVector
{
    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/> with a length of zero. </summary>
    public ByteVector()
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/> by copying the values from another instance. </summary>
    /// <param name="vector"> A <see cref="ByteVector"/> object containing the bytes to be stored in the new instance. </param>
    public ByteVector(ByteVector vector)
        : this(vector.Data)
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/>
    /// by copying the values from a specified <see cref="T:byte[]"/>. </summary>
    /// <param name="data"> A <see cref="T:byte[]"/> containing the bytes to be stored in the new instance.</param>
    public ByteVector(params byte[] data)
        : this(data, data.Length)
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/>
    /// by copying a specified number of values from a specified <see cref="T:byte[]" />. </summary>
    /// <param name="data"> A <see cref="T:byte[]"/> containing the bytes to be stored in the new instance.</param>
    /// <param name="length"> A <see cref="int"/> value specifying the number of bytes to be copied to the new instance.</param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="length"/> is less than zero or greater than the length of the data.</exception>
    public ByteVector(byte[] data, int length)
    {
        if (length > data.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "Length exceeds size of data.");

        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length is less than zero.");

        ResizeInternalWithJunk(length);
        new Span<byte>(data).CopyTo(Data);
    }

    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/> of specified size containing bytes of a specified value. </summary>
    /// <param name="size"> A <see cref="int" /> value specifying the number of bytes to be stored in the new instance. </param>
    /// <param name="value"> A <see cref="byte" /> value specifying the value to be stored in the new instance. </param>
    /// <exception cref="ArgumentOutOfRangeException"> <paramref name="size" /> is less than zero. </exception>
    public ByteVector(int size, byte value = 0)
    {
        switch (size)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(size), "Size is less than zero.");
            case 0:
                return;
        }

        Resize(size, value);
    }
    /// <summary> Constructs and initializes a new instance of <see cref="ByteVector"/>
    /// by copying a specified number of values from a specified <see cref="Span{Byte}"/>. </summary>
    /// <param name="vector"> A <see cref="Span{Byte}"/> containing the bytes to be stored in the new instance.</param>
    public ByteVector(Span<byte> vector)
    {
        ResizeInternalWithJunk(vector.Length);
        vector.CopyTo(Data);
    }
}