using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 SampleSizeBox.
/// <para>This box contains the sample count and a table giving the size in bytes of each sample. This allows the
/// media data itself to be unframed. The total number of samples in the media is always indicated in the
/// sample count.</para>
/// <para>There are two variants of the sample size box. The first variant has a fixed size 32-bit field for
/// representing the sample sizes; it permits defining a constant size for all samples in a track. The second
/// variant permits smaller size fields, to save space when the sizes are varying but small. One of these
/// boxes shall be present; the first version is preferred for maximum compatibility.</para>
/// <para>A sample size of zero is not prohibited in general, but it must be valid and defined for the coding
/// system, as defined by the sample entry, that the sample belongs to.</para></summary>
[HasBoxFactory("stsz")]
public class SampleSizeBox  : FullBoxWithData
{
    private SampleSizeBox  (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            SampleSize = value[..4].ToUInt();
            var count = value[4..8].ToUInt();
            EntrySizes.Clear();
            int offset = 8;
            if(SampleSize == 0)
            {
                for (var i = 0U; i < count; i++)
                {
                    EntrySizes.Add(value[offset..(offset + 4)].ToUInt() );
                    offset += 4;
                }
            }

            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(SampleSize);
        builder.Add(SampleCount);
        foreach (var entry in EntrySizes)
        {
            builder.Add(entry);
        }
        return builder;
    }
    /// <summary> Integer specifying the default sample size. If all the samples are the same size, this field
    /// contains that size value. If this field is set to 0, then the samples have different sizes, and those sizes
    /// are stored in the sample size table. If this field is not 0, it specifies the constant sample size, and no
    /// array follows. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint SampleSize { get; set; }

    /// <summary> An integer that gives the number of samples in the track; if sample-size is 0, then it is
    /// also the number of entries in the following table. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint SampleCount => (uint)EntrySizes.Count;
    
    /// <summary> Integers specifying the size of a sample, indexed by its number. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public List<uint> EntrySizes { get;  } = new ();
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + SampleCount * 4;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} C: {SampleCount} ({new string(EntrySizes.Select(e=> e.ToString()).Join(", ").Take(50).ToArray())}...)";

}