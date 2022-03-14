using Vipl.Base;
using Vipl.Media.Abstraction;

namespace Vipl.Media.Core;

public abstract partial class MediaFile: IFile
{
    /// <inheritdoc/>
    public string Name => File.Name;

    /// <inheritdoc/>
    public async Task SetModeAsync(AccessMode value)
    {
        await File.SetModeAsync(value);
    }
    /// <inheritdoc/>
    public AccessMode Mode => File.Mode;
    /// <inheritdoc/>
    public long Position
    {
        get => File.Position;
        set => File.Position = value;
    }
    /// <inheritdoc/>
    public async Task<ByteVector> ReadBlockAsync(uint length)
    {
        return await File.ReadBlockAsync(length);
    }
    /// <inheritdoc/>
    public async Task WriteBlockAsync(ByteVector data)
    {
        await File.WriteBlockAsync(data);
    }
    /// <inheritdoc/>
    public async Task InsertAsync(ByteVector data, long start, long replace = 0)
    {
        await File.InsertAsync(data, start, replace);
    }
    /// <inheritdoc/>
    public async Task InsertAsync(long size, long start)
    {
        await File.InsertAsync(size, start);
    }
    /// <inheritdoc/>
    public async Task RemoveBlockAsync(long start, long length)
    {
        await File.RemoveBlockAsync(start, length);
    }
    /// <inheritdoc/>
    public async Task InsertAsync(ByteVector? data, long size, long start, long replace)
    {
        await File.InsertAsync(data, size, start, replace);
    }
    /// <inheritdoc/>
    public void Truncate(long length)
    {
        File.Truncate(length);
    }
    /// <inheritdoc/>
    public void Seek(long offset, SeekOrigin origin)
    {
        File.Seek(offset, origin);
    }
    /// <inheritdoc/>
    public long Length => File.Length;
    /// <inheritdoc/>
    public uint BufferSize
    {
        get => File.BufferSize;
        set => File.BufferSize = value;
    }

    /// <summary> Gets the <see cref="IFile"/> representing the file. </summary>
    public IFile File { get; }
}