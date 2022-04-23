using System.Collections;
using System.Text;
using Vipl.Base.Extensions;

namespace Vipl.Base;

/// <summary> This class represents and performs operations on variable length list of <see cref="byte"/> elements. </summary>
public sealed partial class ByteVector: IEquatable<ByteVector>, IComparable<ByteVector>
{
    private readonly List<byte> _data = new();

    /// <summary> Gets the data stored in the current instance as Span of bytes. </summary>
    /// <value> A <see cref="Span{Byte}"/> containing the data stored in the current instance. </value>
    public Span<byte> Data => ((Span<byte>)_data.GetInternalArray())[..Count];
    
    /// <summary> Gets the data stored in the current instance. </summary>
    /// <value> A <see cref="T:byte[]" /> containing the data stored in the  current instance. </value>
    public byte[] DataArray => _data.GetInternalArray();
  

    /// <summary> Adds the contents of another <see cref="ByteVector" /> object to the end of the current instance. </summary>
    /// <param name="data"> A <see cref="ByteVector" /> object containing data to add to the end of the current instance. </param>
    public void Add(ByteVector data)
    {
        Add(data._data.GetInternalArray());
    }

    /// <summary> Adds the contents of an array to the end of the current instance. </summary>
    /// <param name="data"> A <see cref="T:byte[]" /> containing data to add to the end of the current instance. </param>
    public void Add(byte[] data)
    {
        var count = Count;
        _data.ResizeWithJunkInternal(count + data.Length);
        new Span<byte>(data).CopyTo(Data.Slice(count, data.Length));
    }
    
    /// <inheritdoc cref="List{T}.InsertRange"/>
    public void Insert(int index, ByteVector collection)
    {
        _data.InsertRange(index, collection._data);
    }

    /// <inheritdoc cref="List{T}.InsertRange"/>
    public void Insert(int index, byte[] data)
    {
        if (IsReadOnly)
            throw new NotSupportedException("Cannot edit readonly objects.");

        _data.InsertRange(index, data);
    }

    /// <summary> Resizes the current instance. </summary>
    /// <param name="size"> A <see cref="int" /> value specifying the new size of the current instance. </param>
    /// <param name="padding"> A <see cref="byte" /> object containing the padding byte to use if the current instance is growing. </param>
    /// <returns> The current instance. </returns>
    public ByteVector Resize(int size, byte? padding = 0)
    {
        var oldSize = _data.Count;
        _data.ResizeWithJunkInternal(size);

        if (oldSize >= size || padding is null) 
            return this;
        if (padding == 0)
        {
            Array.Clear(_data.GetInternalArray(), oldSize, size - oldSize);
        }
        else
        {
            Array.Fill(_data.GetInternalArray(), padding.Value, oldSize, size - oldSize);
        }
        return this;
    }

    /// <summary> Removes a range of data from the current instance. </summary>
    /// <param name="index"> A <see cref="int" /> value specifying the range at which to start removing data. </param>
    /// <param name="count"> A <see cref="int" /> value specifying the number of bytes to remove. </param>
    public void RemoveRange(int index, int count)
    {
        if (IsReadOnly)
            throw new NotSupportedException("Cannot edit readonly objects.");

        _data.RemoveRange(index, count);
    }
    
    /// <summary> Gets whether or not the current instance is empty. </summary>
    /// <value> A <see cref="bool" /> value indicating whether or not the current instance is empty. </value>
    public bool IsEmpty => Data.Length == 0;
    /// <summary> Number of bytes in <see cref="ByteVector"/> </summary>
    public int Count => _data.Count;
    
    /// <summary> Get part of <see cref="ByteVector"/> starting from <paramref name="index"/> to end.  </summary>
    /// <param name="index">Index from where part was taken.</param>
    /// <returns><see cref="Span{Byte}"/> with bytes from original vector. This instance point to same bytes as original</returns>
    public Span<byte> Mid(int index)
    {
        return Mid(index, Count - index);
    }
    /// <summary> Get part of <see cref="ByteVector"/> inside <paramref name="range"/> </summary>
    /// <param name="range"><see cref="Range"/> from which bytes will be taken.</param>
    /// <returns><see cref="Span{Byte}"/> with bytes from original vector. This instance point to same bytes as original</returns>
    public Span<byte> Mid(Range range)
    {
        var startIndex = range.Start.IsFromEnd ? Count - range.Start.Value : range.Start.Value;
        var endIndex = range.End.IsFromEnd ? Count - range.End.Value : range.End.Value;
        return Mid(startIndex, endIndex - startIndex);
    }

    /// <summary> Get part of <see cref="ByteVector"/> starting from <paramref name="startIndex"/> next <paramref name="length"/> bytes.</summary>
    /// <param name="startIndex">Index from where part was taken.</param>
    /// <param name="length">Number of bytes taken from byte vector.</param>
    /// <returns><see cref="Span{Byte}"/> with bytes from original vector. This instance point to same bytes as original</returns>
    public Span<byte> Mid(int startIndex, int length)
    {
        return Data.Slice(startIndex, length);
    }
    
    /// <summary> Find index location where <paramref name="pattern"/> byte vectors occurs. </summary>
    /// <param name="pattern">Pattern to be searched.</param>
    /// <param name="offset">Starting index for search.</param>
    /// <param name="byteAlign">Alignment of resulting index. If result is found not align, that result would be ignored.</param>
    /// <returns>Index where <paramref name="pattern"/> was located first time in this vector.
    /// If pattern was not found <c>-1</c></returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is negative or if byteAlign is less then 1.</exception>
    public int Find(Span<byte> pattern, int offset = 0, int byteAlign = 1)
    {
        return Data.Find(pattern, offset, byteAlign);
    }
    
    /// <summary> Find index location where <paramref name="pattern"/> byte vectors occurs last.</summary>
    /// <param name="pattern">Pattern to be searched.</param>
    /// <param name="offset">Starting index for search.</param>
    /// <param name="byteAlign">Alignment of resulting index. If result is found not align, that result would be ignored.</param>
    /// <returns>Index where <paramref name="pattern"/> was located last time in this vector.
    /// If pattern was not found <c>-1</c></returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is negative or if byteAlign is less then 1.</exception>
    public int ReverseFind(Span<byte> pattern, int offset = 0, int byteAlign = 1)
    {
        return Data.ReverseFind(pattern, offset, byteAlign);
    }
    
    /// <summary> Check if <paramref name="pattern"/> is placed from <paramref name="offset"/>.</summary>
    /// <param name="pattern">Pattern to be searched for.</param>
    /// <param name="offset">Offset where pattern should appear.</param>
    /// <param name="patternOffset">From which index of pattern search in conducted.</param>
    /// <param name="patternLength">How much bytes of pattern is checked</param>
    /// <returns><c>true</c> if pattern is found <c>false</c> otherwise.</returns>
    public bool ContainsAt(Span<byte> pattern, int offset, int patternOffset = 0, int patternLength = int.MaxValue)
    {
        return Data.ContainsAt(pattern, offset, patternOffset, patternLength);
    }
    /// <summary> Does this byte vector starts with <paramref name="pattern"/> </summary>
    /// <param name="pattern">Pattern to check.</param>
    /// <returns><c>true</c> if pattern is found at start of byte vector <c>false</c> otherwise.</returns>
    public bool StartsWith(Span<byte> pattern)
    {
        return ContainsAt(pattern, 0);
    }
    
    /// <summary> Does this byte vector ends with <paramref name="pattern"/> </summary>
    /// <param name="pattern">Pattern to check.</param>
    /// <returns><c>true</c> if pattern is found at end of byte vector <c>false</c> otherwise.</returns>
    public bool EndsWith(Span<byte> pattern)
    {
        return ContainsAt(pattern, Data.Length - pattern.Length);
    }

    /// <summary> Gets whether or not the current instance contains a specified byte. </summary>
    /// <param name="item"> A <see cref="byte" /> value to look for in the current  instance.</param>
    /// <returns>  <see langword="true" /> if the value could be found; otherwise <see langword="false" />. </returns>
    public bool Contains(byte item)
    {
        return Data.Contains(item);
    }

    /// <summary> Determines whether another <see cref="ByteVector" /> object is equal to the current instance. </summary>
    /// <param name="other"> A <see cref="ByteVector" /> object to compare to the current instance. </param>
    /// <returns> <see langword="true" /> if <paramref name="other" /> is not <see langword="null" /> and equal to the current instance; otherwise <see langword="false" />. </returns>
    public bool Equals(ByteVector? other)
    {
        return other is not null && Data.SequenceEqual(other.Data);
    }
    
    /// <summary> Determines whether another object is equal to the current instance. </summary>
    /// <param name="other"> A <see cref="object" /> to compare to the current instance. </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="other" /> is not <see langword="null" />,
    /// is of type <see cref="ByteVector" />, and is equal to the current instance; otherwise <see langword="false" />.
    /// </returns>
    public override bool Equals(object? other)
    {
        return other is ByteVector vector && Equals(vector);
    }

    /// <summary> Gets the hash value for the current instance. </summary>
    /// <returns> A <see cref="int" /> value equal to the CRC checksum of the current instance. </returns>
    public override int GetHashCode()
    {
        unchecked { return (int) this.Checksum(); }
    }

    /// <inheritdoc />
    public int CompareTo(ByteVector? other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        return Data.SequenceCompareTo(other.Data);
    }

    /// <summary> Determines whether two specified <see cref="ByteVector" /> objects are equal. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare. </param>
    /// <returns><see langword="true" /> if <paramref name="first" /> and <paramref name="second" /> contain the same data; otherwise, <see langword="false" />. </returns>
    public static bool operator ==(ByteVector? first, ByteVector? second)
    {
        if (first is null && second is null)
            return true;
        if (first is null || second is null)
            return false;

        return first.Equals(second);
    }

    /// <summary> Determines whether two specified <see cref="ByteVector" /> objects differ. </summary>
    /// <param name="first">A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare. </param>
    /// <returns> <see langword="true" /> if <paramref name="first" /> and <paramref name="second" /> contain different
    /// data; otherwise, <see langword="false" />. </returns>
    public static bool operator !=(ByteVector? first, ByteVector? second)
    {
        return !(first == second);
    }

    /// <summary> Determines whether or not one <see cref="ByteVector" />  is less than another. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare.</param>
    /// <returns> <see langword="true" /> if <paramref name="first" /> is less than <paramref name="second" />;
    /// otherwise,<see langword="false" />. </returns>
    public static bool operator <(ByteVector first, ByteVector second)
    {
        return first.CompareTo(second) < 0;
    }

    /// <summary> Determines whether or not one <see cref="ByteVector" /> is less than or equal to another. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare. </param>
    /// <returns> <see langword="true" /> if <paramref name="first" /> is less than or equal to <paramref name="second" />;
    /// otherwise, <see langword="false" />. </returns>
    public static bool operator <=(ByteVector first, ByteVector second)
    {
        return first.CompareTo(second) <= 0;
    }

    /// <summary> Determines whether or not one <see cref="ByteVector"/> is greater than another. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare. </param>
    /// <returns><see langword="true" /> if <paramref name="first" /> is greater than <paramref name="second" />;
    /// otherwise <see langword="false" />. </returns>
    public static bool operator >(ByteVector first, ByteVector second)
    {
        return first.CompareTo(second) > 0;
    }

    /// <summary> Determines whether or not one <see cref="ByteVector" /> is greater than or equal to another. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to compare. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to compare. </param>
    /// <returns><see langword="true" /> if <paramref name="first" /> is greater than or equal to <paramref name="second" />;
    /// otherwise <see langword="false" />. </returns>
    public static bool operator >=(ByteVector first, ByteVector second)
    {
        return first.CompareTo(second) >= 0;
    }

    /// <summary> Creates a new <see cref="ByteVector" /> object by adding two objects together. </summary>
    /// <param name="first"> A <see cref="ByteVector" /> to combine. </param>
    /// <param name="second"> A <see cref="ByteVector" /> to combine. </param>
    /// <returns> A new instance of <see cref="ByteVector" /> with the contents
    /// of <paramref name="first" /> followed by the contents of <paramref name="second" />. </returns>
    public static ByteVector operator +(ByteVector first, ByteVector second)
    {
        return new ByteVectorBuilder(first.Count + second.Count)
            .Add(first).Add(second).Build();
    }
    
    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <param name="offset">Starting offset from where characters will be taken</param>
    /// <param name="count">Number of byte which will be taken</param>
    /// <returns>Resulting string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset or count is out of range of value.</exception>
    public string ToString(Encoding encoding, int offset, int count)
    {
        return Data.ToString(encoding, offset, count);
    }
    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <param name="offset">Starting offset from where characters will be taken</param>
    /// <returns>Resulting string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is out of range of value.</exception>
    public string ToString(Encoding encoding, int offset)
    {
        return Data.ToString(encoding, offset);
    }
    
    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <returns>Resulting string.</returns>
    public string ToString(Encoding encoding)
    {
        return Data.ToString(encoding);
    }
    
    /// <summary> Converts an string into a encoded data representation. </summary>
    /// <param name="text">  A <see cref="string" /> object containing the text to convert. </param>
    /// <param name="encoding"> A <see cref="Encoding" /> value specifying the encoding to use when converting the text. </param>
    /// <param name="length"> A <see cref="int" /> value specifying the number of characters in <paramref name="text" /> to encoded. </param>
    public static ByteVector FromString(string text, Encoding encoding, int length)
    {
        text = text[..Math.Min(length, text.Length)];
        if (text.IsNullOrEmpty())
            return new ByteVector();
        var bytes = encoding.GetBytes(text);

        if (!Equals(encoding, Encoding.Unicode))
            return bytes;

        return new ByteVectorBuilder(2 + bytes.Length)
            .Add(new byte[] {0xff, 0xfe})
            .Add(bytes)
            .Build();
    }
    
    /// <summary> Converts an string into a encoded data representation. </summary>
    /// <param name="text">  A <see cref="string" /> object containing the text to convert. </param>
    /// <param name="encoding"> A <see cref="Encoding" /> value specifying the encoding to use when converting the text. </param>
    public static ByteVector FromString(string text, Encoding encoding)
    {
        return FromString(text, encoding, int.MaxValue);
    }

    /// <summary> Converts an string into a encoded data representation. </summary>
    /// <param name="text">  A <see cref="string" /> object containing the text to convert. </param>
    /// <param name="length"> A <see cref="int" /> value specifying the number of characters in <paramref name="text" /> to encoded. </param>
    public static ByteVector FromString(string text, int length)
    {
        return FromString(text, Encoding.UTF8, length);
    }

    /// <summary> Converts an string into a encoded data representation. </summary>
    /// <param name="text">  A <see cref="string" /> object containing the text to convert. </param>
    public static ByteVector FromString(string text)
    {
        return FromString(text, Encoding.UTF8);
    }

    /// <summary> Converts the current instance into a <see cref="string" />  object using a UTF-8 encoding. </summary>
    public override string ToString()
    {
        return ToString(Encoding.UTF8);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    /// <summary> Get part of <see cref="ByteVector"/> inside <paramref name="index"/> </summary>
    /// <param name="index"><see cref="Range"/> from which bytes will be taken.</param>
    /// <returns><see cref="Span{Byte}"/> with bytes from original vector. This instance point to same bytes as original</returns>
    public Span<byte> this[Range index] => Mid(index);

}