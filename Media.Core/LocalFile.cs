using Vipl.Base;
using Vipl.Media.Abstraction;

namespace Vipl.Media.Core;

/// <summary> This class implements <see cref="IFile" /> to provide support for accessing the local/standard file system. </summary>
public class LocalFile : IFile
{
    /// <summary> Contains buffer size to use when reading. </summary>
    private const int DefaultBufferSize = 8*1024;
    
    /// <inheritdoc/>
    public uint BufferSize { get; set; } = DefaultBufferSize;
    
    /// <summary>   Constructs and initializes a new instance of <see cref="LocalFile" /> for a specified path in the local file system. </summary>
    /// <param name="path"> A <see cref="string" /> object containing the path of the file to use in the new instance. </param>
    public LocalFile (string path)
    {
        Name = path;
    }

    /// <inheritdoc/>
    public string Name { get; }

    private Stream? _readStream;
    private Stream ReadExclusiveReadStream
    {
        get
        {
            if (_readStream is not null)
                return _readStream;
            _readStream = File.Open(Name, FileMode.Open, FileAccess.Read, FileShare.Read);
            _readStream.Position = Position;
            return _readStream;
        }
    }
    private Stream ReadStream =>
        Mode switch
        {
            AccessMode.ReadWrite => WriteStream,
            AccessMode.Read => ReadExclusiveReadStream,
            _ => throw new InvalidOperationException("File is closed")
        };


    private Stream? _writeStream;
    private Stream WriteStream
    {
        get
        {
            if (_writeStream is not null)
                return _writeStream;
            _writeStream = File.Open(Name, FileMode.Open, FileAccess.ReadWrite);
            _writeStream.Position = Position;
            return _writeStream;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    { 
        _readStream?.Dispose();
        _writeStream?.Dispose();
        _readStream = _writeStream = null;
        GC.SuppressFinalize(this);
    }
    
    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_readStream is not null)
        {
            await _readStream.DisposeAsync();
        }
        if (_writeStream is not null)
        {
            await _writeStream.DisposeAsync();
        }
        _readStream = _writeStream = null;
        GC.SuppressFinalize(this);
    }
    
    private AccessMode _mode;
    
    /// <inheritdoc/>
    public async Task SetModeAsync(AccessMode value)
    {
        if (value != AccessMode.Read && _readStream is not null)
        {
            await _readStream.DisposeAsync();
        }
        if (value != AccessMode.ReadWrite && _writeStream is not null)
        {
            await _writeStream.DisposeAsync();
        }

        _mode = value;
    }
    /// <inheritdoc/>
    public AccessMode Mode => _mode;
    
    /// <inheritdoc/>
    public long Position { get; set; }


    /// <inheritdoc/>
    public async Task<ByteVector> ReadBlockAsync(uint length)
    {
        if (length == 0)
            return new ByteVector();
        
        var oldMode = Mode;
        try
        {
            if (Mode == AccessMode.Closed)
            {
                await SetModeAsync(AccessMode.Read);
            }
            
            return await ReadStream.ToByteVectorAsync(length);
        }
        finally
        {
            Position = ReadStream.Position;
            await SetModeAsync(oldMode);
        }
    }

    /// <inheritdoc/>
    public async Task WriteBlockAsync(ByteVector data)
    {
        var oldMode = Mode;
        try
        {
            await SetModeAsync(AccessMode.ReadWrite);
            await WriteStream.WriteAsync(data.Memory[..data.Count]);
        }
        finally
        {
            Position = WriteStream.Position;
            await SetModeAsync(oldMode);
        }
        
    }

    /// <inheritdoc/>
    public async Task InsertAsync(ByteVector data, long start, long replace = 0)
    {
        await InsertAsync(data, data.Count, start, replace);
    }

    /// <inheritdoc/>
    public async Task InsertAsync(long size, long start)
    {
        await InsertAsync(null, size, start, 0);
    }

    /// <inheritdoc/>
    public async Task RemoveBlockAsync(long start, long length)
    {
        if (length <= 0)
            return;
        
        var oldMode = Mode;
        try
        {
            await SetModeAsync(AccessMode.ReadWrite);
            
            var newSize = ReadExclusiveReadStream.Length - length;

            ReadExclusiveReadStream.Position = start + length;
            WriteStream.Position = start;

            await ReadExclusiveReadStream.CopyToAsync(WriteStream, (int)BufferSize);
            await WriteStream.FlushAsync();
            await ReadExclusiveReadStream.DisposeAsync();
            _readStream = null;

            Truncate(newSize);
            
        }
        finally
        {
            Position = WriteStream.Position;
            await SetModeAsync(oldMode);
        }
    }
    /// <inheritdoc/>
    public async Task InsertAsync(ByteVector? data, long size, long start, long replace)
    {
        var oldMode = Mode;
        
        try
        {
            await SetModeAsync(AccessMode.ReadWrite);
            
            if (size == replace)
            {
                await InsertEntirelyReplacingAsync(data, size, start);
            }
            else if (size < replace)
            {
                await InsertAndReduceSizeAsync(data, size, start, replace);
            }
            else
            {
                await InsertAndIncreaseSizeAsync(data, size, start, replace);
            }
        }
        finally
        {
            Position = start + size;
            await SetModeAsync(oldMode);
        }
    }

    /// <inheritdoc/>
    public void Truncate(long length)
    {
        WriteStream.SetLength(length);
    }
    
    /// <inheritdoc/>
    public void Seek (long offset, SeekOrigin origin)
    {
        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length + offset,
            _ => throw new ArgumentOutOfRangeException(nameof(origin), origin, null)
        };

        if (Mode == AccessMode.Read)
        {
            ReadStream.Seek(offset, SeekOrigin.Begin);
        }

        if (Mode == AccessMode.ReadWrite)
        {
            WriteStream.Seek(offset, SeekOrigin.Begin);
        }
    }
    /// <inheritdoc/>
    public long Length =>
        Mode switch
        {
            AccessMode.Read => ReadStream.Length,
            AccessMode.ReadWrite => WriteStream.Length,
            _ => throw new InvalidOperationException("File is closed")
        };
    private async Task InsertAndIncreaseSizeAsync(ByteVector? data, long size, long start, long replace)
    {
        var bufferLength = CalculateMinBufferLengthForInsert(size, replace);
        var readPosition = start + replace;
        var writePosition = start;

        ReadExclusiveReadStream.Position = readPosition;
        var aboutToOverwrite = new byte[bufferLength];
        var readSize = await ReadExclusiveReadStream.ReadAsync(aboutToOverwrite.AsMemory(0, bufferLength));
        readPosition += readSize;

        if (data is not null)
        {
            WriteStream.Position = writePosition;
            await WriteStream.WriteAsync(data.Memory[..(int) Math.Min(data.Count, size)]);
            await WriteBlockAsync(data);
        }
        else if (start + size > WriteStream.Length)
        {
            WriteStream.SetLength(start + size);
        }

        writePosition += size;
        var buffer = new byte[aboutToOverwrite.Length];
        (buffer, aboutToOverwrite) = (aboutToOverwrite, buffer);

        while (readSize == aboutToOverwrite.Length)
        {
            ReadExclusiveReadStream.Position = readPosition;
            readSize = await ReadExclusiveReadStream.ReadAsync(aboutToOverwrite.AsMemory(0, aboutToOverwrite.Length));
            readPosition += readSize;

            WriteStream.Position = writePosition;
            await WriteStream.WriteAsync(buffer.AsMemory(0, buffer.Length));
            writePosition += bufferLength;

            (buffer, aboutToOverwrite) = (aboutToOverwrite, buffer);
        }

        await WriteStream.WriteAsync(buffer.AsMemory(0, readSize));
    }
    
    private int CalculateMinBufferLengthForInsert(long size, long replace)
    {
        var bufferLength = (int) (size - replace);
        var modulo = (int) (bufferLength % BufferSize);
        if (modulo != 0) bufferLength += (int) (BufferSize - modulo);
        return bufferLength;
    }

    private async Task InsertAndReduceSizeAsync(ByteVector? data, long size, long start, long replace)
    {
        WriteStream.Position = start;
        if (data is not null)
        {

            await WriteStream.WriteAsync(data.Memory[..(int) Math.Min(data.Count, size)]);
        }

        await RemoveBlockAsync(start + size, replace - size);
    }

    private async Task InsertEntirelyReplacingAsync(ByteVector? data, long size, long start)
    {
        WriteStream.Position = start;
        if (data is not null)
        {
            await WriteStream.WriteAsync(data.Memory[..(int) Math.Min(data.Count, size)]);
        }
    }
}