namespace Vipl.Media.Abstraction
{
    /// <summary>  This interface inherits <see cref="ICodec" /> to provide information about a video codec. </summary>
    /// <remarks> <para>When dealing with a <see cref="ICodec" />, if <see cref="ICodec.MediaTypes" /> contains
    /// <see cref="MediaTypes.Video" />, it is safe to assume that the object also inherits <see cref="IVideoCodec" />
    /// and can be recast without issue.</para></remarks>
    public interface IVideoCodec : ICodec
    {
        /// <summary> Gets the width of the video represented by the current instance. </summary>
        int VideoWidth { get; }

        /// <summary> Gets the height of the video represented by the current instance. </summary>
        int VideoHeight { get; }
    }
}