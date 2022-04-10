using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 SampleDescriptionBox.
/// <para>The sample description table gives detailed information about the coding type used, and any
/// initialization information needed for that coding. The syntax of the sample entry used is determined by
/// both the format field and the media handler type.</para>
/// <para>The information stored in the <see cref="SampleDescriptionBox"/>  after the entry-count is both track-type specific
/// as documented here, and can also have variants within a track type (e.g. different codings may use
/// different specific information after some common fields, even within a video track).</para>
/// <para>Which type of sample entry form is used is determined by the media handler, using a suitable form,
/// such as one defined in Clause 12, or defined in a derived specification, or registration.</para>
/// <para>Multiple descriptions may be used within a track.</para> </summary>
[HasBoxFactory("stsd")]
// ReSharper disable once ClassNeverInstantiated.Global
public class SampleDescriptionBox : FullContainerBox
{
    private SampleDescriptionBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
	
    /// <inheritdoc />
    protected override async Task<Box> InitAsync(MP4 file)
    {
        EntryCount = (await file.ReadBlockAsync (4)).ToUInt();
        await this.LoadChildrenAsync (file);
        return this;
    }

    /// <summary>  Integer that gives the number of entries in the following table </summary>
    public uint EntryCount { get; private set; }
    /// <inheritdoc />
    public override IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        Header.Render(builder)
            .Add(Version)
            .Add(Flags.ToByteVector().Mid (1, 3))
            .Skip(4)
            .Add(EntryCount);
		
        Children.ForEach(c => c.Render(builder));
        return builder;
    }
    /// <inheritdoc />
    public override ulong DataPosition => base.DataPosition + 4;

    /// <inheritdoc />
    public override ulong DataSize => base.DataSize - 4;
}