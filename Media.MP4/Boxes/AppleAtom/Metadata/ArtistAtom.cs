namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime ArtistAtom.</summary>  
[HasBoxFactory("©ART") ]
public class ArtistAtom  : ContainerBox
{
    private ArtistAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}