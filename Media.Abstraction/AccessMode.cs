namespace Vipl.Media.Abstraction;

/// <summary> Specifies the type of file access operations currently permitted on an instance of <see cref="IFile" />. </summary>
public enum AccessMode
{
    /// <summary> Read operations can be performed. </summary>
    Read,
    /// <summary> Read and write operations can be performed. </summary>
    ReadWrite,

    /// <summary> The file is closed for both read and write operations. </summary>
    Closed
}