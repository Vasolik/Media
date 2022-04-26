namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple Apple iTunes 4.0 GenreAtom.</summary>  
[HasBoxFactory("©gen") ]
public class GenreAtom  : ContainerBox
{
    private GenreAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}