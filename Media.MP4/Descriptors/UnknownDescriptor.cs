using Vipl.Base;

namespace Vipl.Media.MP4.Descriptors;

/// <summary> Descriptor which is not known to the library. </summary>
public class UnknownDescriptor : BaseDescriptor
{
    /// <summary> Creates a new instance of the <see cref="UnknownDescriptor"/> class. </summary>
    /// <param name="header">Header of descriptor</param>
    /// <param name="data">Data of descriptor</param>
    public UnknownDescriptor(DescriptorHeader header, Span<byte> data) : base(header)
    {
        _data = data;
    }

    private ByteVector _data;
    ///<inheritdoc />
    public sealed override Span<byte> Data { get => _data; set => _data = value; }

    ///<inheritdoc />
    public override ulong ActualDataSize => (ulong) _data.Count;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {nameof(UnknownDescriptor)}";

}