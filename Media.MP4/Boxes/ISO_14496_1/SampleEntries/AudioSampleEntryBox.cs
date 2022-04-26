using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.ISO_14496_1.SampleEntries;

/// <summary>  This class extends <see cref="SampleEntryBox" /> to provide an implementation of a ISO/IEC 14496-14 MP4AudioSampleEntry.
/// <para>Sample entry for audio streams.</para></summary>
[HasBoxFactory("mp4a",  typeof(SampleDescriptionBox))]
public class MP4AudioSampleEntryBox : SampleEntryBox
{
    private MP4AudioSampleEntryBox (BoxHeader header,  HandlerBox? handler)
        : base (header, handler)
    {
    }

    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            TimeScale = value[24..26].ToUShort();
            base.Data = value;
        } 
    }
    
    /// <summary> Time scale copied from the track. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort TimeScale { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder)
            .Clear(8)
            .Add((ushort)2)
            .Add((ushort)16)
            .Clear(4)
            .Add(TimeScale)
            .Clear(2);
    }
    
    /// <inheritdoc />
    public override ulong DataSize => 28;
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Box.DataSize"/>, this value is calculated after every change of data. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public override ulong ActualDataSize => 28;
    
    /// <inheritdoc />
    public override ulong ChildrenPosition => DataPosition + 28;

    /// <inheritdoc />
    public override ulong  ChildrenSize => base.DataSize - 28;
    
    /// <inheritdoc />
    protected override string DebugDisplayMoreData() =>  $"TS: {TimeScale}";
    
}