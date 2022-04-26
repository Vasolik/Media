using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple iTunes 4.0 CommentAtom.</summary>  
[HasBoxFactory("©cmt") ]
public class CommentAtom  : ContainerBox
{
    private CommentAtom(BoxHeader header, HandlerBox? handler)
        : base(header, handler)
    {
    }

}