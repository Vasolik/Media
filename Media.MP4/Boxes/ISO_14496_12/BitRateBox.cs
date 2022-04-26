using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 BitRateBox.
///<para>An optional <see cref="BitRateBox"/> may be present in any <see cref="SampleEntryBox"/> to signal
/// the bit rate information of a stream. This can be used for buffer configuration.</para></summary>
[HasBoxFactory("btrt")]
public class BitRateBox : BoxWithData
{
    private BitRateBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }

    /// <summary>  Size of the decoding buffer for the elementary stream in bytes. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint BufferSizeDecodingBuffer { get; set; }
    
    /// <summary> Gives the maximum rate in bits/second over any window of one second; this is a measured
    /// value for stored content, or a value that a stream is configured not to exceed; the stream shall not
    /// exceed this bitrate. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint MaxBitRate { get; set; }
    
    /// <summary> Gives the average rate in bits/second over any window of one second; this is a measured value
    /// for stored content, or a value that a stream is configured not to exceed; the stream shall not exceed
    ///  this bitrate. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint AvgBitRate { get; set; }
    
    /// <inheritdoc />
    public override ByteVector Data
    {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            BufferSizeDecodingBuffer = value[..4].ToUInt();
            MaxBitRate = value[4..8].ToUInt();
            AvgBitRate = value[8..12].ToUInt();
            Debug.Assert(Data == value);
        }
    }
    
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(BufferSizeDecodingBuffer);
        builder.Add(MaxBitRate);
        builder.Add(AvgBitRate);
        return builder;
    }
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 12;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} BS: {BufferSizeDecodingBuffer} MBR: {MaxBitRate} ABR: {AvgBitRate}";
    
}