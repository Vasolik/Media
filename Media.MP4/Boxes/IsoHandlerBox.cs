using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes;

/// <summary>
/// <para>This class extends <see cref="FullBox" /> to provide an
/// implementation of a ISO/IEC 14496-12 FullBox.</para>
/// 
/// <para>This box within a <see cref="MediaBox"/> declares media type of the track, and thus the process by which the media-
/// data in the track is presented. For example, a format for which the decoder delivers video would be
/// stored in a video track, identified by being handled by a video handler. The documentation of the
/// storage of a media format identifies the media type which that format uses.</para>
/// 
/// <para> This box when present within a MetaBox, declares the structure or format of the MetaBox contents.</para>
///
/// <para> There is a general handler for metadata streams of any type; the specific format is identified by the
/// sample entry, as for video or audio, for example.</para>
/// </summary>
[HasBoxFactory("hdlr")]
public class IsoHandlerBox : FullBoxWithData
{
	/// <summary>  Constructs and initializes a new instance of <see  cref="IsoHandlerBox" />
	/// with a provided header and handler by reading the contents from a specified file. </summary>
	/// <param name="header"> A <see cref="BoxHeader" /> object containing the header to use for the new instance. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies to the new instance. </param>
	private IsoHandlerBox (BoxHeader header, IsoHandlerBox? handler)
		: base (header, handler)
	{
		HandlerType = null!;
		Name = null!;
	}

	/// <summary>
	///    Gets the data contained in the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="ByteVector" /> object containing the
	///    rendered version of the data contained in the current
	///    instance.
	/// </value>
	public override ByteVector Data
	{
		get => RenderData(new ByteVectorBuilder((int) ActualSize)).Build();
		set
		{
			HandlerType = value[4..8].ToByteVector();
			Name = value[20..].ToString(Encoding.UTF8);
		}
	}
	/// <inheritdoc/>
	public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
	{
		return builder.Skip(4)
			.Add(HandlerType)
			.Clear(12)
			.Add(Name);
	}

	/// <summary><para> When present in a <see cref="MediaDataBox"/>, contains a value as defined in Clause 12, or a value from
	/// a derived specification, or registration. </para>
	/// <para>When present in a <see cref="MetaBox"/>, contains an appropriate value to indicate the format of the
	/// <see cref="MetaBox"/> contents. The value 'null' can be used in the primary MetaBox to indicate that it is
	/// merely being used to hold resources.</para>
	/// </summary>
	public ByteVector HandlerType { get; private set; }

	/// <summary> Human-readable name for the track type (for debugging and inspection purposes). </summary>
	public string Name { get; private set; }

	/// <inheritdoc/>
	public override ulong ActualDataSize => (ulong)(20 + Encoding.UTF8.GetByteCount(Name));
	
	/// <inheritdoc />
	public override string DebugDisplay(int level)
		=> $"{base.DebugDisplay(level)} HandlerType: {HandlerType} Name: {Name}";
}