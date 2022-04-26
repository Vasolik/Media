using System.Diagnostics;
using Vipl.Base;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 SoundMediaHeaderBox.
/// <para>Audio tracks use the <see cref="SoundMediaHeaderBox"/>  in the MediaInformationBox as defined in 8.4.5. The sound
/// media header contains general presentation information, independent of the coding, for audio media.
/// This header is used for all tracks containing audio.</para> </summary>
[HasBoxFactory("smhd")]
public class SoundMediaHeaderBox : FullBoxWithData
{
    private SoundMediaHeaderBox (BoxHeader header,  HandlerBox? handler)
        : base (header, handler)
    {
    }

    /// <summary> Number that places mono audio tracks in a stereo space; 0 is centre (the
    /// normal value); full left is -1.0 and full right is 1.0. </summary>
    public FixedPoint8_8 Balance { get; set; }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            Balance = value[..2];
            Debug.Assert(Data == value);
        } 
    }

   
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(Balance)
            .Skip(2);
        return builder;
    }
    /// <inheritdoc />
    public override ulong ActualDataSize => 4UL;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} Balance: {Balance.Value}";
}