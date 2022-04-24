using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12.DataEntries;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of Apple QuickTime DataAtom
/// with unknown data type.</summary>  
public class UnknownDataAtom  : DataAtom
{
    internal UnknownDataAtom(BoxHeader header, IsoHandlerBox? handler, DataAtomTypes type)
        : base(header, handler, type)
    {
        UnknownData = null!;
    }
	
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            UnknownData = value[8..];
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>String representation of this data.</summary>
    public ByteVector UnknownData { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder).Add(UnknownData);
    }

    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + (ulong)UnknownData.Count;
	
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {UnknownData.Count.BytesToString()}";

}