using Vipl.Base;
using Vipl.Media.Abstraction;

namespace Vipl.Media.Core;

public abstract partial class MediaFile
{
    /// <summary> Constructs and initializes a new instance of <see cref="MediaFile" /> for a specified path in the local file system. </summary>
    /// <param name="path"> A <see cref="string" /> object containing the path of the file to use in the new instance. </param>
    protected MediaFile(string path)
    {
        File = new LocalFile(path);
    }

    /// <summary>  Constructs and initializes a new instance of <see cref="MediaFile" /> for a specified file abstraction. </summary>
    /// <param name="abstraction"> A <see cref="IFile" /> object to use when reading from and writing to the file. </param>
    protected MediaFile(IFile abstraction)
    {
        File = abstraction;
    }
}