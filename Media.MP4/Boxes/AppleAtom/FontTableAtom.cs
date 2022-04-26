using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime FontTableAtom.
/// <para>This atom specifies the font used to display the subtitle.</para></summary>
[HasBoxFactory("ftab")]
public class FontTableAtom  : BoxWithData
{
    private FontTableAtom  (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            var count = value[..2].ToUShort();
            FontId = value[2..4].ToUShort();
            var size = value[4];
            FontName = size > 0 ? value[5..(5 + size)].ToString(Encoding.UTF8) : null;
            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add((ushort)1)
            .Add(FontId);
        if (FontName is null)
        {
            builder.Add((byte) 0);
        }
        else
        {
            builder.Add((byte) Encoding.UTF8.GetByteCount(FontName))
                .Add(FontName);
        }

        return builder;
    }
    
    /// <summary> An unsigned 16-bit integer that identifies the font. This can be any number to uniquely
    /// identify this font in this table, but it must match the font number specified in the subtitle sample
    /// description and in any per-sample style records ('styl'). </summary>
    public ushort FontId { get; set; }

    /// <summary> Font names entries. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public string? FontName { get; set; } 
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 5UL + (ulong)(FontName is null? 0 : Encoding.UTF8.GetByteCount(FontName));
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} FID: {FontId} FN: {FontName?? ""}";

}