using System.Diagnostics;
using Vipl.Base;
using Vipl.Media.MP4.Boxes.ISO_14496_12;
using Vipl.Media.MP4.Descriptors;

namespace Vipl.Media.MP4.Boxes.ISO_14496_1;

/// <summary>This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-14 ESDBox.
///<para>The <see cref="ElementaryStreamDescriptionBox"/> conveys all information related to a particular elementary stream and has three major
/// parts.</para></summary>
[HasBoxFactory("esds")]
public class ElementaryStreamDescriptionBox : FullBoxWithData
{
    private ElementaryStreamDescriptionBox (BoxHeader header,  HandlerBox? handler)
        : base (header, handler)
    {
    }

    /// <summary> Elementary Stream Descriptor for this stream. </summary>
    public ElementaryStreamDescriptor ElementaryStreamDescriptor { get; set; } = null!;
    
    /// <inheritdoc />
    public override ByteVector Data
    {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ElementaryStreamDescriptor = (ElementaryStreamDescriptor) DescriptorFactory.Create(value);
            Debug.Assert(Data == value);
            
        }
    }
    
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return ElementaryStreamDescriptor.Render(builder);
    }
    
    /// <inheritdoc />
    public override ulong ActualDataSize => ElementaryStreamDescriptor.ActualSize;

    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => base.DebugDisplay(level) + "\n" + ElementaryStreamDescriptor.DebugDisplay(level + 1);

}