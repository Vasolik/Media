using Vipl.Base;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12.SampleEntries;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackReferenceTypeBox.
/// <para>This is unknown track reference type box</para></summary>
[HasBoxFactory(null,  typeof(SampleDescriptionBox))]
public class UnknownSampleEntryBox : SampleEntryBox
{
    private UnknownSampleEntryBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
        UnknownData = null!;
    }

    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            UnknownData = value[8..];
            base.Data = value;
        } 
    }
    
    /// <summary> Unrecognized data in the box </summary>
    public ByteVector UnknownData { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder)
            .Add(UnknownData);
    }
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Box.DataSize"/>, this value is calculated after every change of data. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public override ulong ActualDataSize => 8UL + (ulong)UnknownData.Count;
}