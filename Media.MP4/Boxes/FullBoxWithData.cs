using System.Diagnostics;
using Vipl.Base;

namespace Vipl.Media.MP4.Boxes;

/// <summary> FullBox with Data. </summary>
public abstract class FullBoxWithData : FullBox
{
    /// <summary> Constructs and initializes a new instance of <see cref="FullBoxWithData" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
#pragma warning disable CS8618 
    protected FullBoxWithData(BoxHeader header, IsoHandlerBox? handler = null)
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
        Data = await file.ReadBlockAsync((uint) DataSize);
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
    public abstract IByteVectorBuilder RenderData(IByteVectorBuilder builder);
    
    /// <inheritdoc />
    public override ulong ActualSize => Header.HeaderSize + 4 + ActualDataSize;
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Box.DataSize"/>, this value is calculated after every change of data. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public  abstract ulong ActualDataSize { get;  }
}