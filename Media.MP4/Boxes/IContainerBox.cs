namespace Vipl.Media.MP4.Boxes;

/// <summary> Box with children boxes. </summary>
public interface IContainerBox
{
    /// <summary> Gets the children of the current instance.</summary>
    /// <value> A <see cref="T:IList{Box}" /> object enumerating the children of the current instance. </value>
    IList<Box> Children { get; }

    /// <summary>Total size of all children.</summary>
    ulong TotalChildrenSize => Children.Aggregate(0UL, (sum, box) => sum + box.Size);

    /// <summary> Gets a child box from the current instance by finding a matching box type. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" /> if no matching box was found. </returns>
    Box? GetChild(BoxType type)
    {
        return Children.FirstOrDefault(box => box.Type == type);
    }

    /// <summary> Gets all child boxes from the current instance by finding a matching box type. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A List of <see cref="Box" /> objects containing the matched box,
    /// or <see langword="null" /> if no matching boxes was found. </returns>
    List<Box> GetChildren(BoxType type)
    {
        return Children.Where(box => box.Type == type).ToList();
    }

    /// <summary> Gets a child box from the current instance by finding a matching box type, searching recursively. </summary>
    /// <param name="type"> A <see cref="BoxType" /> object containing the box type to match. </param>
    /// <returns> A <see cref="Box" /> object containing the matched box, or <see langword="null" />
    /// if no matching box was found. </returns>
    Box? GetChildRecursively(BoxType type)
    {
        return GetChild(type)
               ?? Children
                   .OfType<IContainerBox>()
                   .Select(box => box.GetChildRecursively(type)).FirstOrDefault(childBox => childBox != null);
    }

    /// <summary> Removes all children with a specified box type from the current instance. </summary>
    /// <param name="type">A <see cref="BoxType" /> object containing the box type to remove. </param>
    void RemoveChild(BoxType type)
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            if (Children[i].Type == type)
                Children.RemoveAt(i);
        }
    }
    
    /// <summary> Location of children in the box.</summary>
    ulong ChildrenPosition { get; }
    
    /// <summary> Size of all children in the box.</summary>
    ulong ChildrenSize { get; }
    
}