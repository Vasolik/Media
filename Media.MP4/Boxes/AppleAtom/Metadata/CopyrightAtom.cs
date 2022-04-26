namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime CopyrightAtom.</summary>  
[HasBoxFactory("cprt") ]
public class CopyrightAtom  : ContainerBox
{
    private CopyrightAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}