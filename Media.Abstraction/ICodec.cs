namespace Vipl.Media.Abstraction
{
    /// <summary> This interface provides basic information, common to all media  codecs. </summary>
    public interface ICodec
    {
        /// <summary> Gets the duration of the media represented by the current instance. </summary>
        TimeSpan Duration { get; }

        /// <summary> Gets the types of media represented by the current instance. </summary>
        MediaTypes MediaTypes { get; }

        /// <summary> Gets a text description of the media represented by the current instance.</summary>
        string Description { get; }
    }
}