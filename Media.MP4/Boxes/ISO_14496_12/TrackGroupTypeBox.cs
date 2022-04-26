using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackGroupBox.
/// <para>This box enables indication of groups of tracks, where each group shares a particular characteristic or
/// the tracks within a group have a particular relationship. The box contains zero or more boxes, and the
/// particular characteristic or the relationship is indicated by the box type of the contained boxes. The
/// contained boxes include an identifier, which can be used to conclude the tracks belonging to the same
/// track group. The tracks that contain the same type of a contained box within the <see cref="TrackGroupBox" />  and
/// have the same identifier value within these contained boxes belong to the same track group..</para>
/// <para>Track groups shall not be used to indicate dependency relationships between tracks. Instead, the
/// <see cref="TrackReferenceBox" />  is used for such purposes.</para>
/// <para>(flags &amp; 1) equal to 1 in a <see cref="TrackGroupTypeBox" />  of a particular track_group_type indicates that track_group_id
/// in that <see cref="TrackGroupTypeBox" /> is not equal to any track_ID value and is not equal to track_group_id
/// of any other <see cref="TrackGroupTypeBox" /> with a different track_group_type. When (flags &amp; 1) is equal to 1 in
/// a TrackGroupTypeBox with particular values of track_group_type and track_group_id, (flags &amp; 1) shall
/// be equal to 1 in all <see cref="TrackGroupTypeBox" />es of the same values of track_group_type and track_group_id,
/// respectively.</para>  </summary>
public abstract class TrackGroupTypeBox : BoxWithData
{
    /// <summary> Constructs and initializes a new instance of <see cref="TrackGroupTypeBox" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="HandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected TrackGroupTypeBox (BoxHeader header,  HandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            TrackGroupId = value[..4].ToUInt();
            Debug.Assert(Data == value);
        } 
    }
    
    /// <summary>  The pair of <see cref="TrackGroupId"/> and <see cref="BoxHeader.BoxType"/> (of this box) identifies a track group within the file. The tracks
    /// that contain a particular TrackGroupTypeBox having the same value of <see cref="TrackGroupId"/> and <see cref="BoxHeader.BoxType"/> (of this box) belong to the same track group. </summary>
    public uint TrackGroupId { get; set; }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(TrackGroupId);
        return builder;
    }
}