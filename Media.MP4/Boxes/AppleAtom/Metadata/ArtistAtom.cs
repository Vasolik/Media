using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime ArtistAtom.</summary>  
[HasBoxFactory("©ART") ]
public class ArtistAtom  : ContainerBox
{
    private ArtistAtom(BoxHeader header, HandlerBox? handler)
        : base(header, handler)
    {
    }

}