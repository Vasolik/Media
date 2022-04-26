namespace Vipl.Media.MP4.Boxes.AppleAtom.SampleEntries;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime ContentTitleAtom.</summary>  
[HasBoxFactory("©nam") ]
public class ContentTitleAtom  : ContainerBox
{
    private ContentTitleAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}