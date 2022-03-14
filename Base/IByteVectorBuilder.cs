using System.Text;

namespace Vipl.Base;

/// <summary>
/// Interface for used for joining different data in one <see cref="ByteVector"/>.
/// </summary>
public interface IByteVectorBuilder
{
    /// <summary> Current position in built <see cref="ByteVector"/>. Next added data will be starting at this position </summary>
    public long Position { get; set; }
    /// <summary> Capacity of currently build <see cref="ByteVector"/>. Final result will have this size. </summary>
    public long Capacity{ get; set; }
    
    /// <summary> Add <see cref="int"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="int"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(int value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="uint"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="uint"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(uint value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="string"/> to byte vector. </summary>
    /// <param name="text">A <see cref="string"/> value to convert into bytes.</param>
    /// <param name="encoding">Encoding used to convert to bytes</param>
    /// <param name="length">Number of bytes added to byte vector.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(string text, Encoding encoding, int length);
    /// <summary> Add <see cref="string"/> to byte vector. </summary>
    /// <param name="text">A <see cref="string"/> value to convert into bytes.</param>
    /// <param name="encoding">Encoding used to convert to bytes</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add (string text, Encoding encoding);
    /// <summary> Add <see cref="string"/> to byte vector. </summary>
    /// <param name="text">A <see cref="string"/> value to convert into bytes.</param>
    /// <param name="length">Number of bytes added to byte vector.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add (string text, int length);
    /// <summary> Add <see cref="string"/> to byte vector. </summary>
    /// <param name="text">A <see cref="string"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(string text);
    /// <summary> Add <see cref="short"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="short"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add ( short value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="ushort"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="ushort"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add (ushort value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="long"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="long"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add ( long value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="ulong"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="ulong"/> value to convert into bytes.</param>
    /// <param name="mostSignificantByteFirst"> <see langword="true" /> if the most significant byte is to appear first (big endian format),
    /// or <see langword="false" /> if the least significant byte is to appear first (little endian format). </param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add ( ulong value, bool mostSignificantByteFirst = true);
    /// <summary> Add <see cref="Guid"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="Guid"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(Guid value);
    /// <summary> Add <see cref="ByteVector"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="ByteVector"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(ByteVector value);
    /// <summary> Add <see cref="T:byte[]"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="T:byte[]"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(byte[] value);
    /// <summary> Add <see cref="T:Span{byte}"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="T:Span{byte}"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(Span<byte> value);
    /// <summary> Add <see cref="T:byte"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="T:byte"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(byte value);
    /// <summary> Add <see cref="FixedPoint8_8"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="FixedPoint8_8"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(FixedPoint8_8 value);
    /// <summary> Add <see cref="FixedPoint16_16"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="FixedPoint16_16"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(FixedPoint16_16 value);
    /// <summary> Add <see cref="FixedPoint2_30"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="FixedPoint2_30"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(FixedPoint2_30 value);
    /// <summary> Add <see cref="TransformationMatrix"/> to byte vector. </summary>
    /// <param name="value"> A <see cref="TransformationMatrix"/> value to convert into bytes.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Add(TransformationMatrix value);
    /// <summary> Skip number of bytes in builder. Those bytes will have whatever was there before. </summary>
    /// <param name="size">Number of bytes skipped.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Skip(uint size);
    /// <summary> Set to 0 <paramref name="size"/> bytes. </summary>
    /// <param name="size">Number of bytes to clear.</param>
    /// <returns> <c>this</c> object to allow chaining. </returns>
    public IByteVectorBuilder Clear(uint size);
    /// <summary> Returned built byte vector. </summary>
    /// <returns>Built byte vector</returns>
    public ByteVector Build();

}