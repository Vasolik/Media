using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 EditListBox.
/// <para>The length of the whole track in an EditListBox might be the overall duration of the
/// whole movie excluding fragments of a fragment movie. Since edit lists cannot occur in movie fragments, there is
/// an implied edit at the end of the current explicit or implied edit list, that inserts the new media material and the
/// presentation of fragments starts after the presentation of the movie in the MovieBox.</para>
/// </summary>
[HasBoxFactory("elst")]
public class EditListBox  : FullBoxWithData, IBoxWithMovieHeaderScalableProperties
{
    private EditListBox  (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            int offset = 0;
            var count = value[..4].ToUInt();
            Entries.Clear();
            for (var i = 0U; i < count; i++)
            {
                if (Version == 1)
                {
                    var entry = value.Mid(offset, offset + 24);
                    Entries.Add(new Entry()
                    {
                        SegmentDuration = entry[..8].ToTimeSpan(Header.Timescale),
                        MediaTime = entry[8..16].ToTimeSpan(Header.Timescale),
                        MediaRate = entry[16..24]
                    } );
                    offset = 24;
                }
                else
                {
                    var entry = value.Mid(offset, offset + 12);
                    Entries.Add(new Entry()
                    {
                        SegmentDuration = entry[..4].ToTimeSpan(Header.Timescale),
                        MediaTime = entry[4..8].ToTimeSpan(Header.Timescale),
                        MediaRate = entry[8..12]
                    } );
                    offset = 12;
                }
            }
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(EntryCount);
        foreach (var entry in Entries)
        {
            if (Version == 1)
            {
                builder.AddLong(entry.SegmentDuration, Header.Timescale)
                    .AddLong(entry.MediaTime, Header.Timescale)
                    .Add(entry.MediaRate);
            }
            else
            {
                builder.Add(entry.SegmentDuration, Header.Timescale)
                    .Add(entry.MediaTime, Header.Timescale)
                    .Add(entry.MediaRate);
            } 
        }

        return builder;
    }

    /// <summary>  Integer that gives the number of entries in the following table </summary>
    public uint EntryCount => (uint)Entries.Count;

    /// <summary>Media segment entries.</summary>
    public class Entry
    {
        /// <summary>Duration of this edit in units of the timescale in the <see cref="MovieHeaderBox"/> .</summary>
        public TimeSpan SegmentDuration { get; set; }
        
        /// <summary> Starting time within the media of this edit entry (in media time
        /// scale units, in composition time). If this field is set to –1 (scaled), it is an empty edit.
        /// The last edit in a track shall never be an empty edit. Any difference between the duration in the <see cref="MovieHeaderBox"/>,
        /// and the track’s duration is expressed as an implicit empty edit at the end. </summary>
        public TimeSpan MediaTime { get; set; }
        
        /// <summary> The relative rate at which to play the media corresponding to this edit entry.
        /// If this value is 0, then the edit is specifying a ‘dwell’: the media at media-time is presented
        /// for the edit_duration </summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public FixedPoint16_16 MediaRate { get; set; }
        
        /// <summary> Debug string use to print during debug. </summary>
        /// <returns>Debug string</returns>
        public  string DebugDisplay()
            => $"D: {SegmentDuration}, T: {MediaTime}, R: {MediaRate.Value}";
    }

    /// <summary> Segment entries in the Edit List Box. </summary>
    public IList<Entry> Entries { get;  } = new List<Entry>();
    /// <inheritdoc />
    public void ChangeTimescale(uint oldTimeScale, uint newTimescale)
    {
        foreach (var entry in Entries)
        {
            entry.MediaTime = entry.MediaTime * oldTimeScale / newTimescale;
            entry.SegmentDuration = entry.SegmentDuration * oldTimeScale / newTimescale;
        }
    }
    /// <inheritdoc />
    public override ulong ActualDataSize => 4UL + (ulong) (EntryCount * (Version == 1 ? 20 : 12));
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} ({Entries.Select(e=> e.DebugDisplay()).Join("), (")})";

}