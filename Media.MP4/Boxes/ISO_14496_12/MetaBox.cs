namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 MetaBox.
/// <para>A common base structure is used to contain general untimed metadata. This structure is called the
/// <see cref="MetaBox"/> as it was originally designed to carry metadata, i.e. data that is annotating other data. However,
/// it is now used for a variety of purposes including the carriage of data that is not annotating other data,
/// especially when present at ‘file level’.</para>
/// <para>The MetaBox is required to contain a HandlerBox indicating the structure or format of the MetaBox contents.</para> </summary>
[HasBoxFactory("meta")]
public class MetaBox : FullContainerBox
{
	private MetaBox (BoxHeader header, IsoHandlerBox? handler)
		: base (header, handler)
	{ }

}