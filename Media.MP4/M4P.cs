using Vipl.Media.Abstraction;
using Vipl.Media.Core;
using Vipl.Media.MP4.Boxes;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4;

/// <summary> Responsible for reading and writing MP4 files. </summary>
public class MP4 : MediaFile
{
    
    /// <summary>  Initializes a new instance of the <see cref="MP4" /> class. </summary>
    /// <param name="path"> Path to local file where MP4 file is stored.</param>
    public MP4(string path) : base(path)
    {
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="MP4" /> class.
    /// </summary>
    /// <param name="abstraction"> File abstraction used for reading/writing MP4 file.</param>
    public MP4(IFile abstraction) : base(abstraction)
    {
    }
    /// <inheritdoc />
    protected override Task SaveImplementationAsync()
    {
        throw new NotImplementedException();
    }
    
    /// <summary> Reads boxes from MP4 file. </summary>
    /// <param name="start">Starting location in file from which boxes will be read.</param>
    /// <param name="end">Ending location in file from which boxes will be read.</param>
    public async Task ParseBoxesAsync (long start, long end)
    {
        Box box;
        for (var position = start; position < end; position += (long)box.Size )
        {
            box = await BoxFactory.CreateBoxAsync(this, position);
            Boxes.Add(box);
        }

    }

    /// <summary> Boxes that are contained in MP4 file. </summary>
    public IList<Box> Boxes { get; } = new List<Box>();

    private MovieHeaderBox? _movieHeaderBox = null;

    /// <summary>Exposed <see cref="MovieHeaderBox"/>. THis box contain Important data for handling MP4 file. </summary>
    public MovieHeaderBox? MovieHeaderBox
    {
        get => _movieHeaderBox ??= (Boxes
                .OfType<ContainerBox>()
                .Select(b => b.GetChildRecursively(BoxType.MovieHeader))
                .FirstOrDefault(b => b is MovieHeaderBox) as MovieHeaderBox);
        set => _movieHeaderBox = value;
    }
    
    /// <summary>This is an integer that specifies the time-scale for the entire presentation; this is the number of
    /// time units that pass in one second. For example, a time coordinate system that measures time in
    /// sixtieths of a second has a time scale of 60. </summary>
    public uint Timescale => MovieHeaderBox?.Timescale ?? 1;
}