namespace Vipl.Media.Abstraction;

/// <summary> This interface inherits <see cref="ICodec" /> to provide  information about an audio codec. </summary>
/// <remarks> <para>When dealing with a <see cref="ICodec" />, if <see cref="ICodec.MediaTypes" /> contains <see cref="MediaTypes.Audio" />,
///  it is safe to assume that the object also inherits <see cref="IAudioCodec" /> and can be recast without issue.</para> </remarks>
public interface IAudioCodec : ICodec
{
    /// <summary> Gets the bitrate of the audio represented by the current instance. </summary>
    int AudioBitrate { get; }
    /// <summary> Gets the sample rate of the audio represented by the current instance. </summary>
    int AudioSampleRate { get; }
    /// <summary> Gets the number of channels in the audio represented by the current instance. </summary>
    int AudioChannels { get; }
}