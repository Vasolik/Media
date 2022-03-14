using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Vipl.Base.Extensions;

/// <summary>
/// Extensions for <see cref="string"/>
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Format <paramref name="text"/> with <paramref name="args"/> list of params.
    /// </summary>
    /// <param name="text">Text to be formatted.</param>
    /// <param name="args">Args to format text.</param>
    /// <returns>Formatted text.</returns>
    public static string Format(this string text, params object[] args)
    {
        return string.Format(text, args);
    }
    /// <summary>
    /// Check if <paramref name="value"/> is <c>null</c> or empty string
    /// </summary>
    /// <param name="value">String to be checked.</param>
    /// <returns><c>true</c> if string is <c>null</c> or empty, <c>false</c> otherwise.</returns>

    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
    {
        return string.IsNullOrEmpty(value);
    }
    /// <summary>
    /// Check if <paramref name="value"/> is <c>null</c> or white spaced string
    /// </summary>
    /// <param name="value">String to be checked.</param>
    /// <returns><c>true</c> if string is <c>null</c> or white space, <c>false</c> otherwise.</returns>
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    /// <summary>
    /// Check if <paramref name="text"/> contains any of strings inside <paramref name="testMatches"/> array.
    /// </summary>
    /// <param name="text">string to be checked.</param>
    /// <param name="testMatches">Array of strings to find in <paramref name="text"/></param>
    /// <returns><c>true</c> if any match is found, <c>false</c> otherwise.</returns>
    public static bool ContainsAny(this string text, params string[] testMatches)
    {
        return testMatches.Any(text.Contains);
    }
    /// <summary>
    /// Convert <paramref name="valueToNulInt"/> to int value, or null if converting is impossible.
    /// </summary>
    /// <param name="valueToNulInt">String to be converted.</param>
    /// <returns>Value of string, or null.</returns>
    public static int? ToNullableInt(this string valueToNulInt)
    {
        return int.TryParse(valueToNulInt, out var i) ? i : default(int?);
    }
    /// <summary>
    /// Convert <see cref="Encoding.BigEndianUnicode"/> hex string to string
    /// </summary>
    /// <param name="text"><see cref="Encoding.BigEndianUnicode"/> encoded string.</param>
    /// <returns>Converted string.</returns>
    public static string ConvertUnicodeHexStringToText(this string text)
    {
        return Encoding.BigEndianUnicode.GetString(Enumerable.Range(0, text.Length / 2).Select(i => Convert.ToByte(text.Substring(i * 2, 2), 16)).ToArray());
    }
    /// <summary>
    /// Get Md5 hash bytes of <paramref name="inputString"/>.
    /// </summary>
    /// <param name="inputString"><see cref="string"/> which needs to be converted</param>
    /// <returns>Md5 hash bytes of <paramref name="inputString"/></returns>
    public static byte[] ToMd5HashBytes(this string inputString)
    {
        using HashAlgorithm algorithm = MD5.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    /// <summary>
    /// Convert <paramref name="inputString"/> to md5 hash.
    /// </summary>
    /// <param name="inputString">Value to be converted.</param>
    /// <returns>Md5 hash value of <paramref name="inputString"/></returns>
    public static string ToMd5Hash(this string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in inputString.ToMd5HashBytes())
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
    /// <summary>
    /// Get SHA1 hash bytes of <paramref name="inputString"/>.
    /// </summary>
    /// <param name="inputString"><see cref="string"/> which needs to be converted</param>
    /// <returns>SHA1 hash bytes of <paramref name="inputString"/></returns>
    public static byte[] ToSha1HashBytes(this string inputString)
    {
        using HashAlgorithm algorithm = SHA1.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    /// <summary>
    /// Convert <paramref name="inputString"/> to SHA1 hash.
    /// </summary>
    /// <param name="inputString">Value to be converted.</param>
    /// <returns>SHA1 hash value of <paramref name="inputString"/></returns>
    public static string ToSha1Hash(this string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in inputString.ToSha1HashBytes())
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
    /// <summary>
    /// Remove characters from string witch are not supported by XML.
    /// </summary>
    /// <param name="txt"><see cref="string"/> to be immunized.</param>
    /// <returns><see cref="string"/> without unsupported xml characters.</returns>
    public static string ImmunizeToXmlString(this string txt)
    {
        var r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26\xD800-\xDFFF\xFFFF]";
        return Regex.Replace(txt, r, string.Empty, RegexOptions.Compiled);
    }
    /// <summary>
    /// Move avery line of string (where line is detonated with <paramref name="separator"/>
    /// for <paramref name="count"/> number of <paramref name="intendString"/> characters.
    /// </summary>
    /// <param name="value">Value to be intended</param>
    /// <param name="count">Number of times string is intended</param>
    /// <param name="first">Does first line needs to be intended.</param>
    /// <param name="separator">End of line separator.</param>
    /// <param name="intendString">String used to move lines.</param>
    /// <returns>Sting in which all lines are moved with intend text.</returns>
    public static string Intend(this string value, int count, bool first = false, string separator = "\n", string intendString = "    ")
    {
        var indent = string.Join("", Enumerable.Repeat(intendString, count));
        return (first ? indent: "") + value.Replace(separator, separator + indent);
    }
    /// <summary>
    /// Split string in string tokens.
    /// </summary>
    /// <param name="value">Value to be split</param>
    /// <param name="allWhite">Should splitting by all white characters. If set to false <paramref name="separators"/> will be used.</param>
    /// <param name="separators">U</param>
    /// <returns>Array of tokens.</returns>
    public static string[] Tokenized(this string value, bool allWhite = false, string separators = "\n\r")
    {
        if (allWhite)
        {
            separators = "\t\n\r ";
        }
        return value.Split(separators.ToArray())
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();
    }
    /// <summary>
    /// Join enumeration of string to one string using separator <paramref name="separator"/> between them.
    /// </summary>
    /// <param name="values">Strings to be joined</param>
    /// <param name="separator">Separator between joined strings</param>
    /// <returns>Newly created string.</returns>
    public static string Join (this IEnumerable<string?> values, string separator = "\n")
    {
        return string.Join(separator, values.Where(v => v is not null));
    }
    /// <summary>
    /// Join enumeration of string to one string using separator <paramref name="separator"/> between them.
    /// Move avery line of newly string (where line is detonated with <paramref name="separator"/>
    /// for <paramref name="intendCount"/> number of <paramref name="intendString"/> characters.
    /// </summary>
    /// <param name="values">Strings to be joined</param>
    /// <param name="separator">Separator between joined strings</param>
    /// <param name="intendCount">Number of times string is intended</param>
    /// <param name="first">Does first line needs to be intended.</param>
    /// <param name="intendString">String used to move lines.</param>
    /// <returns>Newly created string.</returns>
    public static string JoinAndIntend (this IEnumerable<string?> values, int intendCount = 0,  bool first = false, string separator = "\n", string intendString = "    ")
    {
        return values.Join(separator).Intend(intendCount, first, separator, intendString);
    }
    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="value">Array of bytes to be converted</param>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <param name="offset">Starting offset from where characters will be taken</param>
    /// <param name="count">Number of byte which will be taken</param>
    /// <returns>Resulting string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset or count is out of range of value.</exception>
    public static string ToString(this Span<byte> value, Encoding encoding, int offset, int count)
    {
        if (offset < 0 || offset > value.Length)
            throw new ArgumentOutOfRangeException(nameof(offset));

        if (count < 0 || count + offset >  value.Length)
            throw new ArgumentOutOfRangeException(nameof(count));

        var bom = (Equals(encoding, Encoding.Unicode) || Equals(encoding, Encoding.BigEndianUnicode)) &&
                  value.Length - offset > 1
            ?  value.Slice(offset, 2)
            : null;
        
        encoding = Equals(Encoding.Unicode, encoding) ? GetBomEncoding(bom) : encoding;

        var s = encoding.GetString(value.ToArray(), offset, count);

        // UTF16 BOM
        if (s.Length != 0 && (s[0] == 0xfffe || s[0] == 0xfeff))
            return s[1..];

        return s;
    }
    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="value">Array of bytes to be converted</param>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <param name="offset">Starting offset from where characters will be taken</param>
    /// <returns>Resulting string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If offset is out of range of value.</exception>
    public static string ToString(this Span<byte> value, Encoding encoding, int offset)
    {
        return value.ToString(encoding, offset, value.Length - offset);
    }

    /// <summary> Converts span of bytes to string using <paramref name="encoding"/> encoding. </summary>
    /// <param name="value">Array of bytes to be converted</param>
    /// <param name="encoding">Encoding used in conversion.</param>
    /// <returns>Resulting string.</returns>
    public static string ToString(this Span<byte> value, Encoding encoding)
    {
        return value.ToString(encoding, 0, value.Length);
    }
    
    /// <summary> Get encoding from bom bytes </summary>
    /// <param name="bom">Bom bytes</param>
    /// <returns>Encoding written in bom bytes.</returns>
    private static Encoding GetBomEncoding(Span<byte> bom)
    {
        if (bom[0] == 0xFF && bom[1] == 0xFE)
            return Encoding.Unicode;

        if (bom[1] == 0xFF && bom[0] == 0xFE)
            return Encoding.BigEndianUnicode;

        return Encoding.Unicode;
    }
    
}