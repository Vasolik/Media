namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  <para> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 MediaDataBox.</para>
/// <para>This box contains the media data. In video tracks, this box would contain video frames.
/// A presentation may contain zero or more MediaDataBoxes. The actual media data follows the type field;
/// its structure is described by the structure-data</para> </summary>
[HasBoxFactory("mdat")]
public class MediaBox : Box
{

    private MediaBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    /// <inheritdoc />
    protected override Task<Box> InitAsync(MP4 file)
    {
        return Task.FromResult((Box)this);
    }

    /// <inheritdoc />
    public override ulong ActualSize => Size;
}