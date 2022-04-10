using Vipl.Base;

namespace Vipl.Media.Abstraction;

/// <summary> This interface provides generic information about a picture,
/// including its contents, as used by various formats.</summary>
public interface IPicture
{
    /// <summary> Gets and sets the mime-type of the picture data stored in the current instance. </summary>
    string MimeType { get; set; }

    /// <summary>  Gets and sets the type of content visible in the picture stored in the current instance. </summary>
    PictureType Type { get; set; }
        
    /// <summary> Gets and sets a filename of the picture stored in the current instance. </summary>
    /// <value> A <see cref="string" /> object containing the filename, with its extension,
    /// of the picture stored in the current instance. </value>
    string Filename { get; set; }
        
    /// <summary> Gets and sets a description of the picture stored in the  current instance. </summary>
    string Description { get; set; }

    /// <summary> Gets and sets the picture data stored in the current instance. </summary>
    ByteVector Data { get; set; }
}