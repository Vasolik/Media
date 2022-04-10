namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 EditBox.
/// <para> An EditBox maps the presentation timeline to the media timeline as it is stored
/// in the file. The EditBox is a container for the edit lists. </para>
/// <para> The EditBox is optional. In the absence of this box, there is an implicit one-to-one
/// mapping of these timelines, and the presentation of a track starts at the beginning of the
/// presentation. An empty edit is used to offset the start time of a track.</para> </summary>
[HasBoxFactory("edts")]
public class EditBox : ContainerBox
{
    private EditBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}