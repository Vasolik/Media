using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Descriptors;

/// <summary> This class implementation of a ISO/IEC 14496-1 BaseDescriptor.
/// <para>This class is an abstract base class that is extended by the descriptor classes specified in 7.2.6. Each
/// descriptor constitutes a self-describing class, identified by a unique class tag. This abstract base class
/// establishes a common name space for the class tags of these descriptors. The values of the class tags are
/// defined in Table 1. As an expandable class the size of each class instance in bytes is encoded and accessible
/// through the instance variable sizeOfInstance (see 8.3.3).</para> </summary>
public abstract class BaseDescriptor 
{
    /// <summary> Creates a new instance of the <see cref="BaseDescriptor"/> class. </summary>
    /// <param name="header">Header of descriptor</param>
    protected BaseDescriptor(DescriptorHeader header)
    {
        Header = header;
    }

    /// <summary> Header of the descriptor. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public DescriptorHeader Header { get; set; }

    
    /// <summary>Descriptor as <see cref="ByteVector"/>. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public abstract Span<byte> Data { get; set; }
    
    /// <summary> Renders the current instance as a byte vector to byte vector. </summary>
    /// <returns>Builder instance for chaining.</returns>
    public IByteVectorBuilder Render()
    {
        return Render(new ByteVectorBuilder((int)ActualSize));
    }

    /// <summary> Renders the current instance as a byte vector to byte vector builder. </summary>
    /// <param name="builder">Builder to store current instance.</param>
    /// <returns>Builder instance for chaining.</returns>
    public virtual IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        builder.Add((byte)Header.Tag)
            .Add((byte)0x80).Add((byte)0x80).Add((byte)0x80)
            .Add((byte)Header.Length);
        return RenderData(builder);
    }
    
    /// <summary> Renders the current instance <see cref="Data"/>
    /// as a byte vector to byte vector builder.  </summary>
    /// <param name="builder">Builder to store current instance.</param>
    /// <returns>Builder instance for chaining.</returns>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return builder.Add(Data);
    }

    /// <summary> Actual size of the descriptor in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="DescriptorHeader.Length"/>, this value is calculated after every change of data. </summary>
    public virtual ulong ActualSize => 5 + ActualDataSize;
    
    /// <summary> Actual size of data in description. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once MemberCanBeProtected.Global
    public abstract ulong ActualDataSize { get; }
    
    /// <summary> Debug string use to print during debug. </summary>
    /// <param name="level">Level on which this element has been found</param>
    /// <returns>Debug string</returns>
    public virtual string DebugDisplay(int level)
        => $"{Header.Tag.GetDescription()} ({Header.Length.BytesToString()})".Intend(level, true);
    
    /// <summary> Total Size of the descriptor in the file. This is the size of the header plus the size of the data. </summary>
    public int TotalSize => Header.Length + 5;
}