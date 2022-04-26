using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple Apple iTunes 4.0 ComposerAtom.</summary>  
[HasBoxFactory("©wrt") ]
public class ComposerAtom  : ContainerBox
{
    private ComposerAtom(BoxHeader header, HandlerBox? handler)
        : base(header, handler)
    {
    }

}