using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>   This class extends <see cref="Box" /> to provide an
///    implementation of a ISO/IEC 14496-12 SampleEntryBox.
/// <para>The sample description table gives detailed information about the coding type used, and any
/// initialization information needed for that coding. The syntax of the sample entry used is determined by
/// both the format field and the media handler type.</para>
/// <para>The information stored in the <see cref="SampleDescriptionBox"/>  after the entry-count is both track-type specific
/// as documented here, and can also have variants within a track type (e.g. different codings may use
/// different specific information after some common fields, even within a video track).</para>
/// <para>Which type of sample entry form is used is determined by the media handler, using a suitable form,
/// such as one defined in Clause 12, or defined in a derived specification, or registration.</para>
/// <para>Multiple descriptions may be used within a track.</para>
/// <para>Derived specifications deriving Sample Entry classes listed in the table of 8.12.1 should be extremely
/// careful. Derivation by adding boxes at the end of the class should be preferred as it preserves Sample
/// Entry parsing and does not require a new 'encX' value. Adding a new field to a class will not allow
/// for the use of the associated 'encX' scheme for parsing reasons. A new 'encX' scheme will have to be
/// defined for signaling encrypted stream based on that derived class.</para>
/// <para>The definition of sample entries specifies boxes in a particular order, and this is usually also followed in
/// derived specifications. For maximum compatibility, writers should construct files respecting the order
/// both within specifications and as implied by the inheritance, whereas readers should be prepared to
/// accept any box order.</para>
/// <para>All  <see cref="SampleEntryBox"/>es may contain “extra boxes” not explicitly defined in the box syntax of this or
/// derived specifications. When present, such boxes shall follow all defined fields and should follow any
/// defined contained boxes. Decoders shall presume a sample entry box could contain extra boxes and
/// shall continue parsing as though they are present until the containing box length is exhausted.</para>
/// <para>An optional <see cref="BitRateBox"/> may be present in any <see cref="SampleEntryBox"/> to signal the bit rate information of a
/// stream. This can be used for buffer configuration.</para></summary>
public abstract class SampleEntryBox : BoxWithData, IContainerBox
{
	/// <summary> Constructs and initializes a new instance of <see cref="SampleEntryBox" />
	/// with a specified header and handler.</summary>
	/// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies
	/// to the new instance, or <see langword="null" /> if no handler applies.</param>
	protected SampleEntryBox(BoxHeader header, IsoHandlerBox? handler)
		: base(header, handler)
	{
	}

	/// <inheritdoc />
	protected sealed override async Task<Box> InitAsync(MP4 file)
	{
		await base.InitAsync(file);
		await this.LoadChildrenAsync(file);
		Debug.Assert(Size == ActualSize);
		return this;
	}

	/// <inheritdoc />
	public override ByteVector Data
	{
		get => RenderData(new ByteVectorBuilder((int) ActualDataSize)).Build();
		set
		{
			DataReferenceIndex = value[6..8].ToUShort();
			Debug.Assert(Data == value);
		}
	}

	/// <inheritdoc/>
	public override IByteVectorBuilder Render(IByteVectorBuilder builder)
	{
		base.Render(builder);
		RenderData(builder);
		Children.ForEach(c => c.Render(builder));
		return builder;
	}

	/// <inheritdoc />
	public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
	{
		return builder.Skip(6)
			.Add(DataReferenceIndex);
	}

	/// <inheritdoc />
	public IList<Box> Children { get; } = new List<Box>();

	/// <inheritdoc />
	ulong IContainerBox.ChildrenPosition => DataPosition + 8;

	/// <inheritdoc />
	ulong IContainerBox.ChildrenSize => 0;

	/// <summary> An integer that contains the index of the DataEntry to use to retrieve
	/// data associated with samples that use this sample description. Data entries are stored in
	/// DataReferenceBoxes. The index ranges from 1 to the number of data entries. </summary>
	public ushort DataReferenceIndex { get; set; }

	/// <inheritdoc />
	public override ulong ActualSize => Header.HeaderSize 
        + ActualDataSize
        + Children.Aggregate(0UL, (sum, box) => sum + box.ActualSize);
}

