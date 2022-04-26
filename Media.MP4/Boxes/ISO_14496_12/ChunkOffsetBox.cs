using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 ChunkOffsetBox.
/// <para>The chunk offset table gives the index of each chunk into the containing file. There are two variants,
/// permitting the use of 32-bit or 64-bit offsets. The latter is useful when managing very large
/// presentations. At most one of these variants will occur in any single instance of a sample table.</para>
/// <para>When the referenced data reference entry is not DataEntryImdaBox or DataEntrySeqNumImdaBox, offsets
/// are file offsets, not the offset into any box within the file (e.g. <see cref="MediaDataBox"/> ). This permits referring
/// to media data in files without any box structure. It does also mean that care must be taken when
/// constructing a self-contained ISO file with its structure-data (<see cref="MovieBox"/>) at the front, as the size of the
/// MovieBox will affect the chunk offsets to the media data.</para>
/// <para>When the referenced data reference entry is DataEntryImdaBox or DataEntrySeqNumImdaBox, offsets
/// are relative to the first byte of the payload of the IdentifiedMediaDataBox corresponding to the data
/// reference entry. This permits reordering file-level boxes and receiving a subset of file-level boxes but
/// could require traversing the file-level boxes until the referenced IdentifiedMediaDataBox is found.</para></summary>
[HasBoxFactory("stco")]
public class ChunkOffsetBox  : FullBoxWithData
{
    private ChunkOffsetBox  (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            var count = value[..4].ToULong();
            ChunkOffsets.Clear();
            int offset = 4;
            for (var i = 0U; i < count; i++)
            {
                ChunkOffsets.Add(value[offset..(offset + 4)].ToUInt() );
                offset += 4;
            }

            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(EntryCount);
        foreach (var entry in ChunkOffsets)
        {
            builder.Add(entry);
        }
        return builder;
    }

    /// <summary> An integer that gives the number of entries in the following table. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint EntryCount => (uint)ChunkOffsets.Count;
    
    /// <summary> Integers specifying the size of a sample, indexed by its number. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public List<uint> ChunkOffsets { get;  } = new ();
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 4UL + (ulong)ChunkOffsets.Count * 4;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} C: {EntryCount} ({new string(ChunkOffsets.Select(e=> e.ToString()).Join(", ").Take(50).ToArray())}...)";
}