using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes;

/// <summary> Box whose sole purpose is to contain and group a set of related boxes. </summary>
public abstract class ContainerBox : Box, IContainerBox
{
    /// <summary> Constructs and initializes a new instance of <see cref="ContainerBox" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="HandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected ContainerBox(BoxHeader header, HandlerBox? handler = null) 
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
    /// <summary>Total size of all children.</summary>
    public ulong TotalChildrenSize => Children.Aggregate(0UL, (sum, box) => sum + box.Size);

    /// <inheritdoc />
    public override ulong ActualSize => Header.HeaderSize + Children.Aggregate(0UL, (sum, box) => sum + box.ActualSize);    
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => base.DebugDisplay(level) 
           + "\n" + Children.Select(c => c.DebugDisplay(level + 1)).Join();
}