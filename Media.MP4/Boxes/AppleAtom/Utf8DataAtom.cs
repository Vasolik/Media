using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.AppleAtom;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of Apple QuickTime DataAtom
/// with data type of utf8 string.</summary>  
public class Utf8DataAtom  : DataAtom
{
    internal Utf8DataAtom(BoxHeader header, IsoHandlerBox? handler, DataAtomTypes type)
        : base(header, handler, type)
    {
    }
	
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            Utf8Data = value[8..].ToString(Encoding.UTF8);
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>String representation of this data.</summary>
    public string Utf8Data { get; set; } = "";

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder).Add(Utf8Data);
    }

    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + (ulong) Encoding.UTF8.GetByteCount(Utf8Data);
	
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {Utf8Data}";

}