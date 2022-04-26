namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple iTunes 4.0 CommentAtom.</summary>  
[HasBoxFactory("©cmt") ]
public class CommentAtom  : ContainerBox
{
    private CommentAtom(BoxHeader header, IsoHandlerBox? handler)
        : base(header, handler)
    {
    }

}