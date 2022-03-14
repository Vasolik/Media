using Vipl.Base;
using Vipl.Media.Abstraction;

namespace Vipl.Media.Core;

public abstract partial class MediaFile
{
    /// <summary> Searches forwards through a file for a specified pattern, starting at a specified offset. </summary>
    /// <param name="pattern">A <see cref="ByteVector" /> object containing a pattern to search for in the current instance. </param>
    /// <param name="startPosition"> A <see cref="int" /> value specifying at what seek position to start searching. </param>
    /// <param name="before"> A <see cref="ByteVector" /> object specifying a pattern that the searched for
    /// pattern must appear before. If this  pattern is found first, -1 is returned.</param>
    /// <returns>  A <see cref="long" /> value containing the index at which the value was found. If not found, -1 is returned. </returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public async Task<long> FindAsync(ByteVector pattern, long startPosition = 0, ByteVector? before = null)
    {
        if (File.IsClosed)
        {
            await File.SetModeAsync(AccessMode.Read);
        }

        if (pattern.Count > File.BufferSize)
            File.BufferSize = (uint)pattern.Count * 2;

        // The position in the file that the current buffer
        // starts at.

        var bufferOffset = startPosition;
        var originalPosition = File.Position;

        try
        {
            // Start the search at the offset.
            File.Position = startPosition;
            for (var buffer = await File.ReadBlockAsync(File.BufferSize); buffer.Count > 0; buffer = await File.ReadBlockAsync(File.BufferSize))
            {
                var location = buffer.Find(pattern.Data);
                if (before is not null)
                {
                    var beforeLocation = buffer.Find(pattern.Data);
                    if (beforeLocation >= 0 && location == -1)
                        return -1;
                    if (beforeLocation < location)
                        return -1;
                }

                if (location >= 0)
                    return bufferOffset + location;

                // Ensure that we always rewind the stream a little so we never have a partial
                // match where our data exists between the end of read A and the start of read B.
                bufferOffset += File.BufferSize - pattern.Count;
                if (before is not null && before.Count > pattern.Count)
                    bufferOffset -= before.Count - pattern.Count;
                File.Position = bufferOffset;
            }

            return -1;
        }
        finally
        {
            File.Position = originalPosition;
        }
    }
    
    /// <summary> Searches backwards through a file for a specified pattern, starting at a specified offset. </summary>
    /// <param name="pattern"> A <see cref="ByteVector" /> object containing a pattern to search for in the current instance. </param>
    /// <param name="startPosition"> A <see cref="int" /> value specifying at what seek position to start searching. </param>
    /// <param name="after"> A <see cref="ByteVector" /> object specifying a pattern that the searched for pattern must appear after. If this pattern is found first, -1 is returned. </param>
    /// <returns> A <see cref="long" /> value containing the index at which the value was found. If not found, -1 is returned. </returns>
    /// <remarks> Searching for <paramref name="after" /> is not yet  implemented. </remarks>
    public async Task<long> ReverseFindAsync(ByteVector pattern, long startPosition = 0, ByteVector? after = null)
    {
        if (File.IsClosed)
        {
            await File.SetModeAsync(AccessMode.Read);
        }

        if (pattern.Count > File.BufferSize)
            File.BufferSize = (uint)pattern.Count * 2;
        var originalPosition = File.Position;
        try
        {
            var bufferOffset = File.Length - startPosition;

            var readSize = (uint)Math.Min(bufferOffset, File.BufferSize);
            bufferOffset -= readSize;
            File.Position = bufferOffset;
            
            for (var buffer = await File.ReadBlockAsync(readSize); buffer.Count > 0; buffer = await File.ReadBlockAsync(readSize))
            {
                long location = buffer.ReverseFind(pattern.Data);
                if (after is not null)
                {
                    var afterLocation = buffer.Find(after.Data);
                    if (afterLocation > location)
                        return -1;
                }
                if (location >= 0)
                    return bufferOffset + location;
                
                readSize = (uint) Math.Min(bufferOffset, File.BufferSize);
                bufferOffset -= readSize;
                if (readSize + pattern.Count > File.BufferSize)
                    bufferOffset += pattern.Count;

                File.Position = bufferOffset;
            }
            
            return -1;
        }
        finally
        {
            File.Position = originalPosition;
        }
    }

}