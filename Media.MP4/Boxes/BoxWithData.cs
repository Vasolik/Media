using System.Diagnostics;
using Vipl.Base;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes;

/// <summary> Box with Data. </summary>
public abstract class BoxWithData : Box
{
    /// <summary> Constructs and initializes a new instance of <see cref="BoxWithData" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="HandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
#pragma warning disable CS8618 
    protected BoxWithData(BoxHeader header, HandlerBox? handler = null)
        : base(header, handler)
    {
        
    }
#pragma warning restore CS8618
    
    /// <summary> Gets the data contained in the box. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual ByteVector Data { get; set; }

    /// <inheritdoc />
    protected override async Task<Box> InitAsync(MP4 file)
    {
        file.Seek((long)DataPosition, SeekOrigin.Begin);
        var dataValue = await file.ReadBlockAsync((uint) DataSize);
        Data = dataValue;
        Debug.Assert(Data == dataValue);
        Debug.Assert(Size == ActualSize);
        return this;
    }
    
    /// <inheritdoc/>
    public override IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        base.Render(builder);
        return RenderData(builder);
    }
    
    /// <summary> Renders the current instance <see cref="Data"/>
    /// as a byte vector to byte vector builder.  </summary>
    /// <param name="builder">Builder to store current instance.</param>
    /// <returns>Builder instance for chaining.</returns>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return builder.Add(Data);
    }

    /// <inheritdoc />
    public override ulong ActualSize => Header.HeaderSize + ActualDataSize;
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Box.DataSize"/>, this value is calculated after every change of data. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public abstract ulong ActualDataSize { get;  }
}