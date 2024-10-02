using System.Text;
using Vipl.Base.Extensions;

// ReSharper disable MemberCanBePrivate.Global

namespace Vipl.Base;
/// <summary>
/// 
/// </summary>
public static class ByteVectorConverterExtensions
{
    /// <inheritdoc cref="ByteVector.FromString(string, Encoding, int)"/>
    public static ByteVector ToByteVector (this string text, Encoding encoding, int length) => ByteVector.FromString(text, encoding, length);

    /// <inheritdoc cref="ByteVector.FromString(string, Encoding)"/>
    public static ByteVector ToByteVector (this string text, Encoding encoding) => ByteVector.FromString(text, encoding);

    /// <inheritdoc cref="ByteVector.FromString(string, int)"/>
    public static ByteVector ToByteVector (this string text, int length) => ByteVector.FromString(text, length);

    /// <inheritdoc cref="ByteVector.FromString(string)"/>
    public static ByteVector ToByteVector(string text) => ByteVector.FromString(text);

    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="int"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this int value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }

    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="uint"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this uint value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }

    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="short"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this short value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }
    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="ushort"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this ushort value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }
    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="long"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this long value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }
    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="ulong"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector (this ulong value, bool mostSignificantByteFirst = true)
    {
        return new ByteVectorBuilder(4) {{value, mostSignificantByteFirst}}.Build();
    }
    /// <summary> Converts an value into a big-endian or little-endian data representation. </summary>
    /// <param name="value"> A <see cref="ulong"/> value to convert into bytes.</param>
    /// <returns> A <see cref="ByteVector"/> object containing the encoded representation of <paramref name="value" />. </returns>
    public static ByteVector ToByteVector(this Span<byte> value)
    {
        return new ByteVector(value);
    }
    /// <summary> Converts an first four bytes of the current instance to a <see cref="int" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="int"/> value containing the value read from the current instance. </returns>
    public static int ToInt(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToInt(mostSignificantByteFirst);
    }
    /// <summary> Converts an first four bytes of the current instance to a <see cref="uint" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="uint"/> value containing the value read from the current instance. </returns>
    public static uint ToUInt(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToUInt(mostSignificantByteFirst);
    }
    /// <summary> Converts an first two bytes of the current instance to a <see cref="short" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="short"/> value containing the value read from the current instance. </returns>
    public static  short ToShort(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToShort(mostSignificantByteFirst);
    }
    /// <summary> Converts an first two bytes of the current instance to a <see cref="ushort" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="ushort"/> value containing the value read from the current instance. </returns>
    public static  ushort ToUShort(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToUShort(mostSignificantByteFirst);
    }
    /// <summary> Converts an first eight bytes of the current instance to a <see cref="long" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="long"/> value containing the value read from the current instance. </returns>
    public static  long ToLong(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToLong(mostSignificantByteFirst);
    }
    /// <summary> Converts an first eight bytes of the current instance to a <see cref="ulong" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="ulong"/> value containing the value read from the current instance. </returns>
    public static  ulong ToULong(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToULong(mostSignificantByteFirst);
    }
    /// <summary>Converts an first four bytes of the current instance to a <see cref="float" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="float"/> value containing the value read from the current instance. </returns>
    public static float ToFloat(this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToFloat(mostSignificantByteFirst);
    }
    /// <summary> Converts an first eight bytes of the current instance to
    /// a <see cref="double" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns>  A <see cref="double"/> value containing the value read from the current instance. </returns>
    public static double ToDouble (this ByteVector value, bool mostSignificantByteFirst = true)
    {
        return value.Data.ToDouble(mostSignificantByteFirst);
    }
    /// <summary> Convert span of bytes to its Guid value </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <returns>Guid value represented by bytes</returns>
    public static Guid ToGuid(this ByteVector value)
    {
        return value.Data.ToGuid();
    }
    /// <summary> Creates a new instance of <see cref="ByteVector" /> by reading in the contents of a specified stream. </summary>
    /// <param name="stream">  A <see cref="Stream"/> object containing the stream to read. </param>
    /// <param name="count">Count of bytes read.</param>
    /// <returns>  A <see cref="ByteVector"/> object containing the contents of the specified stream. </returns>
    public static async Task<ByteVector> ToByteVectorAsync (this Stream stream, uint count = uint.MaxValue)
    {
        var vector = new ByteVector ();
        
        var takeCount = (int)Math.Min(count, stream.Length - stream.Position);
        vector.Resize( takeCount, null);
        var bytesRead = await stream.ReadAsync(vector.Memory).ConfigureAwait(false);
        if (bytesRead < takeCount)
        {
            vector.Resize(bytesRead);
        }

        return vector;
    }

    /// <summary> Creates a new instance of <see cref="ByteVector" /> by reading in the contents of a specified stream. </summary>
    /// <param name="stream">A <see cref="Stream"/> object containing the stream to read. </param>
    /// <param name="count">Count of bytes read.</param>
    /// <param name="copyFirstChunk"> A <see cref="bool" /> value specifying whether or not to copy the first chunk of the file into </param>
    /// <returns>
    ///     A <see cref="ByteVector" /> object containing the contents of the specified file.<br/>
    ///     In addition if <paramref name="copyFirstChunk"/> is <c>true</c>  this method is returning first data chunk from the read file.
    /// </returns>
    public static async Task<(ByteVector result, byte[] firstChunk)> ToByteVectorAsync (this Stream stream, bool copyFirstChunk, uint count = uint.MaxValue)
    {
        return MakeFirstChunk(await stream.ToByteVectorAsync(count).ConfigureAwait(false), copyFirstChunk );
    }

    private static (ByteVector result, byte[] firstChunk) MakeFirstChunk( ByteVector vector, bool copyFirstChunk)
    {
        byte[] firstChunk;
        if (copyFirstChunk)
        {
            firstChunk = new byte[Math.Min(4096, vector.Count)];
            vector.Data.CopyTo(new Span<byte>(firstChunk));
        }
        else
        {
            firstChunk = Array.Empty<byte>();
        }

        return (vector, firstChunk);
    }
}
