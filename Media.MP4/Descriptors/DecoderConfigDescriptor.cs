using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Descriptors;

/// <summary> This class extends <see cref="BaseDescriptor" /> to provide an implementation of a ISO/IEC 14496-1 DecoderConfigDescriptor.
/// <para>The DecoderConfigDescriptor provides information about the decoder type and the required decoder
/// resources needed for the associated elementary stream. This is needed at the receiving terminal to determine
/// whether it is able to decode the elementary stream. A stream type identifies the category of the stream while
/// the optional decoder specific information descriptor contains stream specific information for the set up of the
/// decoder in a stream specific format that is opaque to this layer.</para> </summary>
public class DecoderConfigDescriptor : BaseDescriptor
{
    /// <summary>Mask for retrieving StreamType.</summary>>
    private const int StreamTypeMask = 0x3F;
    /// <summary>Mask for retrieving IsUpStream Flag</summary>>
    private const int IsUpStreamMask = 0x40;
    
    /// <summary> Creates a new instance of the <see cref="ProfileLevelIndicationIndexDescriptor"/> class. </summary>
    /// <param name="header">Header of descriptor</param>
    /// <param name="data">Data in descriptor</param>
    public DecoderConfigDescriptor(DescriptorHeader header, Span<byte> data) : base(header)
    {
        Data = data;
    }

    /// <summary> an indication of the object or scene description type that needs to be supported
    /// by the decoder for this elementary stream </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ObjectTypeIndicationType ObjectTypeIndication { get; set; }
    
    /// <summary>Conveys the type of this elementary stream </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public StreamType StreamType  { get; set; }

    /// <summary> Indicates that this stream is used for upstream information. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsUpStream { get; set; }
    
    /// <summary> The size of the decoding buffer for this elementary stream in byte. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint BufferSize { get; set; }
    /// <summary> The maximum bitrate of the elementary stream in bits per second. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint MaximumBitrate { get; set; }
    /// <summary> average bitrate in bits per second of this elementary stream. For streams with variable
    /// bitrate this value shall be set to zero.. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint AverageBitrate { get; set; }
    /// <summary> Array of zero or one decoder specific information classes </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public DecoderSpecificInfoDescriptor? DecoderSpecificInfo { get; set; }
    
    /// <summary> an array of unique identifiers for a set of profile and
    /// level indications as carried in the  ExtensionProfileLevelDescriptor. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public List<BaseDescriptor> ProfileLevelIndication { get; set; } = new ();
    
    /// <summary>Descriptor as <see cref="ByteVector"/>. </summary>
    public sealed override Span<byte> Data
    {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ObjectTypeIndication = (ObjectTypeIndicationType)value[0];
            StreamType = (StreamType)(value[1] & StreamTypeMask);
            IsUpStream = (value[1] & IsUpStreamMask) != 0;
            BufferSize = value[2..5].ToUInt();
            MaximumBitrate = value[5..9].ToUInt();
            AverageBitrate = value[9..13].ToUInt();
            var remainingSize = Header.Length - 13;
            var offset = 13;
            ProfileLevelIndication = new List<BaseDescriptor>();
            while (remainingSize > 0)
            {
                var descriptor = DescriptorFactory.Create(value[offset..]);
                remainingSize -= descriptor.TotalSize;
                offset += descriptor.TotalSize;
                if (descriptor is ProfileLevelIndicationIndexDescriptor indexDescriptor)
                {
                    ProfileLevelIndication.Add(indexDescriptor);
                }
                else if (descriptor is DecoderSpecificInfoDescriptor decoderSpecificInfoDescriptor)
                {
                    DecoderSpecificInfo = decoderSpecificInfoDescriptor;
                }
                else
                {
                    ProfileLevelIndication.Add(descriptor);
                }
              
            }

            Debug.Assert(Data.ToByteVector() == value.ToByteVector());
        }
    }
        
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add((byte)ObjectTypeIndication)
            .Add((byte)((byte)StreamType | (IsUpStream ? IsUpStreamMask : 0)))
            .Add(BufferSize.ToByteVector()[1..])
            .Add(MaximumBitrate)
            .Add(AverageBitrate);
        if(DecoderSpecificInfo != null)
        {
            DecoderSpecificInfo.Render(builder);
        }  
        foreach (var profileLevelIndication in ProfileLevelIndication)
        {
            profileLevelIndication.Render(builder);
        }

        return builder;
    }
        
    /// <summary> Actual size of description. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public override ulong ActualDataSize => 13
        + (DecoderSpecificInfo?.ActualSize ?? 0)
        + (ulong)ProfileLevelIndication.Sum(x => (long)x.ActualSize);


    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} OT: {ObjectTypeIndication.GetDescription()} ST: {StreamType.GetDescription()} US: {IsUpStream} BS: {BufferSize.BytesToString()} MB: {MaximumBitrate.BytesToString()} AB: {AverageBitrate.BytesToString()}"
           + (DecoderSpecificInfo is not null ? "\n" : "") + (DecoderSpecificInfo?.DebugDisplay(level + 1) ?? "")
           + (ProfileLevelIndication.Any() ? "\n" : "") + ProfileLevelIndication.Select(x => x.DebugDisplay(level + 1)).Join();
}