using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes;

/// <summary> Box whose sole purpose is to contain and group a set of related boxes. </summary>
public abstract class FullContainerBox : FullBox, IContainerBox
{
    /// <summary> Constructs and initializes a new instance of <see cref="FullContainerBox" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected FullContainerBox(BoxHeader header, IsoHandlerBox? handler = null) 
        : base(header, handler)
    {
    }
        
    /// <inheritdoc />
    protected override async Task<Box> InitAsync(MP4 file)
    {
        await this.LoadChildrenAsync(file);
        Debug.Assert(Size == ActualSize);
        return this;
    }

    /// <inheritdoc />
    public IList<Box> Children { get; } = new List<Box>();
    
    /// <inheritdoc />
    ulong IContainerBox.ChildrenPosition => DataPosition;
    
    /// <inheritdoc />
    ulong IContainerBox.ChildrenSize => DataSize;

    /// <inheritdoc />
    public override IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        base.Render(builder);
        Children.ForEach(c => c.Render(builder));
        return builder;
    }

    /// <inheritdoc />
    public override ulong ActualSize => Header.HeaderSize + 4 + Children.Aggregate(0UL, (sum, box) => sum + box.ActualSize);    
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => base.DebugDisplay(level) 
           + "\n" + Children.Select(c => c.DebugDisplay(level + 1)).Join();
}