namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime CreatedDayAtom.</summary>  
[HasBoxFactory("©day") ]
public class CreatedDayAtom  : ContainerBox
{
    private CreatedDayAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}