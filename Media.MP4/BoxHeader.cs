using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.Core;
using Vipl.Media.MP4.Boxes;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4;

/// <summary><para> This structure provides support for reading and writing headers for ISO/IEC 14496-12 boxes.</para>
///<para>Boxes start with a header which gives both size and type. The header permits compact or extended size
/// (32 or 64 bits) and compact or extended types (32 bits or full universal unique identifiers, i.e. UUIDs).
/// The standard boxes all use compact types (32-bit) and most boxes will use the compact (32-bit) size.
/// Typically, only the <see cref="MediaDataBox"/>  needs the 64-bit size.</para>
/// <para>  To permit ease of identification, the 32-bit compact type can be expressed as four characters from the
/// range 0020 to 007E, inclusive, of ASCII.  The four individual byte values of the field are placed in order in the file. </para> 
/// <para> The size is the entire size of the box, including the size and type header, fields, and all contained boxes.</para>
/// <para> User extensions use an extended box type; in this case, the type field is set to 'uuid'.</para>
/// </summary>
public class BoxHeader
{
    private const int NotFromFilePosition = -1;
    

#pragma warning disable CS8618
    private BoxHeader(){ }
#pragma warning disable CS8618
    private async Task<BoxHeader> InitAsync(MP4 mediaFile, long position)
    {
        File = mediaFile;
        Position = position;
        mediaFile.Seek (position, SeekOrigin.Begin);

        var data = await mediaFile.ReadBlockAsync (32);
        if (data.Count < 8)
            throw new CorruptFileException ("Not enough data in box header.");
        
        ulong size = data[..4].ToUInt ();
        BoxType = BoxType.FromByteVector(data[4..8].ToByteVector());
			
        var offset = 0;
            
        if (size == 1) {
            offset += 8;
                
            if (data.Count < 8 + offset)
                throw new CorruptFileException ("Not enough data in box header.");
                
            size = data[8..16].ToULong();
        }
        
        // UUID has a special header with 16 extra bytes.
        if (BoxType == BoxType.Uuid) {
            if (data.Count < 16 + offset)
                throw new CorruptFileException ("Not enough data in box header.");
            
            ExtendedType = data.Mid (offset, 16).ToGuid();
        } 

        if (size > (ulong)(mediaFile.Length - position)) {
            throw new CorruptFileException (
                $"Box header specified a size of {TotalBoxSize} bytes but only {mediaFile.Length - position} bytes left in the file");
        }

        DataSize = size - (DataPosition - (ulong)Position);
        
        Box = null;
        return this;
    }

    /// <summary> Constructs and initializes a new instance of <see  cref="BoxHeader" /> by
    /// reading it from a specified seek position in a specified file. </summary>
    /// <param name="mediaFile"> A <see cref="MP4" /> object to read the new instance from. </param>
    /// <param name="position"> A <see cref="long" /> value specifying the seek position
    /// in <paramref name="mediaFile" /> at which to start reading. </param>
    /// <exception cref="CorruptFileException"> There isn't enough data in the file to read the complete header or for complete box.</exception>
    public static async Task<BoxHeader> CreateAsync(MP4 mediaFile, long position)
    {
        return await new BoxHeader().InitAsync(mediaFile, position);
    }
        
    /// <summary> Constructs and initializes a new instance of <see  cref="BoxHeader" /> with a specified box type. </summary>
    /// <param name="type"> A <see cref="ByteVector" /> object containing the four byte box type. </param>
    /// <exception cref="ArgumentException">When <paramref name="type" /> isn "<c>uuid</c>".
    /// For creating box header with extended type use <see cref="BoxHeader(Guid)"/> constructor.</exception>
    public BoxHeader (BoxType type)
    {
        Position = NotFromFilePosition;
        Box = null;
        BoxType = type;
        if (BoxType == BoxType.Uuid) 
        {
            throw new ArgumentException ("For creating header with extended type use Header(Guid) constructor.");
        }
        ExtendedType = null;
    }
        
    /// <summary> Constructs and initializes a new instance of <see  cref="BoxHeader" />
    /// with a specified box extended type. </summary>
    /// <param name="extendedType"> A <see cref="ByteVector" /> object containing the "<c>uuid</c>" box type. </param>
    public BoxHeader (Guid extendedType)
    {
        Position = NotFromFilePosition;
        Box = null;
        BoxType = BoxType.Uuid;
        ExtendedType = extendedType;
    }
    
    /// <summary> Gets the type of box represented by the current instance. </summary>
    /// <value> A <see cref="ByteVector" /> object containing the 4 byte box type. </value>
    public BoxType BoxType { get; private set; }

    /// <summary> Gets the extended type of the box represented by the current instance. </summary>
    /// <value>A <see cref="Guid" /> object containing the 16 byte extended type,
    /// or <see langword="null" /> if <see cref="BoxType" /> is not "<c>uuid</c>". </value>
    public Guid? ExtendedType { get; private set; }

    /// <summary> Gets the size of the header represented by the current instance. </summary>
    /// <value> A <see cref="long" /> value containing the size of the header represented by the current instance. </value>
    public uint HeaderSize =>
        8u + (DataSize > uint.MaxValue ? 8u : 0u)
           + (BoxType == BoxType.Uuid ? 16u : 0u);

    /// <summary>Gets and sets the size of the data in the box described by the current instance. </summary>
    /// <value> A <see cref="long" /> value containing the size of the
    /// data in the box described by the current instance. </value>
    public ulong DataSize { get; set; }

    /// <summary> Gets the position box data represented by the current instance in the file it comes from.</summary>
    public ulong DataPosition => (ulong)Position + HeaderSize;

    /// <summary> Gets the total size of the box described by the current instance. </summary>
    /// <value> A <see cref="ulong" /> value containing the total size of  the box described by the current instance. </value>
    public ulong TotalBoxSize => HeaderSize + DataSize;

    /// <summary> Gets the position box represented by the current instance in the file it comes from. </summary>
    public long Position { get; private set; }
        
    /// <summary>  Renders the header represented by the current instance. </summary>
    /// <returns> A <see cref="ByteVector" /> object containing the rendered version of the current instance. </returns>
    public IByteVectorBuilder Render()
    {
        return Render(new ByteVectorBuilder((int) HeaderSize));
    }
        
    /// <summary> Add content of byte header inside of <see cref="IByteVectorBuilder"/>. </summary>
    /// <param name="builder">Builder where data will be stored.</param>
    /// <returns><paramref name="builder"/> to allow chaining.</returns>
    public IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        builder.Add((uint) (TotalBoxSize <= uint.MaxValue ? TotalBoxSize : 1))
            .Add(BoxType.Header);
        if (TotalBoxSize > uint.MaxValue)
            builder.Add(TotalBoxSize);
        if (BoxType != BoxType.Uuid) return builder;
        if (ExtendedType != null)
        {
            builder.Add(TotalBoxSize);
        }
        else
        {
            builder.Position += 16;
        }

        return builder;
    }


    /// <summary> Constructs and initializes a new instance of <see cref="BoxHeader" /> with a specified box type. </summary>
    /// <param name="type"> A <see cref="ByteVector" /> object containing the four byte box type. </param>
    /// <exception cref="ArgumentException">When <paramref name="type" /> isn "<c>uuid</c>".
    /// For creating box header with extended type use <see cref="BoxHeader(Guid)"/> constructor.</exception>
    public static implicit operator BoxHeader(BoxType type) => new (type);
    
    /// <summary>
    ///    Gets and sets the box represented by the current instance
    ///    as a means of temporary storage for internal uses.
    /// </summary>
    internal Box? Box { get; set; }
    
    /// <summary>MP4 file where this header belongs.</summary>
    public MP4? File { get; set; }

    /// <summary>This is an integer that specifies the time-scale for the entire presentation; this is the number of
    /// time units that pass in one second. For example, a time coordinate system that measures time in
    /// sixtieths of a second has a time scale of 60. </summary>
    public uint Timescale => File?.Timescale ?? 1;

}