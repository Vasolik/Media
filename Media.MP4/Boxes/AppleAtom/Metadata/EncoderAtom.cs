namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime EncoderAtom.</summary>  
[HasBoxFactory("©too") ]
public class EncoderAtom  : ContainerBox
{
    private EncoderAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}