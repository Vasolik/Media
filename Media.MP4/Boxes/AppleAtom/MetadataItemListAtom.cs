namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime UserDataBox.
/// <para>he metadata item list atom holds a list of actual metadata values that are present in the metadata atom.
/// The metadata items are formatted as a list of items. The metadata item list atom is of type ‘ilst’ and contains
/// a number of metadata items, each of which is an atom.</para> </summary>
[HasBoxFactory("ilst")]
public class MetadataItemListAtom : ContainerBox
{
    private MetadataItemListAtom (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    { }
}