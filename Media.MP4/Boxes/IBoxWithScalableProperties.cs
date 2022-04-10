namespace Vipl.Media.MP4.Boxes;

/// <summary> Box that have time which needs to be scaled to the media timescale. </summary>
public interface IBoxWithMovieHeaderScalableProperties
{
    /// <summary> Changes the timescale of the time properties in this box. </summary>
    /// <param name="oldTimeScale">Value of time scale which was before update.</param>
    /// <param name="newTimescale">New time scale.</param>
    public void ChangeTimescale(uint oldTimeScale, uint newTimescale);
}

/// <summary> Box that have time which needs to be scaled to the media timescale. </summary>
public interface IBoxMediaHeaderScalableProperties
{
    /// <summary> Changes the timescale of the time properties in this box. </summary>
    /// <param name="oldTimeScale">Value of time scale which was before update.</param>
    /// <param name="newTimescale">New time scale.</param>
    public void ChangeTimescale(uint oldTimeScale, uint newTimescale);
}