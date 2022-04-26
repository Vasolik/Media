using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 DataReferenceBox.
/// <para>The data reference object contains a table of data references (normally URLs) that declare the location(s)
/// of the media data used within the presentation. The data reference index in the sample description ties
/// entries in this table to the samples in the track. A track may be split over several sources in this way.</para>
/// <para>If the flag is set indicating that the data is in the same file as this box, then no string (not even an empty
/// one) shall be supplied in the entry field.</para>
/// <para>The <see cref="EntryCount"/> in the DataReferenceBox shall be 1 or greater.</para>
/// <para>When a file that has data entries with the flag set indicating that the media data is in the same file,
/// is split into segments for transport, the value of this flag does not change, as the file is (logically)
/// reassembled after the transport operation.</para> </summary>
[HasBoxFactory("dref")]
// ReSharper disable once ClassNeverInstantiated.Global
public class DataReferenceBox : FullContainerBox
{
    private DataReferenceBox (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
	
    /// <inheritdoc />
    protected override async Task<Box> InitAsync(MP4 file)
    {
        file.Seek((long)base.DataPosition , SeekOrigin.Begin);
        EntryCount = (await file.ReadBlockAsync (4)).ToUInt();
        await this.LoadChildrenAsync (file);
        return this;
    }

    /// <summary> Integer that counts the actual entries</summary>
    public uint EntryCount { get;  set; }
    /// <inheritdoc />
    public override IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        Header.Render(builder)
            .Add(Version)
            .Add(Flags.ToByteVector().Mid (1, 3))
            .Add(EntryCount);
		
        Children.ForEach(c => c.Render(builder));
        return builder;
    }
    /// <inheritdoc />
    public override ulong DataPosition => base.DataPosition + 4;

    /// <inheritdoc />
    public override ulong DataSize => base.DataSize - 4;
    
    /// <inheritdoc />
    public override ulong ActualSize => base.ActualSize + 4;   

}