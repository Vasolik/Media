namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 MovieBox.
/// <para>This box warns readers that there might be MovieFragmentBoxes in this file. To know of all samples
/// in the tracks, these MovieFragmentBoxes must be found and scanned in order, and their information
/// logically added to that found in the Movie Box.</para> </summary>
[HasBoxFactory("moov")]
public class MovieBox :  ContainerBox
{
    private MovieBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}