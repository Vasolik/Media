namespace Vipl.Media.MP4.Boxes;

/// <summary> Box with children boxes. </summary>
public interface IContainerElement<T>
{
    /// <summary> Gets the children of the current instance.</summary>
    /// <value> A <see cref="T:IList{T}" /> object enumerating the children of the current instance. </value>
    IList<T> Children { get; }
    
    /// <summary> Location of children in the element.</summary>
    ulong ChildrenPosition { get; }
    
    /// <summary> Size of all children in the element.</summary>
    ulong ChildrenSize { get; }
    
}