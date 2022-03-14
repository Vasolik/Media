namespace Vipl.Media.Abstraction;

/// <summary>This interface provides information specific to lossless audio codecs. </summary>
public interface ILosslessAudioCodec : IAudioCodec
{
    /// <summary> Gets the number of bits per sample in the audio represented by the current instance. </summary>
    int BitsPerSample { get; }
}