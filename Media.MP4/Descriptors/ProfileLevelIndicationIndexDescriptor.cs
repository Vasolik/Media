using System.Diagnostics;
using Vipl.Base;

namespace Vipl.Media.MP4.Descriptors;

/// <summary> A unique identifier for the set of profile and level indications described in this descriptor within
/// the name scope defined by the Initial object descriptor </summary>
public class ProfileLevelIndicationIndexDescriptor : BaseDescriptor
{
    /// <summary> Creates a new instance of the <see cref="ProfileLevelIndicationIndexDescriptor"/> class. </summary>
    /// <param name="header">Header of descriptor</param>
    /// <param name="data">Data in descriptor</param>
    public ProfileLevelIndicationIndexDescriptor(DescriptorHeader header, Span<byte> data) : base(header)
    {
        Data = data;
    }
    
    ///  <summary> A unique identifier for the set of profile and level indications described in
    /// this descriptor within the name scope defined by the Initial object descriptor </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public byte ProfileLevelIndicationIndex { get; set; }
    
    /// <summary>Descriptor as <see cref="ByteVector"/>. </summary>
    public sealed override Span<byte> Data
    {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ProfileLevelIndicationIndex = value[0];
            Debug.Assert(Data == value);
        }
    }
        
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return builder.Add(ProfileLevelIndicationIndex);
    }
        
    /// <summary> Actual size of description. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public override ulong ActualDataSize => 1;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} PLI: {ProfileLevelIndicationIndex}";
}