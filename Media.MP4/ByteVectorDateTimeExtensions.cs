using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4;

/// <summary> Extensions for converting between <see cref="ByteVector"/> and <see cref="DateTime"/>
/// or <see cref="TimeSpan"/> as they are stored in ISO/IEC 14496-12 file format. </summary>
public static class ByteVectorDateTimeExtensions
{
    /// <summary>Zero time as specified in ISO/IEC 14496-12. Creations and modification times are stored as seconds from this moment.</summary>
    public static readonly DateTime ZeroTime = new (1904, 1, 1, 0, 0, 0);

    /// <summary> Converts <see cref="ByteVector"/> to <see cref="DateTime"/>.
    /// Bytes <paramref name="value"/> are treated as 64 or 32 bits unsigned integer value of seconds
    /// since midnight of 01/01/1904. If <paramref name="value"/> have length of 8. 64 bits are used, 32 bits otherwise.</summary>
    /// <param name="value"> A <see cref="ByteVector"/> value to convert into <see cref="DateTime"/>.</param>
    /// <returns> A <see cref="DateTime"/> object with time interpretation of bytes in <see cref="ByteVector"/>. </returns>
    public static DateTime ToDateTime(this ByteVector value)=> value.Data.ToDateTime();
        
    /// <summary> Converts <see cref="Span{Byte}"/> to <see cref="DateTime"/>.
    /// Bytes <paramref name="value"/> are treated as 64 or 32 bits unsigned integer value of seconds
    /// since midnight of 01/01/1904. If <paramref name="value"/> have length of 8. 64 bits are used, 32 bits otherwise.</summary>
    /// <param name="value"> A <see cref="Span{Byte}"/> value to convert into <see cref="DateTime"/>.</param>
    /// <returns> A <see cref="DateTime"/> object with time interpretation of bytes in <see cref="Span{Byte}"/>. </returns>
    public static DateTime ToDateTime(this Span<byte> value)
    {
        return value.Length == 8 ? ZeroTime.AddSeconds((long)value.ToULong()) : ZeroTime.AddSeconds(value.ToUInt());
    }
        
    /// <summary> Converts <see cref="DateTime"/> to <see cref="ByteVector"/>.
    /// From <paramref name="value"/> is calculated number of seconds since midnight of 01/01/1904. That value is
    /// stored as 32 bit unsigned integer big endian byte representation.</summary>
    /// <param name="value"> A <see cref="DateTime"/> value to convert into <see cref="ByteVector"/>.</param>
    /// <returns> Big-endian byte representation of 32 bit integer value of seconds since midnight of 01/01/1904 of time stored in <paramref name="value"/>. </returns>
    /// <remarks>If 64 bit representation is needed use <see cref="ToByteVectorLong(System.DateTime)"/> method.</remarks>
    public static ByteVector ToByteVector(this DateTime value)
    {
        return ((uint)((value - ZeroTime).Ticks / TimeSpan.TicksPerSecond)).ToByteVector();
    }
    
    /// <summary> Converts <see cref="DateTime"/> to <see cref="ByteVector"/>.
    /// From <paramref name="value"/> is calculated number of seconds since midnight of 01/01/1904. That value is
    /// stored as 64 bit unsigned integer big endian byte representation.</summary>
    /// <param name="value"> A <see cref="DateTime"/> value to convert into <see cref="ByteVector"/>.</param>
    /// <returns> Big-endian byte representation of 32 bit integer value of seconds since midnight of 01/01/1904 of time stored in <paramref name="value"/>. </returns>
    /// <remarks>If 32 bit representation is needed use <see cref="ToByteVector(System.DateTime)"/> method.</remarks> 
    public static ByteVector ToByteVectorLong(this DateTime value)
    {
        return ((ulong)((value - ZeroTime).Ticks / TimeSpan.TicksPerSecond)).ToByteVector();
    }

    /// <summary> Add <see cref="DateTime"/> to <see cref="IByteVectorBuilder"/>.
    /// From <paramref name="value"/> is calculated number of seconds since midnight of 01/01/1904. That value is
    /// stored as 32 bit unsigned integer big endian byte representation.</summary>
    /// <param name="builder">A <see cref="IByteVectorBuilder"/> where these date will be stored.</param>
    /// <param name="value"> A <see cref="DateTime"/> value to add into <see cref="IByteVectorBuilder"/>.</param>
    /// <returns> <paramref name="builder"/> object to allow chaining. </returns>
    /// <remarks>If 64 bit representation is needed use <see cref="AddLong(IByteVectorBuilder, System.DateTime)"/> method.</remarks>
    public static IByteVectorBuilder Add(this IByteVectorBuilder builder,  DateTime value)
    {
        return builder.Add((uint)((value - ZeroTime).Ticks / TimeSpan.TicksPerSecond));
    }
    /// <summary> Add <see cref="DateTime"/> to <see cref="IByteVectorBuilder"/>.
    /// From <paramref name="value"/> is calculated number of seconds since midnight of 01/01/1904. That value is
    /// stored as 64 bit unsigned integer big endian byte representation.</summary>
    /// <param name="builder">A <see cref="IByteVectorBuilder"/> where these date will be stored.</param>
    /// <param name="value"> A <see cref="DateTime"/> value to add into <see cref="IByteVectorBuilder"/>.</param>
    /// <returns> <paramref name="builder"/> object to allow chaining. </returns>
    /// <remarks>If 32 bit representation is needed use <see cref="Add(IByteVectorBuilder, System.DateTime)"/> method.</remarks>
    public static IByteVectorBuilder AddLong(this IByteVectorBuilder builder,  DateTime value)
    {
        return builder.Add((ulong)(value - ZeroTime).Ticks / TimeSpan.TicksPerSecond);
    }
        
        
        
    /// <summary> Converts <see cref="ByteVector"/> to <see cref="TimeSpan"/>.
    /// Bytes <paramref name="value"/> are treated as 64 or 32 bits unsigned integer value
    /// of 1 / <paramref name="scale"/> fractions of seconds. </summary>
    /// <param name="value"> A <see cref="ByteVector"/> value to convert into <see cref="TimeSpan"/>. </param>
    /// <param name="scale"> Scale to be used. Every increment by 1 of <paramref name="value"/> will increment duration for 1/ <paramref name="scale"/> seconds.</param>
    /// <returns>A <see cref="TimeSpan"/> object with time interpretation of bytes in <see cref="ByteVector"/>. </returns>
    public static TimeSpan ToTimeSpan(this ByteVector value, uint scale)
    {
        return value.Data.ToTimeSpan(scale);
    }
    /// <summary> Converts <see cref="Span{Byte}"/> to <see cref="TimeSpan"/>.
    /// Bytes <paramref name="value"/> are treated as 64 or 32 bits unsigned integer value
    /// of 1 / <paramref name="scale"/> fractions of seconds. </summary>
    /// <param name="value"> A <see cref="Span{Byte}"/> value to convert into <see cref="TimeSpan"/>. </param>
    /// <param name="scale"> Scale to be used. Every increment by 1 of <paramref name="value"/> will increment duration for 1/ <paramref name="scale"/> seconds.</param>
    /// <returns>A <see cref="TimeSpan"/> object with time interpretation of bytes in <see cref="Span{Byte}"/>. </returns>
    public static TimeSpan ToTimeSpan(this Span<byte> value, uint scale)
    {
        return (value.Length == 8 ? TimeSpan.FromSeconds((long)value.ToULong()) : TimeSpan.FromSeconds(value.ToUInt()) ) / scale;
    }

    /// <summary> Converts <see cref="TimeSpan"/> to <see cref="ByteVector"/>.
    /// <paramref name="value"/> is transformed to big-endian representation of 32 bits unsigned integer value
    /// where every second is represented as  1 * <paramref name="scale"/>. </summary>
    /// <param name="value"> A <see cref="TimeSpan"/> value  to convert into <see cref="ByteVector"/>. </param>
    /// <param name="scale"> Scale to be used. Every increment by 1 second of <paramref name="value"/> will increment result by  <paramref name="scale"/> seconds.</param>
    /// <returns>Big-endian byte representation of 32 bit unsigned value of 1 / <paramref name="scale"/> fraction of second in <paramref name="value"/> . </returns>
    /// <remarks>If 64 bit representation is needed use <see cref="ToByteVectorLong(TimeSpan, uint)"/> method.</remarks>
    public static ByteVector ToByteVector(this TimeSpan value, uint scale)
    {
        return ((uint)(value.Ticks * scale / TimeSpan.TicksPerSecond)).ToByteVector();
    }
    /// <summary> Converts <see cref="TimeSpan"/> to <see cref="ByteVector"/>.
    /// <paramref name="value"/> is transformed to big-endian representation of 64 bits unsigned integer value
    /// where every second is represented as 1 * <paramref name="scale"/>. </summary>
    /// <param name="value"> A <see cref="TimeSpan"/> value  to convert into <see cref="ByteVector"/>. </param>
    /// <param name="scale"> Scale to be used. Every increment by 1 second of <paramref name="value"/> will increment result by  <paramref name="scale"/> seconds.</param>
    /// <returns>Big-endian byte representation of 64 bit unsigned value of 1 / <paramref name="scale"/> fraction of second in <paramref name="value"/> . </returns>
    /// <remarks>If 32 bit representation is needed use <see cref="ToByteVector(TimeSpan, uint)"/> method.</remarks>
    public static ByteVector ToByteVectorLong(this TimeSpan value, uint scale)
    {
        return ((ulong)(value.Ticks * scale / TimeSpan.TicksPerSecond )).ToByteVector();
    }
    /// <summary> Add <see cref="TimeSpan"/> to <see cref="IByteVectorBuilder"/>.
    /// <paramref name="value"/> is transformed to big-endian representation of 32 bits unsigned integer value
    /// where every second is represented as 1 * <paramref name="scale"/>.  </summary>
    /// <param name="builder">A <see cref="IByteVectorBuilder"/> where these date will be stored.</param>
    /// <param name="value"> A <see cref="TimeSpan"/> value to add into <see cref="IByteVectorBuilder"/>.</param>
    /// <param name="scale"> Scale to be used. Every increment by 1 second of <paramref name="value"/> will increment result by  <paramref name="scale"/> seconds.</param>
    /// <returns> <paramref name="builder"/> object to allow chaining. </returns>
    /// <remarks>If 64 bit representation is needed use <see cref="AddLong(IByteVectorBuilder, TimeSpan, uint)"/> method.</remarks>
    public static IByteVectorBuilder Add(this IByteVectorBuilder builder,  TimeSpan value, uint scale)
    {
        return builder.Add((uint)(value.Ticks * scale / TimeSpan.TicksPerSecond ));
    }
    /// <summary> Add <see cref="TimeSpan"/> to <see cref="IByteVectorBuilder"/>.
    /// <paramref name="value"/> is transformed to big-endian representation of 64 bits unsigned integer value
    /// where every second is represented as 1 * <paramref name="scale"/>.  </summary>
    /// <param name="builder">A <see cref="IByteVectorBuilder"/> where these date will be stored.</param>
    /// <param name="value"> A <see cref="TimeSpan"/> value to add into <see cref="IByteVectorBuilder"/>.</param>
    /// <param name="scale"> Scale to be used. Every increment by 1 second of <paramref name="value"/> will increment result by  <paramref name="scale"/> seconds.</param>
    /// <returns> <paramref name="builder"/> object to allow chaining. </returns>
    /// <remarks>If 32 bit representation is needed use <see cref="Add(IByteVectorBuilder, TimeSpan, uint)"/> method.</remarks>
    public static IByteVectorBuilder AddLong(this IByteVectorBuilder builder,  TimeSpan value, uint scale)
    {
        return builder.Add((ulong)(value.Ticks * scale / TimeSpan.TicksPerSecond ));
    }
}