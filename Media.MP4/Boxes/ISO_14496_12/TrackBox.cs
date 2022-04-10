namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackBox.
/// <para>This is a container box for a single track of a presentation. A presentation consists of one or more tracks.
/// Each track carries its own temporal and spatial information. Each track will contain its associated <see cref="MediaDataBox"/> .</para>
/// <para>Tracks are used for a number of purposes, including: (a) to contain media data (media tracks) and (b) to
/// contain packetization information for streaming protocols (hint tracks).</para>
/// <para>There shall be at least one media track within a MovieBox, and all the media tracks that contributed to
/// the hint tracks shall remain in the file, even if the media data within them is not referenced by the hint
/// tracks; after deleting all hint tracks, the entire un-hinted presentation shall remain.</para>
///  </summary>
[HasBoxFactory("trak")]
public class TrackBox : ContainerBox
{
    private TrackBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
}