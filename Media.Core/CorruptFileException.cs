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
}