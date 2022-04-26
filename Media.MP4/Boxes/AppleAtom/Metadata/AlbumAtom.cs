using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime AlbumAtom.</summary>  
[HasBoxFactory("©alb") ]
public class AlbumAtom  : ContainerBox
{
    private AlbumAtom(BoxHeader header, HandlerBox? handler)
        : base(header, handler)
    {
    }

}