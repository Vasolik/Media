namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 MediaInformationBox.
/// <para>This box contains all the objects that declare characteristic information of the media in the track.</para> </summary>
[HasBoxFactory("minf")]
public class MediaInformationBox :  ContainerBox
{
    private MediaInformationBox (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
}