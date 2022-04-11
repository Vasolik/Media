namespace Vipl.Media.MP4.Boxes.ISO_14496_12.TrackReferenceTypes;

/// <summary>  This class extends <see cref="TrackReferenceTypeBox" /> to provide an implementation of a ISO/IEC 14496-12 TrackReferenceTypeBox.
/// <para>This is unknown track reference type box</para></summary>
[HasBoxFactory(null,  typeof(TrackReferenceBox))]
public class UnknownTrackReferenceTypeBox : TrackReferenceTypeBox
{
    private UnknownTrackReferenceTypeBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}