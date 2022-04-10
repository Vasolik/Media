namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackReferenceTypeBox.
/// <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
/// enclosing track has one of more references of that type. Each reference type shall occur at most once.
/// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
/// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
/// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
/// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
/// sized to fill the reference type box.</para>
/// <para>Links a shadow sync track to a main track.</para></summary>
[HasBoxFactory("cdtg")]
public abstract class GroupTrackReferenceBox : TrackReferenceTypeBox
{
    private GroupTrackReferenceBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}