﻿using System.Reflection;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes;

/// <summary> This abstract class provides a generic implementation of a ISO/IEC 14496-12 box.
/// Object-oriented building block defined by a unique type identifier and length.</summary>
public abstract class Box 
{
    /// <summary> Constructs and initializes a new instance of <see cref="Box" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="HandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected Box(BoxHeader header, HandlerBox? handler = null)
    {
        Header = header;
        Handler = handler;
        header.Box = this;
    }

    /// <summary> Initiate box from <see cref="MP4"/> file. </summary>
    /// <param name="file">File from which will be initiated.</param>
    /// <returns>This object to allow chaining.</returns>
    protected abstract Task<Box> InitAsync(MP4 file);
    
    /// <summary>Factory for creating and initializing the boxes.</summary>
    /// <param name="header">Header of the box.</param>
    /// <param name="file">File from witch box will be initialized.</param>
    /// <param name="handler">Iso handler for better understanding of box.</param>
    /// <typeparam name="T">Type of the box</typeparam>
    /// <returns>Newly created box.</returns>
    public static async Task<Box> CreateAsync<T>(BoxHeader header, MP4 file, HandlerBox? handler)
        where T : Box
    {
        return await BoxActivator<T>.Create(  header, handler).InitAsync(file);
    }

    private static class BoxActivator<T> where T : Box
    {
        private static readonly ConstructorInfo Constructor = typeof(T)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
                CallingConventions.Standard, new[] {typeof(BoxHeader), typeof(HandlerBox)}, null)!;
        public static Box Create(BoxHeader header, HandlerBox? handler)
        {
            return (Constructor.Invoke(new object?[] {header, handler} ) as Box)!;
        }
    }

    /// <summary> Gets the MPEG-4 box type of the current instance.</summary>
    public BoxType Type => Header.BoxType;

    /// <summary> Gets the total size of the current instance as it last appeared on disk. </summary>
    /// <value> A <see cref="ulong" /> value containing the total size of the current instance as it last appeared on disk. </value>
    public ulong Size => Header.TotalBoxSize;

    /// <summary> Gets the handler box that applies to the current instance. </summary>
    /// <value> A <see cref="HandlerBox" /> object containing the handler that applies
    /// to the current instance, or <see langword="null" /> if no handler applies. </value>
    // ReSharper disable once MemberCanBeProtected.Global
    public HandlerBox? Handler { get; set; }
    
    /// <summary> Gets the size of the data contained in the current
    ///    instance, minus the size of any box specific headers. </summary>
    /// <value> A <see cref="ulong" /> value containing the size of
    ///    the data contained in the current instance. </value>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual ulong DataSize => Header.DataSize;

    /// <summary> Gets the position of the data contained in the current
    /// instance, after any box specific headers. </summary>
    /// <value>  A <see cref="ulong" /> value containing the position of
    ///    the data contained in the current instance. </value>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual ulong DataPosition => Header.DataPosition;

    /// <summary> Gets the header of the current instance. </summary>
    /// <value> A <see cref="BoxHeader" /> object containing the header of the current instance. </value>
    // ReSharper disable once MemberCanBeProtected.Global
    public BoxHeader Header { get; }
    
    /// <summary> Renders the current instance as a byte vector to byte vector. </summary>
    /// <returns>Builder instance for chaining.</returns>
    public IByteVectorBuilder Render()
    {
        return Render(new ByteVectorBuilder((int)ActualSize));
    }

    /// <summary> Renders the current instance as a byte vector to byte vector builder. </summary>
    /// <param name="builder">Builder to store current instance.</param>
    /// <returns>Builder instance for chaining.</returns>
    public virtual IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        return Header.Render(builder);
    }
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Size"/>, this value is calculated after every change of data. </summary>
    public abstract ulong ActualSize { get; }

    /// <summary> Debug string use to print during debug. </summary>
    /// <param name="level">Level on which this element has been found</param>
    /// <returns>Debug string</returns>
    public virtual string DebugDisplay(int level)
        => $"{Header.BoxType.Header.ToString().Intend(level, true)} - {GetType().Name}";
    /// <summary> Location relative to start of the box where data is starting. </summary>
    public uint RelativeDataPosition => (uint)(DataPosition - (ulong)Header.Position);
}