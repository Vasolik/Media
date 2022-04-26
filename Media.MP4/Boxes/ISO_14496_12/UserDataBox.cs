namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 UserDataBox.
/// <para>This box contains objects that declare user information about the containing box and its data (presentation or track).</para>
/// <para>The User Data Box is a container box for informative user-data. This user data is formatted as a set of
/// boxes with more specific box types, which declare more precisely their content. The contained boxes
/// are normal boxes, using a defined, registered, or UUID extension box type.</para> </summary>
[HasBoxFactory("udta")]
public class UserDataBox : ContainerBox
{
    private UserDataBox (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    { }
}