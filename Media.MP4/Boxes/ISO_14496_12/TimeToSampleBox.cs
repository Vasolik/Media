using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 TimeToSampleBox.
/// <para>This box contains a compact version of a table that allows indexing from decoding timestamp to sample
/// number. Other tables give sample sizes and pointers, from the sample number. Each entry in the table
/// gives the number of consecutive samples with the same sample duration, and that sample duration. By
/// adding the sample durations a complete time-to-sample map may be built.</para>
/// <para>The TimeToSampleBox contains sample durations, the differences in decoding timestamps (DT):</para>
/// <c>DT[n+1] = DT[n] + sample_delta[n]</c>
/// <para>The sample entries are ordered by decoding timestamps; therefore all the values of sample_delta shall
/// be non-negative.</para>
/// <para>The DT axis has a zero origin; DT[i] = SUM[for j=0 to i-1 of sample_delta[j]], and in the absence of
/// composition offsets, the sum of all sample durations gives the duration of the media in the track (not
/// mapped to the overall timescale, and not considering any edit list).</para> </summary>
[HasBoxFactory("stts")]
public class TimeToSampleBox  : FullBoxWithData, IBoxWithMovieHeaderScalableProperties
{
    private TimeToSampleBox  (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            
            var count = value[..4].ToUInt();
            Entries.Clear();
            int offset = 4;
            for (var i = 0U; i < count; i++)
            {
                var entry = value[offset..(offset + 8)];
                Entries.Add(new Entry()
                {
                    SampleCount = entry[..4].ToUInt(),
                    SampleDelta = entry[4..8].ToTimeSpan(Header.Timescale)
                } );
                offset += 8;
            }
            
            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(EntryCount);
        foreach (var entry in Entries)
        {
            builder.Add(entry.SampleCount)
                .Add(entry.SampleDelta, Header.Timescale);
        }

        return builder;
    }

    /// <summary>  Integer that gives the number of entries in the following table </summary>
    public uint EntryCount => (uint)Entries.Count;
    
    /// <summary>TimeToSampleBox entries.</summary>
    public class Entry
    {
        /// <summary>An integer that counts the number of consecutive samples that have the given duration. .</summary>
        public uint SampleCount { get; set; }
        
        /// <summary> an integer that gives the difference between the decoding timestamp of the next
        /// sample and this one, in the time-scale of the media.</summary>
        public TimeSpan SampleDelta { get; set; }
        
        /// <summary> Debug string use to print during debug. </summary>
        /// <returns>Debug string</returns>
        public  string DebugDisplay()
            => $"C: {SampleCount}, D: {SampleDelta}";
    }

    /// <summary> Segment entries in the Edit List Box. </summary>
    public List<Entry> Entries { get;  } = new ();
    /// <inheritdoc />
    public void ChangeTimescale(uint oldTimeScale, uint newTimescale)
    {
        foreach (var entry in Entries)
        {
            entry.SampleDelta = entry.SampleDelta * oldTimeScale / newTimescale;
        }
    }
    /// <inheritdoc />
    public override ulong ActualDataSize => 4UL + EntryCount * 8;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} C: {EntryCount} ({new string(Entries.Select(e=> e.DebugDisplay()).Join("), (").Take(50).ToArray())}...)";

}