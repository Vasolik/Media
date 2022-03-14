using Vipl.Base;

namespace Vipl.Media.Core;
/// <summary>
/// Base class of all media files.
/// </summary>
public abstract partial class MediaFile
{
    /// <summary>  Indicates if tags can be written back to the current file or not </summary>
    /// <value>  A <see cref="bool" /> which is true if tags can be written to the current file, otherwise false. </value>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual bool Writeable => !PossiblyCorrupt;

    /// <summary>Indicates whether or not this file may be corrupt. </summary>
    /// <value><c>true</c> if possibly corrupt; otherwise, <c>false</c>. </value>
    /// <remarks> Files with unknown corruptions should not be written. </remarks>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool PossiblyCorrupt => !CorruptionReasons.Any();

    /// <summary> The reasons for which this file is marked as corrupt. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public IList<string> CorruptionReasons { get; } = new List<string>();

    /// <summary> Mark the file as corrupt. </summary>
    /// <param name="reason">The reason why this file is considered to be corrupt. </param>
    public void MarkAsCorrupt (string reason)
    {
        CorruptionReasons.Add (reason);
    }

    /// <summary> Dispose the current file. Equivalent to setting the mode to closed </summary>
    public void Dispose ()
    {
        File.Dispose();
        GC.SuppressFinalize(this);
    }
    
    /// <summary> Dispose the current file. Equivalent to setting the mode to closed </summary>
    public async ValueTask DisposeAsync ()
    {
        await File.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    /// <summary> Saves the changes made in the current instance to the  file it represents. </summary>
    public async Task SaveAsync()
    {
        if (!Writeable)
            throw new InvalidOperationException ("File not writeable.");

        if (PossiblyCorrupt)
            throw new CorruptFileException ("Corrupted file cannot be saved.");
        await SaveImplementationAsync();
    }

    /// <summary> This method should be implemented in derived class. It should save changes to file to file abstraction. </summary>
    protected abstract Task SaveImplementationAsync();

}