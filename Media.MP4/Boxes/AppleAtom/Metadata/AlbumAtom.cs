namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime AlbumAtom.</summary>  
[HasBoxFactory("©alb") ]
public class AlbumAtom  : ContainerBox
{
    private AlbumAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}