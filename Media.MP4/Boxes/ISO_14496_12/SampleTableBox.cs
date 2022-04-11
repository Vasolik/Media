namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 SampleTableBox.
/// <para>The sample table contains all the time and data indexing of the media samples in a track. Using the tables
/// here, it is possible to locate samples in time, determine their type (e.g. I-frame or not), and determine
/// their size, container, and offset into that container.</para>
/// <para>If the track that contains the <see cref="SampleTableBox"/>  references no data, then the <see cref="SampleTableBox"/>  does not
/// need to contain any sub-boxes (this is not a very useful media track).</para> </summary>
[HasBoxFactory("stbl")]
public class SampleTableBox : ContainerBox
{
    private SampleTableBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    { }

}