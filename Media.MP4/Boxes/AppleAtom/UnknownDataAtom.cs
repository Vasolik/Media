using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.AppleAtom;

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

/// <summary> This class extends <see cref="Box" /> to provide an implementation of Apple QuickTime DataAtom
/// with jpeg image as data.</summary>  
public class JpegDataAtom  : DataAtom
{
    internal JpegDataAtom(BoxHeader header, IsoHandlerBox? handler, DataAtomTypes type)
        : base(header, handler, type)
    {
        ImageData = null!;
    }
	
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ImageData = value[8..];
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>String representation of this data.</summary>
    public ByteVector ImageData { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder).Add(ImageData);
    }

    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + (ulong)ImageData.Count;
	
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {ImageData.Count.BytesToString()}";

}

/// <summary> This class extends <see cref="Box" /> to provide an implementation of Apple QuickTime DataAtom
/// with png image as data.</summary>  
public class PngDataAtom  : DataAtom
{
    internal PngDataAtom(BoxHeader header, IsoHandlerBox? handler, DataAtomTypes type)
        : base(header, handler, type)
    {
        ImageData = null!;
    }
	
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            ImageData = value[8..];
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>String representation of this data.</summary>
    public ByteVector ImageData { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder).Add(ImageData);
    }

    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + (ulong)ImageData.Count;
	
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} - {ImageData.Count.BytesToString()}";

}