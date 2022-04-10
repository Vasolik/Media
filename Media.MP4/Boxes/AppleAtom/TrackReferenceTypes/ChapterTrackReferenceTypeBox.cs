using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.TrackReferenceTypes;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackReferenceTypeBox.
/// <para>Chapter or scene list. Usually references a text track.</para></summary>
[HasBoxFactory("chap", typeof(TrackReferenceBox))]
public class ChapterTrackReferenceTypeBox : TrackReferenceTypeBox
{
    private ChapterTrackReferenceTypeBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}