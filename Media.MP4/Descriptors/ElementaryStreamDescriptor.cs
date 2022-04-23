using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Descriptors;

/// <summary>This class is implementation of a ISO/IEC 14496-1 ES_Descriptor.
///<para>The <see cref="ElementaryStreamDescriptor"/> conveys all information related to a particular
/// elementary stream and has three major parts.</para>
/// <para>The first part consists of the ES_ID which is a unique reference to the elementary stream within its name
/// scope, a mechanism to describe dependencies of elementary streams within the scope of the parent object descriptor and an optional URL string.</para>
/// <para>The second part consists of the component descriptors which convey the parameters and requirements of the elementary stream.</para>
/// <para>The third part is a set of optional extension descriptors that support the inclusion of future extensions as well
/// as the transport of private data in a backward compatible way.</para> </summary>
public class ElementaryStreamDescriptor : BaseDescriptor
{
    /// <summary>Mask for retrieving DependsOnElementaryStreamID Flag</summary>>
    private const int DependsOnElementaryStreamIDFlagMask = 0x80;
        
    /// <summary>Mask for retrieving Url Flag</summary>>
    private const int UrlFlagMask = 0x40;
        
    /// <summary>Mask for retrieving object clock reference Elementary Stream ID Flag</summary>>
    private const int ObjectClockReferenceElementaryStreamIDFlagMask  = 0x20;

    /// <summary> Constructor for the elementary stream descriptor. </summary>
    /// <param name="header">Header of descriptor</param>
    /// <param name="data">Data present inside of this descriptor including header data.</param>
    public ElementaryStreamDescriptor (DescriptorHeader header, Span<byte> data) : base(header)
    {
        Data = data;
    }
        
    /// <summary> This syntax element provides a unique label for each elementary stream
    /// within its name scope. The values 0 and 0xFFFF are reserved. </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort ElementaryStreamID { get; set; }
        
    /// <summary> contains a UTF-8 (ISO/IEC 10646-1) encoded URL that shall point to the location of an SL-packetized
    /// stream by name. The parameters of the SL-packetized stream that is retrieved from the URL are
    /// fully specified in this <see cref="ElementaryStreamDescriptor"/>. Permissible URLs may be constrained by profile
    /// and levels as well as by specific delivery layers. </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public string? Url { get; set; }
        
    private byte _streamPriority;

    /// <summary>Indicates a relative measure for the priority of this elementary stream. An elementary stream with a
    /// higher <see cref="StreamPriority"/> is more important than one with a lower streamPriority. The absolute values of
    /// <see cref="StreamPriority"/> are not normatively defined. </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public byte StreamPriority
    {
        get => _streamPriority;
        set => _streamPriority = (byte) (value & 0x1F);
    }

    /// <summary> Is the <see cref="ElementaryStreamID"/> of another elementary stream on which this elementary stream depends.
    /// The stream with dependsOn_ES_ID shall also be associated to the same object descriptor as the current <see cref="ElementaryStreamDescriptor"/>. </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort? DependsOnElementaryStreamID { get; set; }

    /// <summary> indicates the <see cref="ElementaryStreamID"/> of the elementary stream within the name scope from which the time base for this
    /// elementary stream is derived. Circular references between elementary streams are not permitted.</summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort? ObjectClockReferenceElementaryStreamID { get; set; }

    /// <summary> DecoderConfigDescriptor </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public DecoderConfigDescriptor DecoderConfigDescriptor { get; set; } = null!;

    /// <summary> Unknown data. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ByteVector UnknownData { get; set; } = null!;

    /// <summary>Descriptor as <see cref="ByteVector"/>. </summary>
    public sealed override Span<byte> Data
    {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ElementaryStreamID = value[..2].ToUShort();
            var flags = value[2];
            StreamPriority = (byte) (flags & 0x1F);
            var offset = 3;
            if ((flags & DependsOnElementaryStreamIDFlagMask) != 0)
            {
                DependsOnElementaryStreamID = value[3..4].ToUShort();
                offset = 5;
            }

            if ((flags & UrlFlagMask) != 0)
            {
                var lenght = value[offset];
                offset += 1;
                Url = value[offset..(offset + lenght)].ToString(Encoding.UTF8);
                offset += lenght;
            }

            if ((flags & ObjectClockReferenceElementaryStreamIDFlagMask) != 0)
            {
                ObjectClockReferenceElementaryStreamID = value[offset..(offset + 1)].ToUShort();
                offset += 2;
            }
            DecoderConfigDescriptor = (DecoderConfigDescriptor)DescriptorFactory.Create(value[offset..]);
            offset += DecoderConfigDescriptor.TotalSize;
            UnknownData = value[offset ..Header.Length];
            Debug.Assert(Data.ToByteVector() == value.ToByteVector());
        }
    }
        
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        var secondByte = DependsOnElementaryStreamID.HasValue ? DependsOnElementaryStreamIDFlagMask : 0
            + (Url is not null ? UrlFlagMask : 0)
            + (ObjectClockReferenceElementaryStreamID.HasValue ? ObjectClockReferenceElementaryStreamIDFlagMask : 0)
            + StreamPriority;

        builder.Add(ElementaryStreamID)
            .Add((byte) secondByte);
        if(DependsOnElementaryStreamID.HasValue)
            builder.Add(DependsOnElementaryStreamID.Value);
        if(Url is not null)
            builder.Add((byte)Url.Length).Add(Url);
        if(ObjectClockReferenceElementaryStreamID.HasValue)
            builder.Add(ObjectClockReferenceElementaryStreamID.Value);
        DecoderConfigDescriptor.Render(builder);
        return builder.Add(UnknownData);
    }
        
    /// <inheritdoc />
    public override ulong ActualDataSize => 3UL 
        + (DependsOnElementaryStreamID.HasValue ? 2UL : 0UL)
        + (Url is not null ? (ulong)Url.Length + 1UL : 0)
        + (ObjectClockReferenceElementaryStreamID.HasValue ? 2UL : 0)
        + (DecoderConfigDescriptor.ActualSize)
        + (ulong)UnknownData.Count;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} UnknownSize:{UnknownData.Count} ES_ID: {ElementaryStreamID}, SP: {StreamPriority}, D_ID: {DependsOnElementaryStreamID}, Url: {Url}, OCR_ID: {ObjectClockReferenceElementaryStreamID}"
            +"\n"+ $"{DecoderConfigDescriptor.DebugDisplay(level + 1)}";
}