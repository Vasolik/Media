namespace Vipl.Media.MP4.Descriptors;

/// <summary> Factory that creates descriptors. </summary>
public static class DescriptorFactory
{
    /// <summary> Creates a descriptor from the given data. </summary>
    /// <param name="data">Data to create the descriptor from.</param>
    /// <returns>Descriptor created from the given data.</returns>
    public static BaseDescriptor Create(Span<byte> data)
    {
        var header = new DescriptorHeader(data);
        return header.Tag switch
        {
            DescriptorHeader.DescriptorTag.Forbidden1 => throw new ArgumentException("Forbidden1 tag is not allowed."),
            DescriptorHeader.DescriptorTag.Forbidden2 => throw new ArgumentException("Forbidden2 tag is not allowed."),
            DescriptorHeader.DescriptorTag.ProfileLevelIndicationIndexDescriptorTag => new ProfileLevelIndicationIndexDescriptor(header, data[header.SizeOfHeader..(header.SizeOfHeader+ header.Length)]),
            DescriptorHeader.DescriptorTag.ElementaryStreamDescriptorTag => new ElementaryStreamDescriptor(header, data[header.SizeOfHeader..(header.SizeOfHeader+ header.Length)]),
            DescriptorHeader.DescriptorTag.DecoderConfigDescriptorTag => new DecoderConfigDescriptor(header, data[header.SizeOfHeader..(header.SizeOfHeader+ header.Length)]),
            _ => new UnknownDescriptor(header, data[header.SizeOfHeader..(header.SizeOfHeader+ header.Length)])
        };
    }
    
}