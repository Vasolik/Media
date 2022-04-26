namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 MediaBox.
/// <para>The media declaration container contains all the objects that declare information about the media data
/// within a track.</para> </summary>
[HasBoxFactory("mdia")]
public class MediaBox :  ContainerBox
{
    private MediaBox (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
}