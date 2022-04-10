using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.Core;

namespace Vipl.Media.MP4.Boxes;

/// <summary> Box whose sole purpose is to contain and group a set of related boxes. </summary>
public abstract class ContainerBox : Box
{
    /// <summary> Constructs and initializes a new instance of <see cref="ContainerBox" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected ContainerBox(BoxHeader header, IsoHandlerBox? handler = null) 
        : base(header, handler)
    {
    }
        
    /// <inheritdoc />
    protected override async Task<Box> InitAsync(MP4 file)
    {
        await LoadChildrenAsync(file);
        Debug.Assert(Size == ActualSize);
        return this;
    }

    /// <summary> Gets the children of the current instance.</summary>
    /// <value> A <see cref="T:IList{Box}" /> object enumerating the children of the current instance. </value>
    public IList<Box> Children { get; } = new List<Box>();
    
    /// <summary> Gets a child box from the current instance by finding a matching box type. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" /> if no matching box was found. </returns>
    public Box? GetChild(BoxType type)
    {
        return Children.FirstOrDefault(box => box.Type == type);
    }

    /// <summary> Gets all child boxes from the current instance by finding a matching box type. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A List of <see cref="Box" /> objects containing the matched box,
    /// or <see langword="null" /> if no matching boxes was found. </returns>
    public List<Box> GetChildren(BoxType type)
    {
        return Children.Where(box => box.Type == type).ToList();
    }

    /// <summary> Gets a child box from the current instance by finding a matching box type, searching recursively. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" />
    /// if no matching box was found. </returns>
    public Box? GetChildRecursively(BoxType type)
    {
        return GetChild(type)
               ?? Children
                   .OfType<ContainerBox>()
                   .Select(box => box.GetChildRecursively(type)).FirstOrDefault(childBox => childBox != null);
    }

    /// <summary> Removes all children with a specified box type from the current instance. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to remove. </param>
    public void RemoveChild(BoxType type)
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            if (Children[i].Type == type)
                Children.RemoveAt(i);
        }
    }

    /// <summary> Loads the children of the current instance from a specified file using
    /// the internal data position and size. </summary>
    /// <param name="file"> The <see cref="MediaFile" /> from which the current instance
    /// was read and from which to read the children. </param>
    /// <returns> A <see cref="T:System.Collections.Generic.IEnumerable`1" /> object
    /// enumerating the boxes read from the file. </returns>
    public async Task LoadChildrenAsync(MP4 file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));
        Children.Clear();
        var position = (long)DataPosition;
        var end = position + (long)DataSize;

        
        while (position < end)
        {
            var child = await BoxFactory.CreateBoxAsync(file, position, Header, Handler, Children.Count);
            if (child.Size == 0)
                break;

            Children.Add(child);
            if (child is IsoHandlerBox isoHandlerBox)
                Handler = isoHandlerBox;
            position += (long)child.Size;
        }
    }
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
        => Header.BoxType.Header.ToString().Intend(level) 
           + "\n" + Children.Select(c => c.DebugDisplay(level + 1)).Join();
}



/// <summary> Box whose sole purpose is to contain and group a set of related boxes. </summary>
public abstract class FullContainerBox : FullBox
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
        await LoadChildrenAsync(file);
        Debug.Assert(Size == ActualSize);
        return this;
    }

    /// <summary> Gets the children of the current instance.</summary>
    /// <value> A <see cref="T:IList{Box}" /> object enumerating the children of the current instance. </value>
    public IList<Box> Children { get; } = new List<Box>();
    
    /// <summary> Gets a child box from the current instance by finding a matching box type. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" /> if no matching box was found. </returns>
    public Box? GetChild(BoxType type)
    {
        return Children.FirstOrDefault(box => box.Type == type);
    }

    /// <summary> Gets all child boxes from the current instance by finding a matching box type. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A List of <see cref="Box" /> objects containing the matched box,
    /// or <see langword="null" /> if no matching boxes was found. </returns>
    public List<Box> GetChildren(BoxType type)
    {
        return Children.Where(box => box.Type == type).ToList();
    }

    /// <summary> Gets a child box from the current instance by finding a matching box type, searching recursively. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" />
    /// if no matching box was found. </returns>
    public Box? GetChildRecursively(BoxType type)
    {
        return GetChild(type)
               ?? Children
                   .OfType<ContainerBox>()
                   .Select(box => box.GetChildRecursively(type)).FirstOrDefault(childBox => childBox != null);
    }

    /// <summary> Removes all children with a specified box type from the current instance. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to remove. </param>
    public void RemoveChild(BoxType type)
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            if (Children[i].Type == type)
                Children.RemoveAt(i);
        }
    }

    /// <summary> Loads the children of the current instance from a specified file using
    /// the internal data position and size. </summary>
    /// <param name="file"> The <see cref="MediaFile" /> from which the current instance
    /// was read and from which to read the children. </param>
    /// <returns> A <see cref="T:System.Collections.Generic.IEnumerable`1" /> object
    /// enumerating the boxes read from the file. </returns>
    public async Task LoadChildrenAsync(MP4 file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));
        Children.Clear();
        var position = (long)DataPosition;
        var end = position + (long)DataSize;

        
        while (position < end)
        {
            var child = await BoxFactory.CreateBoxAsync(file, position, Header, Handler, Children.Count);
            if (child.Size == 0)
                break;

            Children.Add(child);
            if (child is IsoHandlerBox isoHandlerBox)
                Handler = isoHandlerBox;
            position += (long)child.Size;
        }
    }
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
    public override ulong ActualSize => Header.HeaderSize + 4 + Children.Aggregate(0UL, (sum, box) => sum + box.ActualSize);    
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => Header.BoxType.Header.ToString().Intend(level) 
           + "\n" + Children.Select(c => c.DebugDisplay(level + 1)).Join();
}