namespace Vipl.Media.Abstraction;

/// <summary> This interface inherits <see cref="ICodec" /> to provide information about a photo. </summary>
/// <remarks> <para>When dealing with a <see cref="ICodec" />, if <see cref="ICodec.MediaTypes" /> contains
/// <see cref="MediaTypes.Photo"/>, it is safe to assume that the object also inherits <see cref="IPhotoCodec" /> and can be recast without issue.</para> </remarks>
public interface IPhotoCodec : ICodec
{
    /// <summary> Gets the width of the photo represented by the current instance. </summary>
    int PhotoWidth { get; }

    /// <summary> Gets the height of the photo represented by the current  instance. </summary>
    int PhotoHeight { get; }

    /// <summary> Gets the (format specific) quality indicator of the photo represented by the current instance.
    /// A value 0 means that there was no quality indicator for the format or the file. </summary>
    int PhotoQuality { get; }
}