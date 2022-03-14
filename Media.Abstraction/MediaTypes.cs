namespace Vipl.Media.Abstraction
{
    /// <summary> Indicates the types of media represented by a <see cref="ICodec" />. </summary>
    /// <remarks> These values can be bitwise combined to represent multiple media types. </remarks>
    [Flags]
    public enum MediaTypes
    {
        /// <summary> No media is present. </summary>
        None = 0,

        /// <summary> Audio is present. </summary>
        Audio = 1,

        /// <summary> Video is present. </summary>
        Video = 2,

        /// <summary> A Photo is present. </summary>
        Photo = 4,

        /// <summary> Text is present. </summary>
        Text = 8
    }
}