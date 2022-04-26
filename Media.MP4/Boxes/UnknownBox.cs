using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes;

/// <summary>  This class extends <see cref="Box" /> to provide a simple
///    implementation of a box of unknown type.</summary>
public class UnknownBox : BoxWithData
{

	/// <summary> Constructs and initializes a new instance of <see cref="UnknownBox" />
	/// with a provided header and handler by reading the contents from a specified file. </summary>
	/// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
	/// <param name="handler"> A <see cref="HandlerBox" /> object containing the handler that applies
	/// to the new instance, or <see langword="null" /> if no handler applies.</param>
	private UnknownBox (BoxHeader header, HandlerBox? handler) 
		: base (header, handler)
	{
		Data = null!;
	}

	/// <inheritdoc />
	public override ulong ActualDataSize => (ulong)Data.Count;
}