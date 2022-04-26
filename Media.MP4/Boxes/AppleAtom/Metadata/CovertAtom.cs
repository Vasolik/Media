namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime CovertAtom.</summary>  
[HasBoxFactory("covr") ]
public class CovertAtom  : ContainerBox
{
    private CovertAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}