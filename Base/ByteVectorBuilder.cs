using System.Collections;
using System.Text;
using Vipl.Base.Extensions;

namespace Vipl.Base;

/// <summary>
/// Class for used for joining different data in one <see cref="ByteVector"/>.
/// Allocation of space for <see cref="ByteVector"/> is done only once.
/// </summary>
public class ByteVectorBuilder: IEnumerable<byte>, IByteVectorBuilder
{
    private int _capacity;
    private readonly ByteVector _byteVector;
    private int _position ;
    
    /// <summary> Create new <see cref="ByteVectorBuilder"/> with capacity of <paramref name="capacity"/> bytes. </summary>
    /// <param name="capacity">Initial capacity of <see cref="ByteVector"/></param>
    public ByteVectorBuilder(int capacity)
    {
        _capacity = capacity;
        _byteVector = new ByteVector(capacity);
    }
    /// <inheritdoc />
    public long Position
    {
        get => _position;
        set
        {
            ResizeIfNeeded((int)value);
            _position = (int)value;
        }
    }
    /// <inheritdoc />
    public long Capacity
    {
        get => _capacity;
        set
        {
            ResizeIfNeeded((int)value);
            _capacity = (int)value;
            if (_position >= _capacity)
            {
                _position = _capacity - 1;
            }
        }
    }
    private void ResizeIfNeeded(int sizeNeeded)
    {
        if (sizeNeeded <= _capacity) return;
        _capacity = sizeNeeded;
        if (_capacity > _byteVector.Capacity)
        {
            _byteVector.Capacity = _capacity*2;
        }
        _byteVector.Resize(_capacity);
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(int value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 4);
        var data = _byteVector.Data;
        for (var i = 0; i < 4; i++) {
            var offset = mostSignificantByteFirst ? 3 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }

        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(uint value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 4);
        var data = _byteVector.Data;
        for (var i = 0; i < 4; i++) {
            var offset = mostSignificantByteFirst ? 3 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(string text, Encoding encoding, int length)
    {
        if (text.IsNullOrEmpty())
            return this;
        var bytes = encoding.GetBytes(text);
        return Add(new Span<byte>(bytes, 0, Math.Min(length, bytes.Length)));
       
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add (string text, Encoding encoding) => Add(text, encoding, int.MaxValue);
    /// <inheritdoc />
    public IByteVectorBuilder Add (string text, int length) => Add(text, Encoding.UTF8, length);
    /// <inheritdoc />
    public IByteVectorBuilder Add(string text) => Add(text, Encoding.UTF8, int.MaxValue);
    /// <inheritdoc />
    public IByteVectorBuilder Add ( short value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 2);
        var data = _byteVector.Data;
        for (var i = 0; i < 2; i++) {
            var offset = mostSignificantByteFirst ? 1 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }

        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add (ushort value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 2);
        var data = _byteVector.Data;
        for (var i = 0; i < 2; i++) {
            var offset = mostSignificantByteFirst ? 1 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }

        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add ( long value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 8);
        var data = _byteVector.Data;
        for (var i = 0; i < 8; i++) {
            var offset = mostSignificantByteFirst ? 7 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }

        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add ( ulong value, bool mostSignificantByteFirst = true)
    {
        ResizeIfNeeded(_position + 8);
        var data = _byteVector.Data;
        for (var i = 0; i < 8; i++) {
            var offset = mostSignificantByteFirst ? 7 - i : i;
            data[_position++] = (byte) (value >> (offset * 8) & 0xFF);
        }

        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(Guid value)
    { 
        ResizeIfNeeded(_position + 16);
        value.TryWriteBytes(_byteVector.Data.Slice(_position, 16));
        _position += 16;
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(ByteVector value)
    {
        
        ResizeIfNeeded(_position + value.Count);
        value.Data.CopyTo(_byteVector.Data.Slice( _position, value.Count));
        _position += value.Count;
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(byte[] value)
    {
        return Add(new Span<byte>(value));
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(Span<byte> value)
    {
        ResizeIfNeeded(_position + value.Length);
        value.CopyTo(_byteVector.Data.Slice(_position, value.Length));
        _position += value.Length;
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(byte value)
    {
        ResizeIfNeeded(_position + 1);
        _byteVector.Data[_position++] = value;
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(FixedPoint8_8 value)
    {
        return Add(value.BinaryRepresentation);
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(FixedPoint16_16 value)
    {
        return Add(value.BinaryRepresentation);
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(FixedPoint2_30 value)
    {
        return Add(value.BinaryRepresentation);
    }
    /// <inheritdoc />
    public IByteVectorBuilder Add(TransformationMatrix value)
    {
        Add(value.A); Add(value.B); Add(value.U);
        Add(value.C); Add(value.D); Add(value.V);
        Add(value.X); Add(value.Y); Add(value.W);
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Skip(uint size)
    {
        ResizeIfNeeded(_position + (int)size);
        _position += (int)size;
        return this;
    }
    /// <inheritdoc />
    public IByteVectorBuilder Clear(uint size)
    {
        return Skip(size);
    }
    /// <inheritdoc />
    public ByteVector Build()
    {
        _byteVector.Resize(_position);
        return _byteVector;
    }
    /// <inheritdoc/>
    IEnumerator<byte> IEnumerable<byte>.GetEnumerator()
    {
        return _byteVector.GetEnumerator();
    }
    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _byteVector).GetEnumerator();
    }
   
}