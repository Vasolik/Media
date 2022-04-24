using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12.DataEntries;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of Apple QuickTime DataAtom.</summary>  
[HasBoxFactory("data" ) ]
public abstract class DataAtom  : BoxWithData
{
    /// <inheritdoc />
    protected DataAtom(BoxHeader header, IsoHandlerBox? handler, DataAtomTypes type)
        : base(header, handler)
    {
        DataType = type;
    }

    /// <summary> Types of data that can be stored in a DataAtom. </summary>
    public enum DataAtomTypes
    {
        /// <summary> Reserved for use where no type needs to be indicated. </summary>
        Reserved = 0,
        /// <summary> Without any count or NULL terminator </summary>
        Utf8 = 1,
        /// <summary> Also known as UTF-16BE. </summary>
        Utf16 = 2,
        /// <summary> Deprecated unless it is needed for special Japanese characters. </summary>
        SJis = 3,
        /// <summary> Variant storage of a string for sorting only. </summary>
        Utf8Sort = 4,
        /// <summary>Variant storage of a string for sorting only. </summary>
        Utf16Sort = 5,
        /// <summary>In a JFIF wrapper. </summary>
        Jpeg = 13,
        /// <summary> In a PNG wrapper </summary>
        Png = 14,
        /// <summary>A big-endian signed integer in 1,2,3 or 4 bytes.</summary>
        BigEndianSignedInteger = 21,
        /// <summary> A big-endian unsigned integer in 1,2,3 or 4 bytes; size of value
        /// determines integer size. </summary>
        BigEndianUnsignedInteger = 22,
        /// <summary> A big-endian 32-bit floating point value (IEEE754). </summary>
        BigEndianFloat = 23,
        /// <summary> A big-endian 64-bit floating point value (IEEE754). </summary>
        BigEndianDouble = 24,
        /// <summary> Windows bitmap format graphics. </summary>
        Bmp = 27,
        /// <summary>A block of data having the structure of the Metadata atom
        /// defined in this specification </summary>
        QuickTimeMetadata = 28,
		
    }
	
    /// <summary>Factory for creating and initializing the boxes.</summary>
    /// <param name="header">Header of the box.</param>
    /// <param name="file">File from witch box will be initialized.</param>
    /// <param name="handlerBox">Iso handler for better understanding of box.</param>
    /// <typeparam name="T">Type of the box</typeparam>
    /// <returns>Newly created box.</returns>
    public new static async Task<Box> CreateAsync<T>(BoxHeader header, MP4 file, IsoHandlerBox? handlerBox)
        where T : Box
    {
        file.Seek((long)header.DataPosition, SeekOrigin.Begin);
        var typeVector = await file.ReadBlockAsync(8);
        var type = (DataAtomTypes)typeVector[..4].ToUInt();
        file.Seek((long)header.DataPosition, SeekOrigin.Begin);
        var result =(DataAtom) await ((DataAtom)(type switch
        {
            DataAtomTypes.Utf8 => new Utf8DataAtom(header, handlerBox, type),
            _ => new UnknownDataAtom(header, handlerBox, type)
        })).InitAsync(file);
        result.LocalIndicator = typeVector[4..8].ToUInt();
        return result;
    }
	
    /// <summary> Gets the type of the data stored in the atom. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public DataAtomTypes DataType { get;  set; }
	
    /// <summary> The locale indicator is formatted as a four-byte value. It is formed from two two-byte values: a country indicator,
    /// and a language indicator. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint LocalIndicator { get; set; }
	
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(((uint)DataType));
        builder.Add((LocalIndicator));
        return builder;
    }
	
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {DataType}:{LocalIndicator}";
	
	
	
}