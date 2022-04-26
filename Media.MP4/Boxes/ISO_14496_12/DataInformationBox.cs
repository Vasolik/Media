namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 DataInformationBox.
/// <para>The <see cref="DataInformationBox"/> contains objects that declare the location of the media information in a track.</para> </summary>
[HasBoxFactory("dinf")]
public class DataInformationBox :  ContainerBox
{
    private DataInformationBox (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
}