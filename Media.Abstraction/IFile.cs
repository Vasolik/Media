using Vipl.Base;
namespace Vipl.Media.Abstraction
{
    /// <summary> This interface provides abstracted access to a file. </summary>
    public interface IFile : IDisposable, IAsyncDisposable
    {
        /// <summary> Gets the name or identifier used by the implementation. </summary>
        /// <remarks> This value would typically represent a path or  URL to be used when identifying the file in the
        ///    file system, but it could be any value as appropriate for the implementation. </remarks>
        string Name { get; }
        /// <summary> Change Access Mode of file. File can be ready for reading, ready for writing or closed. </summary>
        /// <param name="value">New access mode of the file.</param>
        /// <returns>Task for awaiting the change.</returns>
        Task SetModeAsync(AccessMode value);
        /// <summary> Tell if file can be read or written or if it it closed. </summary>
        AccessMode Mode { get; }
        /// <summary> Current read/write position in file. </summary>
        long Position { get; set; }
    
        /// <summary> Reads a specified number of bytes at the current seek position from the current instance. </summary>
        /// <param name="length"> A <see cref="int" /> value specifying the number of bytes  to read. </param>
        /// <returns> A <see cref="ByteVector" /> object containing the data  read from the current instance. </returns>
        /// <remarks> <para>This method reads the block of data at the current seek position.
        /// To change the seek position, use <see cref="Seek(long,SeekOrigin)" />.</para> </remarks>
        Task<ByteVector> ReadBlockAsync(uint length);

        /// <summary>  Writes a block of data to the file represented by the current instance at the current seek position. </summary>
        /// <param name="data"> A <see cref="ByteVector" /> object containing data to be written to the current instance. </param>
        /// <remarks> This will overwrite any existing data at the seek position and append new data to the file if writing past the current end. </remarks>
        // ReSharper disable once MemberCanBePrivate.Global
        Task WriteBlockAsync(ByteVector data);

        /// <summary> Inserts a specified block of data into the file represented by the current
        /// instance at a specified location, replacing a specified number of bytes. </summary>
        /// <param name="data"> A <see cref="ByteVector" /> object containing the data to  insert into the file. </param>
        /// <param name="start"> A <see cref="long" /> value specifying at which point to insert the data. </param>
        /// <param name="replace"> A <see cref="long" /> value specifying the number of bytes to replace.
        /// Typically this is the original size of the data block so that a new block will replace the old one. </param>
        Task InsertAsync(ByteVector data, long start, long replace = 0);

        /// <summary> Inserts a specified block-size into the file represented by the current instance at a specified location.
        /// Former data at this location is not overwritten and may then contain random content. </summary>
        /// <param name="size"> A <see cref="long" /> value specifying the size in bytes of the block to be inserted (reserved). </param>
        /// <param name="start"> A <see cref="long" /> value specifying at which point to  insert the data. </param>
        /// <remarks> This method is useful to reserve some space in the file.
        /// To insert or replace defined data blocks, use <see cref="InsertAsync(ByteVector,long,long)"/>  </remarks>
        Task InsertAsync(long size, long start);
        
        /// <summary> Inserts a specified block into the file represented by the current instance at a specified location. </summary>
        /// <param name="data">A <see cref="ByteVector" /> object containing the data to insert into the file.
        /// if null, no data is written to the file and the block is just inserted without overwriting the former data at the given location. </param>
        /// <param name="size">A <see cref="long" /> value specifying the size of the block to be inserted. </param>
        /// <param name="start">A <see cref="long" /> value specifying at which point to insert the data. </param>
        /// <param name="replace">A <see cref="long" /> value specifying the number of bytes to replace.
        /// Typically this is the original size of the data block so that a new block will replace the old one. </param>
        /// <remarks>This method inserts a new block of data into the file. To replace an existing block,
        /// ie. replacing an existing tag with a new one of different size, use <see cref="InsertAsync(ByteVector,long,long)" />. </remarks>
        Task InsertAsync(ByteVector? data, long size, long start, long replace);

        /// <summary> Removes a specified block of data from the file represented by the current instance. </summary>
        /// <param name="start">A <see cref="long" /> value specifying at which point to remove data. </param>
        /// <param name="length">A <see cref="long" /> value specifying the number of bytes to remove. </param>
        Task RemoveBlockAsync(long start, long length);

        /// <summary>Resized the current instance to a specified number of bytes. </summary>
        /// <param name="length"> A <see cref="long" /> value specifying the number of bytes to resize the file to. </param>
        void Truncate(long length);

        /// <summary> Seeks the read/write pointer to a specified offset in the current instance, relative to a specified origin. </summary>
        /// <param name="offset"> A <see cref="long" /> value indicating the byte offset to seek to. </param>
        /// <param name="origin"> A <see cref="SeekOrigin" /> value specifying an origin to seek from.</param>
        void Seek(long offset, SeekOrigin origin);

        /// <summary> Total size of File in bytes. </summary>
        long Length { get; }
    
        /// <summary> The buffer size to use when reading large blocks of data in the <see cref="IFile" /> class. </summary>
        uint BufferSize { get; set; }

        /// <summary> Is file closed </summary>
        bool IsClosed => Mode == AccessMode.Closed;
    }
}