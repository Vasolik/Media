using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 FreeSpaceBox.
/// <para>The contents of a <see cref="FreeSpaceBox"/>  are irrelevant and may be ignored, or the box deleted, without affecting
/// the presentation. (Care should be exercised when deleting the box, as this may invalidate offsets used
/// to refer to other data, unless this box is after all the media data).</para> </summary>
[HasBoxFactory("free"), HasBoxFactory("skip")]
public class FreeSpaceBox : BoxWithData
{
	// ReSharper disable once UnusedMember.Local
	private FreeSpaceBox (BoxHeader header,  IsoHandlerBox? handler)
		: base (header, handler)
	{
	}
	
	/// <inheritdoc />
	protected override Task<Box> InitAsync(MP4 file)
	{
		PaddingSize = DataSize;
		return Task.FromResult((Box)this);
	}

	/// <summary> Constructs and initializes a new instance of <see cref="FreeSpaceBox" />
	/// to occupy a specified number of bytes. </summary>
	/// <param name="padding"> A <see cref="long" /> value specifying the number
	/// of bytes the new instance should occupy when rendered. </param>
	public FreeSpaceBox (ulong padding) : base (BoxType.Free )
	{
		PaddingSize = padding;
	}
	
	/// <summary> Gets and sets the data contained in the current instance.
	/// </summary>
	/// <value>
	///    A <see cref="ByteVector" /> object containing the data
	///    contained in the current instance.
	/// </value>
	public override ByteVector Data {
		get => new ByteVectorBuilder((int)PaddingSize).Build();
		set => PaddingSize = (uint)value.Count;
	}
	/// <inheritdoc />
	public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
		=> builder.Skip((uint)PaddingSize);

	/// <summary> Gets and sets the size the current instance will occupy when rendered. </summary>
	/// <value> A <see cref="ulong" /> value containing the size the current instance will occupy when rendered. </value>
	public ulong PaddingSize { get; set; }

	/// <inheritdoc />
	public override ulong ActualDataSize => PaddingSize;
	
	/// <inheritdoc />
	public override string DebugDisplay(int level)
		=> $"{base.DebugDisplay(level)} : {PaddingSize.BytesToString()}";

}