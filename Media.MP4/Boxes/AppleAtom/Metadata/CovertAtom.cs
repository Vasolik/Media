using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime CovertAtom.</summary>  
[HasBoxFactory("covr") ]
public class CovertAtom  : ContainerBox
{
    private CovertAtom(BoxHeader header, HandlerBox? handler)
        : base(header, handler)
    {
    }

}