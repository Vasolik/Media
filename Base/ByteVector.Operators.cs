using System.Text;
using Vipl.Base.Extensions;

namespace Vipl.Base;

public sealed partial class ByteVector
{
    /// <summary> Converts a <see cref="T:byte[]" /> to a new <see cref="ByteVector" /> object. </summary>
    /// <param name="value"> A <see cref="T:byte[]" /> to convert. </param>
    /// <returns> A new instance of <see cref="ByteVector" /> containing the contents of <paramref name="value" />. </returns>
    public static implicit operator ByteVector(byte[] value)
    {
        return new ByteVector(value);
    }
    
    /// <summary> Converts a <see cref="Span{Byte}" /> to a new <see cref="ByteVector" /> object. </summary>
    /// <param name="value"> A <see cref="Span{Byte}" /> to convert. </param>
    /// <returns> A new instance of <see cref="ByteVector" /> containing the contents of <paramref name="value" />. </returns>
    public static implicit operator ByteVector(Span<byte> value)
    {
        return new ByteVector(value);
    }
    /// <summary> Converts a <see cref="ByteVector" /> to a <see cref="Span{Byte}" /> object. </summary>
    /// <param name="value"> A <see cref="ByteVector" /> to convert. </param>
    /// <returns> A instance of <see cref="Span{Byte}" /> containing the contents of <paramref name="value"/>. </returns>
    public static implicit operator Span<byte>(ByteVector value)
    {
        return value.Data;
    }
    
    /// <summary> Converts a <see cref="string" /> to a new <see cref="ByteVector" /> object. </summary>
    /// <param name="value"> A <see cref="string" /> to convert. </param>
    /// <returns> A new instance of <see cref="ByteVector" /> containing the UTF-8 encoded contents of <paramref name="value" />. </returns>
    public static explicit operator ByteVector(string value)
    {
        return FromString(value, Encoding.UTF8);
    }
    /// <summary> Converts a <see cref="ByteVector" /> to a <see cref="string" />  object. </summary>
    /// <param name="value"> A <see cref="ByteVector" /> to convert. </param>
    /// <returns> A instance of <see cref="string"/> containing the contents of <paramref name="value" />. </returns>
    public static explicit operator string(ByteVector value)
    {
        return value.ToString();
    }
    
}