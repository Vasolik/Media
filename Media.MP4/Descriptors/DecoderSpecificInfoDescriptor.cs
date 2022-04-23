namespace Vipl.Media.MP4.Descriptors;

/// <summary>The decoder specific information constitutes an opaque container with information for a specific media decoder.
/// The existence and semantics of decoder specific information depends on the values of
/// DecoderConfigDescriptor.streamType and DecoderConfigDescriptor.objectTypeIndication. </summary>
public abstract class DecoderSpecificInfoDescriptor: BaseDescriptor
{
    /// <inheritdoc />
    protected DecoderSpecificInfoDescriptor(DescriptorHeader header) : base(header)
    {
    }
}