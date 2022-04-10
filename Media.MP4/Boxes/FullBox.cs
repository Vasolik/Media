using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes;

/// <summary> This class extends <see cref="Box" /> to provide an
/// implementation of a ISO/IEC 14496-12 FullBox. </summary>
public abstract class FullBox : Box
{
	/// <summary> Constructs and initializes a new instance of <see cref="FullBox"/>
	/// with a provided header and handler by reading the contents from a specified file. </summary>
	/// <param name="header"> A <see cref="BoxHeader" /> object containing the header to
	/// use for the new instance. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler
	/// that applies to the new instance. </param>
	protected FullBox (BoxHeader header,  IsoHandlerBox? handler)
		: base (header, handler) { }

	/// <inheritdoc />
	protected override async Task<Box> InitAsync(MP4 file)
	{
		file.Seek ((long)base.DataPosition, SeekOrigin.Begin);
		var headerData = await file.ReadBlockAsync (4);
		Version = headerData[0];
		Flags = headerData[1..4].ToUInt();
		file.Seek((long)DataPosition, SeekOrigin.Begin);

		return this;
	}

	/// <summary> Constructs and initializes a new instance of <see cref="FullBox" />
	/// with a provided header, version, and  flags.</summary>
	/// <param name="header">A <see cref="BoxHeader" /> object containing the header to use for the new instance. </param>
	/// <param name="version"> A <see cref="byte" /> value containing the version of the new instance. </param>
	/// <param name="flags"> A <see cref="byte" /> value containing the flags for the new instance. </param>
	protected FullBox (BoxHeader header, byte version, uint flags)
		: base (header)
	{
		Version = version;
		Flags = flags;
	}

	/// <summary> Constructs and initializes a new instance of <see cref="FullBox" />
	/// with a provided header, version, and  flags. </summary>
	/// <param name="type"> A <see cref="ByteVector" /> object containing the four byte box type. </param>
	/// <param name="version"> A <see cref="byte" /> value containing the version of the new instance. </param>
	/// <param name="flags"> A <see cref="byte" /> value containing the flags for the new instance. </param>
	protected FullBox (BoxType type, byte version, uint flags)
		: this (new BoxHeader(type), version, flags)
	{
	}
	
	/// <summary> Version is an integer that specifies the version of this format of the box. </summary>
	protected byte Version { get; set; }

	/// <summary> Gets and sets the flags that apply to the current instance. </summary>
	public uint Flags { get; set; }

	/// <inheritdoc />
	public override ulong DataPosition => base.DataPosition + 4;
	/// <inheritdoc />
	public override IByteVectorBuilder Render (IByteVectorBuilder builder)
	{
		return base.Render(builder)
			.Add(Version)
			.Add(Flags.ToByteVector().Mid (1, 3))
			.Skip(4);
	}
	/// <inheritdoc />
	public override ulong DataSize => base.DataSize - 4;
}