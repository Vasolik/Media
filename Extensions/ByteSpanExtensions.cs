namespace Vipl.Base.Extensions;

/// <summary>
/// Static class for extending span of bytes.
/// </summary>
public static class ByteSpanExtensions
{
    /// <summary> Converts an first four bytes of the current instance to a <see cref="int" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="int"/> value containing the value read from the current instance. </returns>
    public static int ToInt (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        var sum = 0;
        var last = value.Length > 4 ? 3 : value.Length - 1;

        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            unchecked {
                sum |= value[i] << (offset * 8);
            }
        }

        return sum;
    }

    /// <summary> Converts an first four bytes of the current instance to a <see cref="uint" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="uint"/> value containing the value read from the current instance. </returns>
    public static uint ToUInt (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        var sum = 0U;
        var last = value.Length > 4 ? 3 : value.Length - 1;

        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            sum |= (uint)value[i] << (offset * 8);
        }

        return sum;
    }
    /// <summary> Converts an first two bytes of the current instance to a <see cref="short" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="short"/> value containing the value read from the current instance. </returns>
    public static  short ToShort (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        short sum = 0;
        var last = value.Length > 2 ? 1 : value.Length - 1;
        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            unchecked {
                sum |= (short)(value[i] << (offset * 8));
            }
        }

        return sum;
    }
    /// <summary> Converts an first two bytes of the current instance to a <see cref="ushort" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="ushort"/> value containing the value read from the current instance. </returns>
    public static  ushort ToUShort(this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        ushort sum = 0;
        var last = value.Length > 2 ? 1 : value.Length - 1;
        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            sum |= (ushort)(value[i] << (offset * 8));
        }

        return sum;
    }
    /// <summary> Converts an first eight bytes of the current instance to a <see cref="long" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="long"/> value containing the value read from the current instance. </returns>
    public static  long ToLong (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        long sum = 0;
        var last = value.Length > 8 ? 7 : value.Length - 1;
        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            unchecked {
                sum |= (long)value[i] << (offset * 8);
            }
        }
        return sum;
    }
    /// <summary> Converts an first eight bytes of the current instance to a <see cref="ulong" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="ulong"/> value containing the value read from the current instance. </returns>
    public static ulong ToULong(this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        ulong sum = 0;
        var last = value.Length > 8 ? 7 : value.Length - 1;
        for (var i = 0; i <= last; i++) {
            var offset = mostSignificantByteFirst ? last - i : i;
            sum |= (ulong)value[i] << (offset * 8);
        }
        return sum;
    }
    /// <summary>Converts an first four bytes of the current instance to a <see cref="float" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns> A <see cref="float"/> value containing the value read from the current instance. </returns>
    public static float ToFloat (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        var bytes = value.ToArray();

        if (mostSignificantByteFirst) {
            Array.Reverse (bytes);
        }

        return BitConverter.ToSingle (bytes, 0);
    }
    /// <summary> Converts an first eight bytes of the current instance to
    /// a <see cref="double" /> value using big-endian format. </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <param name="mostSignificantByteFirst">
    /// <see langword="true"/> if the most significant byte is to appear first (big endian format), or
    /// <see langword="false"/> if the least significant byte is to appear first (little endian format).
    /// </param>
    /// <returns>  A <see cref="double"/> value containing the value read from the current instance. </returns>
    public static double ToDouble (this Span<byte> value, bool mostSignificantByteFirst = true)
    {
        var bytes = value.ToArray();

        if (mostSignificantByteFirst) {
            Array.Reverse (bytes);
        }
        return BitConverter.ToDouble (bytes, 0);
    }
    /// <summary> Convert span of bytes to its Guid value </summary>
    /// <param name="value">Bytes span to convert</param>
    /// <returns>Guid value represented by bytes</returns>
    public static Guid ToGuid(this Span<byte> value)
    {
        return new Guid(value);
    }
    /// <summary> Find index location where <paramref name="pattern"/> byte vectors occurs. </summary>
    /// <param name="value">Value to be searched.</param>
    /// <param name="pattern">Pattern to be searched.</param>
    /// <param name="offset">Starting index for search.</param>
    /// <param name="byteAlign">Alignment of resulting index. If result is found not align, that result would be ignored.</param>
    /// <returns>Index where <paramref name="pattern"/> was located first time in <paramref name="value"/>.
    /// If pattern was not found <c>-1</c></returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is negative or if byteAlign is less then 1.</exception>
    public static int Find(this Span<byte> value, Span<byte> pattern, int offset = 0, int byteAlign = 1)
    {
        if (offset < 0)
            throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0.");

        if (byteAlign < 1)
            throw new ArgumentOutOfRangeException(nameof(byteAlign), "byteAlign must be at least 1.");

        if (pattern.Length > value.Length - offset)
            return -1;
        
        if (pattern.Length == 1)
        {
            var p = pattern[0];
            for (var i = offset; i < value.Length; i += byteAlign)
                if (value[i] == p)
                    return i;
            return -1;
        }
        
        var lastOccurrence = new int[256];
        Array.Fill(lastOccurrence, pattern.Length);
        
        for (var i = 0; i < pattern.Length - 1; ++i)
            lastOccurrence[pattern[i]] = pattern.Length - i - 1;

        var endOfSearch = value.Length - pattern.Length + 1;
        var patternLastIndex = pattern.Length - 1;

        for (var i = offset; i < endOfSearch; i += lastOccurrence[value[i + patternLastIndex]])
        {
            if(i % byteAlign != 0)
                continue;
            if (value.Slice(i, pattern.Length).SequenceEqual( pattern))
            {
                return i;
            }
        }

        return -1;
    }
    
    /// <summary> Find index location where <paramref name="pattern"/> byte vectors occurs last.</summary>
    /// <param name="value">Value to be searched.</param>
    /// <param name="pattern">Pattern to be searched.</param>
    /// <param name="offset">Starting index for search.</param>
    /// <param name="byteAlign">Alignment of resulting index. If result is found not align, that result would be ignored.</param>
    /// <returns>Index where <paramref name="pattern"/> was located last time in <paramref name="value"/>
    /// If pattern was not found <c>-1</c></returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is negative or if byteAlign is less then 1.</exception>
    public static int ReverseFind(this Span<byte> value, Span<byte> pattern, int offset = 0, int byteAlign = 1)
    {

        if (offset < 0)
            throw new ArgumentOutOfRangeException(nameof(offset));

        if (pattern.Length == 0 || pattern.Length > value.Length - offset)
            return -1;
        
        if (pattern.Length == 1)
        {
            var p = pattern[0];
            for (var i = value.Length - offset - 1;
                 i >= 0;
                 i -= byteAlign)
                if (value[i] == p)
                    return i;
            return -1;
        }

        var firstOccurrence = new int[256];

        Array.Fill(firstOccurrence, pattern.Length);

        for (var i = pattern.Length - 1; i > 0; --i)
            firstOccurrence[pattern[i]] = i;

        for (var i = value.Length - offset - pattern.Length; i >= 0; i -= firstOccurrence[value[i]])
            if ((offset - i) % byteAlign == 0 && value.Slice(i, pattern.Length).SequenceEqual( pattern))
                return i;

        return -1;
    }
    /// <summary> Check if <paramref name="pattern"/> is placed from <paramref name="offset"/>.</summary>
    /// <param name="value">Value to be looked at for pattern.</param>
    /// <param name="pattern">Pattern to be searched for.</param>
    /// <param name="offset">Offset where pattern should appear.</param>
    /// <param name="patternOffset">From which index of pattern search in conducted.</param>
    /// <param name="patternLength">How much bytes of pattern is checked</param>
    /// <returns><c>true</c> if pattern is found <c>false</c> otherwise.</returns>
    public static bool ContainsAt(this Span<byte> value, Span<byte> pattern, int offset, int patternOffset = 0, int patternLength = int.MaxValue)
    {
        if (pattern.Length < patternLength) patternLength = pattern.Length;
        if (offset + patternLength - patternOffset > value.Length 
            || patternOffset + patternLength >= pattern.Length || patternLength <= 0 || offset < 0 || patternOffset < 0) return false;

        return value.Slice(offset, patternLength).SequenceEqual( pattern.Slice(patternOffset, patternLength));
    }
}