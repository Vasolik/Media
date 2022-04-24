using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 SampleToChunkBox.
/// <para>Samples within the media data are grouped into chunks. Chunks can be of different sizes, and the
/// samples within a chunk can have different sizes. This table can be used to find the chunk that contains
/// a sample, its position, and the associated sample description.</para>
/// <para>The table is compactly coded. Each entry gives the index of the first chunk of a run of chunks with the
/// same characteristics. By subtracting one entry here from the previous one, it is possible to compute how
/// many chunks are in this run. This can be converted to a sample count by multiplying by the appropriate
/// samples-per-chunk.</para></summary>
[HasBoxFactory("stsc")]
public class SampleToChunkBox  : FullBoxWithData
{
    private SampleToChunkBox  (BoxHeader header, IsoHandlerBox? handler)
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
                var entry = value[offset..(offset + 12)];
                Entries.Add(new Entry()
                {
                    FirstChunk = entry[..4].ToUInt(),
                    SamplesPerChunk = entry[4..8].ToUInt(),
                    SampleDescriptionIndex = entry[8..12].ToUInt(),
                } );
                offset += 12;
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
            builder.Add(entry.FirstChunk)
                .Add(entry.SamplesPerChunk)
                .Add(entry.SampleDescriptionIndex);
        }

        return builder;
    }

    /// <summary>  Integer that gives the number of entries in the following table </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint EntryCount => (uint)Entries.Count;
    
    /// <summary>TimeToSampleBox entries.</summary>
    public class Entry
    {
        /// <summary>An integer that gives the index of the first chunk in this run of chunks that share the
        /// same samples-per-chunk and sample-description-index; the index of the first chunk in a track has
        /// the value 1 (the first_chunk field in the first record of this box has the value 1, identifying that the
        /// first sample maps to the first chunk). </summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public uint FirstChunk { get; set; }
        
        /// <summary>An integer that gives the number of samples in each of these chunks. .</summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public uint SamplesPerChunk { get; set; }
        
        /// <summary>is an integer that gives the index of the sample entry that describes the samples
        /// in this chunk. The index ranges from 1 to the number of sample entries in the <see cref="SampleDescriptionBox"/> . </summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public uint SampleDescriptionIndex { get; set; }
        
        /// <summary> Debug string use to print during debug. </summary>
        /// <returns>Debug string</returns>
        public  string DebugDisplay()
            => $"F: {FirstChunk}, PC: {SamplesPerChunk}, SDI: {SampleDescriptionIndex}";
    }

    /// <summary> Segment entries in the SampleToChunkBox. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public List<Entry> Entries { get;  } = new ();
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 4UL + EntryCount * 12;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} C: {EntryCount} ({new string(Entries.Select(e=> e.DebugDisplay()).Join("), (").Take(50).ToArray())}...)";

}