using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 MovieHeaderBox.
/// <para>This box defines overall information which is media-independent, and relevant to the entire
/// presentation considered as a whole.</para> </summary>
[HasBoxFactory("mvhd")]
// ReSharper disable once ClassNeverInstantiated.Global
public class MovieHeaderBox : FullBoxWithData
{
	private MovieHeaderBox (BoxHeader header, HandlerBox? handler)
		: base (header,  handler)
	{
		
	}
	/// <summary> Initializes a new instance of <see cref="MovieHeaderBox" /> from a file.  </summary>
	/// <param name="file"> file from which to initialized.</param>
	/// <returns>Initialized box.</returns>
	protected override async Task<Box> InitAsync(MP4 file)
	{
		file.MovieHeaderBox = this;
		await base.InitAsync(file);
		return this;
	}
	/// <inheritdoc />
	public override ByteVector Data {
		get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
		set
		{
			int offset;
			if (Version == 1)
			{
				CreationTime = value[..8].ToDateTime();
				ModificationTime = value[8..16].ToDateTime();
				Timescale = value[16..20].ToUInt();
				Duration = value[20..28].ToTimeSpan(Timescale);
				offset = 28;
			}
			else
			{
				CreationTime = value[..4].ToDateTime();
				ModificationTime = value[4..8].ToDateTime();
				Timescale = value[8..12].ToUInt();
				Duration = value[12..16].ToTimeSpan(Timescale);
				offset = 16;
			}

			Rate = value[offset..(offset+4)];
			Volume = value[(offset + 4)..(offset+6)];
			Matrix = value.Mid(offset + 16, 36);
			NextTrackId = value.Mid(offset + 76, 4).ToUInt();
			Debug.Assert(Data == value);
		} 
	}
	/// <inheritdoc />
	public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
	{
		if (Version == 1)
		{
			builder.AddLong(CreationTime)
				.AddLong(ModificationTime)
				.Add(Timescale)
				.AddLong(Duration, Timescale);
		}
		else
		{
			builder.Add(CreationTime)
				.Add(ModificationTime)
				.Add(Timescale)
				.Add(Duration, Timescale);
		} 
		return builder.Add(Rate)
			.Add(Volume)
			.Clear(10)
			.Add(Matrix)
			.Clear(24)
			.Add(NextTrackId);
	}

	/// <summary> Creation time of the presentation. </summary>
	public DateTime CreationTime { get; set; }
	
	/// <summary> Most recent time the presentation was modified. </summary>
	public DateTime ModificationTime { get; set; }

	private uint _timescale;
	/// <summary>
	///	The timescale for the media. This is the number of time units that pass in one second. For example,
	/// if the timescale is "10000000", one second is equal to 100,000 units. A time coordinate measured in units
	/// is multiplied by the timescale to obtain the number of time units. For example, a time coordinate of 35 would be
	/// 35 * (10000000) = 35 seconds. The timescale is a required field in the header of either an Initial Object
	/// Descriptor or a Media Descriptor.
	/// </summary>
	public uint Timescale
	{
		get => _timescale;
		set
		{
			Duration = Duration * _timescale / value;
			UpdateScaling(Header.File?.Boxes ?? new List<Box>(), _timescale, value );
			_timescale = value;
		}
	}

	private void UpdateScaling(IList<Box> boxes, uint old, uint @new)
	{
		foreach (var box in boxes)
		{
			if (box is ContainerBox boxWithChildren)
			{
				UpdateScaling(boxWithChildren.Children, old, @new);
			}

			if (box is IBoxWithMovieHeaderScalableProperties boxWithScalableProperties)
			{
				boxWithScalableProperties.ChangeTimescale(old, @new);
			}
		}
	}

	/// <summary>The duration of the movie, in movie timescale units. </summary>
	public TimeSpan Duration { get; set; }
	/// <summary>The preferred rate at which to play the movie, expressed as a 16.16 fixed-point number.  </summary>
	public FixedPoint16_16 Rate { get; set; }
	/// <summary> The preferred volume of the movie as a 16.16 fixed-point number. </summary>
	public FixedPoint8_8 Volume { get; set; }
	/// <summary>	The matrix that is used to derive the transformation from the movie's coordinate system to the
	///  coordinate system of the visual presentation. The default value is the identity matrix.  </summary>
	public TransformationMatrix Matrix { get; set; } = new();
		
	/// <summary>The next track ID to use. </summary>
	public uint NextTrackId { get; set; }
	/// <inheritdoc />
	public override ulong ActualDataSize => Version == 1 ? 108UL : 96UL;
	
	/// <inheritdoc />
	public override string DebugDisplay(int level)
		=> $"{base.DebugDisplay(level)}  TS:{Timescale} D:{Duration} R:{Rate.Value} V:{Volume.Value} NextTrackId: {NextTrackId} C:{CreationTime} M:{ModificationTime}";
	
}