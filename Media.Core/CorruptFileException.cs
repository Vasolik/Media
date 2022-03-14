using System.Runtime.Serialization;

namespace Vipl.Media.Core;

/// <summary> This class extends <see cref="Exception" /> and is used to indicate that a file is corrupt. </summary>
[Serializable]
public class CorruptFileException : Exception
{
    /// <summary>  Constructs and initializes a new instance of <see cref="CorruptFileException" /> with a specified message. </summary>
    /// <param name="message"> A <see cref="string" /> containing a message explaining the reason for the exception. </param>
    public CorruptFileException (string message) : base (message)
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="CorruptFileException" /> with the default values. </summary>
    public CorruptFileException ()
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="CorruptFileException" /> with a specified
    /// message containing a specified exception. </summary>
    /// <param name="message"> A <see cref="string" /> containing a message explaining the reason for the exception. </param>
    /// <param name="innerException"> A <see cref="Exception" /> object to be contained in the new exception. For example, previously caught exception. </param>
    public CorruptFileException (string message, Exception innerException)
        : base (message, innerException)
    {
    }

    /// <summary> Constructs and initializes a new instance of <see cref="CorruptFileException" /> from a specified
    /// serialization info and streaming context.</summary>
    /// <param name="info">A <see cref="SerializationInfo" /> object containing the serialized data to be used for the new instance. </param>
    /// <param name="context"> A <see cref="StreamingContext" /> object containing the streaming context information for the new instance. </param>
    /// <remarks> This constructor is implemented because <see cref="CorruptFileException" /> implements the <see cref="ISerializable" /> interface. </remarks>
    protected CorruptFileException (SerializationInfo info, StreamingContext context)
        : base (info, context)
    {
    }
}