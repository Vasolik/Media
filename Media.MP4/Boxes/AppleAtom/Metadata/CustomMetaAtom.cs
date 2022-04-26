namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a CustomMetaAtom.</summary>  
[HasBoxFactory("----") ]
public class CustomMetaAtom  : ContainerBox
{
    private CustomMetaAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}