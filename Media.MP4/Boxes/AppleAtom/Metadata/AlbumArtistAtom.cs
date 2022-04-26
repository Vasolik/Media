namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime AlbumArtistAtom.</summary>  
[HasBoxFactory("aART") ]
public class AlbumArtistAtom  : ContainerBox
{
    private AlbumArtistAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}