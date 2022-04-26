using System.Diagnostics;
using System.Text;
using Vipl.Base;

namespace Vipl.Media.MP4.Boxes.AppleAtom.Metadata;

/// <summary>
///    This class extends <see cref="FullBox" /> to provide an
///    implementation of an name of additional metadata.
/// </summary>
[ HasBoxFactory("name")]
public class MetadataNameBox : FullBoxWithData
{
    private MetadataNameBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header,  handler)
    {
        Text = null!;
    }

    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            Text = value.ToString(Encoding.Latin1);
            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(Text, Encoding.Latin1);
        return builder;
    }

    /// <inheritdoc />
    public override ulong ActualDataSize => (ulong)Encoding.Latin1.GetByteCount(Text);

    /// <summary>  Text representation of the box content. </summary>
    public string Text { get; set; }

    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {Text}";
}