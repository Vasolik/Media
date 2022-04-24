using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12.DataEntries;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 DataEntryUrlBox.</summary>  
[HasBoxFactory("url"),HasBoxFactory("url ") ]
public class DataEntryUrlBox : BoxWithData
{
	private DataEntryUrlBox(BoxHeader header, IsoHandlerBox? handler)
		: base(header, handler)
	{
	}
	
	/// <inheritdoc />
	public override ByteVector Data 
	{ 
		get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
		set
		{
			Location = value[..].ToString(Encoding.UTF8);
			Debug.Assert(Data == value);
		} 
	}

	/// <summary> A URL, and is required in a URL entry and optional in a URN entry, where it gives a location
	/// to find the resource with the given name. The URL type should be of a service that delivers a file
	/// (e.g. URLs of type file, http, ftp etc.), and which services ideally also permit random access. Relative
	/// URLs are permissible and are relative to the file that contains this data reference. </summary>
	// ReSharper disable once MemberCanBePrivate.Global
	public string Location { get; set; } = "";

	/// <inheritdoc />
	public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
	{
		builder.Add(Location);
		return builder;
	}

	/// <inheritdoc />
	public override ulong ActualDataSize => (ulong) Encoding.UTF8.GetByteCount(Location);
	
	/// <inheritdoc />
	public override string DebugDisplay(int level)
		=> $"{base.DebugDisplay(level)} - {Location}";

}