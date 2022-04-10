using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 TrackReferenceTypeBox.
/// <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
/// enclosing track has one of more references of that type. Each reference type shall occur at most once.
/// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
/// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
/// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
/// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
/// sized to fill the reference type box.</para> </summary>  
public abstract class TrackReferenceTypeBox : BoxWithData
{
    /// <summary> Constructs and initializes a new instance of <see cref="TrackReferenceTypeBox" />
    /// with a specified header and handler.</summary>
    /// <param name="header"> A <see cref="BoxHeader" /> object describing the new  instance. </param>
    /// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies
    /// to the new instance, or <see langword="null" /> if no handler applies.</param>
    protected TrackReferenceTypeBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            TrackIds.Clear();
            for (var i = 0; i < value.Count; i += 4)
            {
                TrackIds.Add(value.Mid(i, 4).ToUInt());
            }
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>An array of integers providing the track identifiers of the referenced tracks or <see cref="TrackGroupTypeBox.TrackGroupId"/>
    /// values of the referenced track groups. Each value track_IDs[i], where i is a valid index
    /// to the <see cref="TrackIds"/>[] array, is an integer that provides a reference from the containing track to the
    /// track with <see cref="TrackHeaderBox.TrackId"/> equal to <see cref="TrackIds"/>[i] or to the track group with both track_group_id equal
    /// to <see cref="TrackIds"/>[i] and (flags &amp; 1) of TrackGroupTypeBox equal to 1. When a <see cref="TrackGroupTypeBox.TrackGroupId"/> value
    /// is referenced, the track reference applies to each track of the referenced track group individually
    /// unless stated otherwise in the semantics of particular track reference types. The value 0 shall not
    /// be present. In the array there shall be no duplicated value; however, a track_ID may appear in the
    /// array and also be a member of one or more track groups for which the <see cref="TrackGroupTypeBox.TrackGroupId"/>s appear
    /// in the array. This means that in forming the list of tracks, after replacing t<see cref="TrackGroupTypeBox.TrackGroupId"/>s by the
    /// track_IDs of the tracks in those groups, there might be duplicate track_IDs. A <see cref="TrackGroupTypeBox.TrackGroupId"/> shall
    /// not be used when the semantics of the reference requires that the reference be to a single track.</summary>
    public IList<uint> TrackIds { get; set; } = new List<uint>();
    
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        TrackIds.ForEach(c => builder.Add(c));
        return builder;
    }
    
    /// <inheritdoc />
    public override ulong ActualDataSize => (ulong) (TrackIds.Count * 4);
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} TrackIds: {string.Join(", ", TrackIds)}";
}