using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 TrackHeadBox.
/// <para>This box specifies the characteristics of a single track. Exactly one <see cref="TrackHeaderBox"/>  is contained in track.</para>
/// <para>In the absence of an edit list, the presentation of a track starts at the beginning of the overall
/// presentation. An ‘empty’ edit is used to offset the start time of a track</para>
/// <para>The tracks marked with the <see cref="TrackHeaderFlags.InMovie"/> flag set to 1 are those that are intended by the file writer
/// for direct presentation. Thus a track that is used as input to another track — either before or after
/// decoding — but that is not presented by itself — should have the <see cref="TrackHeaderFlags.InMovie"/> flag set to 0. Tracks
/// having the <see cref="TrackHeaderFlags.InMovie"/> flag set are candidates for playback, regardless of whether they are media
/// tracks or reception hint tracks. A track may be used as input and also have the <see cref="TrackHeaderFlags.InMovie"/> flag set to 1.</para>
/// <para>If a track is a member of a group that presents alternatives for presentation, either by using the
/// <see cref="AlternateGroup"/> field in the track header, or by using the <see cref="TrackGroupBox"/>  with a group type that defines
/// alternatives, then either only the preferred or default choice track should have the <see cref="TrackHeaderFlags.InMovie"/> flag
/// set to 1, or all tracks should have the <see cref="TrackHeaderFlags.InMovie"/> flag set to 1 (if no default is to be indicated).</para>
/// <para>If an 'altr' entity group contains entities for playing, <see cref="TrackHeaderFlags.InMovie"/> should be equal to 1 in all the tracks of the entity group.</para>
/// <para>If in a presentation no tracks have <see cref="TrackHeaderFlags.InMovie"/> set, and therefore it appears that there is nothing to present, then
/// a player may enable a track from each group for presentation; derived specifications can give further guidance and/or restrictions.</para>
/// <para>Tracks that are marked as not enabled (<see cref="TrackHeaderFlags.Enabled"/> set to 0) shall be ignored and treated as if not present.
/// Application environments may offer a way to enable/disable tracks at run-time and dynamically alter the state of this flag.</para> </summary>
[HasBoxFactory("tkhd")]
public class TrackHeaderBox : FullBoxWithData, IBoxWithMovieHeaderScalableProperties
{
    private TrackHeaderBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            int offset;
            if (Version == 1)
            {
                CreationTime = value[..8].ToDateTime();
                ModificationTime = value[8..16].ToDateTime();
                TrackId = value[16..20].ToUInt();
                Duration = value[24..32].ToTimeSpan(Header.Timescale);
                offset = 32;
            }
            else
            {
                CreationTime = value[..4].ToDateTime();
                ModificationTime = value[4..8].ToDateTime();
                TrackId = value[8..12].ToUInt();
                Duration = value[16..20].ToTimeSpan(Header.Timescale);
                offset = 20;
            }

            Layer = value.Mid(offset + 8, 2).ToShort();
            AlternateGroup = value.Mid(offset + 10, 2).ToShort();
            Volume = value.Mid(offset + 12, 2);
            TransformationMatrix = value.Mid(offset + 16, 36);
            Width = value.Mid(offset + 52, 4).ToInt();
            Height = value.Mid(offset + 56, 4).ToInt();
            Debug.Assert(Data == value);
        } 
    }
    
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        if (Version == 1)
        {
            builder.AddLong(CreationTime)
                .AddLong(ModificationTime)
                .Add(TrackId).Clear(4)
                .AddLong(Duration, Header.Timescale);
        }
        else
        {
            builder.Add(CreationTime)
                .Add(ModificationTime)
                .Add(TrackId).Clear(4)
                .Add(Duration, Header.Timescale);
        } 
        return builder.Clear(8).Add(Layer)
            .Add(AlternateGroup).Add(Volume)
            .Clear(2)
            .Add(TransformationMatrix)
            .Add(Width)
            .Add(Height);
    }

    /// <summary> Flags indicating the track properties.  </summary>
    [Flags]
    public enum TrackHeaderFlags
    {
        /// <summary> The value 1 indicates that the track is enabled. A disabled
        /// track (when the value of this flag is zero) is treated as if it were not present. </summary>
        Enabled = 0x000001,
        /// <summary> The value 1 indicates that the track, or one of its alternatives (if any) forms
        /// a direct part of the presentation. The value 0 indicates that the track  does not represent a direct part of the presentation. </summary>
        InMovie = 0x000002,
        /// <summary> This flag currently has no assigned meaning, and the value should be ignored by readers.
        /// In the absence of further guidance (e.g. from derived specifications), the same value as
        /// for <see cref="InMovie"/> should be written. </summary>
        InPreview = 0x000004,
        /// <summary> The value 1 indicates that the width and height fields are not expressed in pixel units.
        /// The values have the same units but these units are not specified. The values are only an indication
        /// of the desired aspect ratio. If the aspect ratios of this track and other related tracks are not identical, then the respective positioning of
        /// the tracks is undefined, possibly defined by external contexts. </summary>
        SizeIsAspectRatio = 0x000008,
    }

    /// <inheritdoc cref="Flags"/>
    public new TrackHeaderFlags Flags { get => (TrackHeaderFlags)base.Flags; set => base.Flags = (uint)value; }
    /// <summary> The value 1 indicates that the track is enabled. A disabled
    /// track (when the value of this flag is zero) is treated as if it were not present. </summary>
    public bool IsEnabled { get => Flags.HasFlag(TrackHeaderFlags.Enabled); set => Flags = Flags.SetFlag(TrackHeaderFlags.Enabled, value); }
    /// <summary> The value 1 indicates that the track, or one of its alternatives (if any) forms
    ///  a direct part of the presentation. The value 0 indicates that the track  does not represent a direct part of the presentation. </summary>
    public bool IsInMovie { get => Flags.HasFlag(TrackHeaderFlags.InMovie); set => Flags = Flags.SetFlag(TrackHeaderFlags.InMovie, value); }
    /// <summary> This flag currently has no assigned meaning, and the value should be ignored by readers.
    /// In the absence of further guidance (e.g. from derived specifications), the same value as
    /// for track_in_movie should be written. </summary>
    public bool IsInPreview { get => Flags.HasFlag(TrackHeaderFlags.InPreview); set => Flags = Flags.SetFlag(TrackHeaderFlags.InPreview, value); }
    /// <summary> The value 1 indicates that the width and height fields are not expressed in pixel units.
    /// The values have the same units but these units are not specified. The values are only an indication
    /// of the desired aspect ratio. If the aspect ratios of this track and other related tracks are not identical, then the respective positioning of
    /// the tracks is undefined, possibly defined by external contexts. </summary>
    public bool IsSizeIsAspectRatio { get => Flags.HasFlag(TrackHeaderFlags.SizeIsAspectRatio); set => Flags = Flags.SetFlag(TrackHeaderFlags.SizeIsAspectRatio, value); }
    
    /// <summary> Creation time of this track. </summary>
    public DateTime CreationTime { get; set; }
    /// <summary> Most recent time the track was modified. </summary>
    public DateTime ModificationTime { get; set; }
    /// <summary> An integer that uniquely identifies this track over the entire life-time of this presentation;
    /// <see cref="TrackId"/> are never re-used and cannot be zero </summary>
    public uint TrackId { get; set; }
    
    /// <summary> an integer that indicates the duration of this track (in the timescale indicated in the
    /// <see cref="MovieHeaderBox"/> ) This duration field may be indefinite (all 1s) when either there is no edit list
    /// and the MediaHeaderBox duration is indefinite (i.e. all 1s), or when an indefinitely repeated edit
    /// list is desired. If there is no edit list and the duration is not indefinite, then the duration shall be equal
    /// to the media duration given in the <see cref="MediaHeaderBox"/>, converted into the timescale in the <see cref="MovieHeaderBox"/> .
    /// Otherwise the value of this field is equal to the sum of the durations of all of the track’s edits (possibly including repetitions). </summary>
    public TimeSpan Duration { get; set; }
    /// <summary> Front-to-back ordering of video tracks; tracks with lower numbers are closer to the viewer. 0 is the normal value,
    /// and -1 would be in front of track 0, and so on. </summary>
    public short Layer { get; set; }
    
    /// <summary> is an integer that specifies a group or collection of tracks. If this field is 0 there is
    /// no information on possible relations to other tracks. If this field is not 0, it should be the same for
    /// tracks that contain alternate data for one another and different for tracks belonging to different
    /// such groups. Only one track within an alternate group should be played or streamed at any one time,
    /// and shall be distinguishable from other tracks in the group via attributes such as bitrate,codec,
    /// language, packet size etc. A group may have only one member. </summary>
    public short AlternateGroup { get; set; }
    
    /// <summary> The track's relative audio volume. Full volume is 1.0 (0x0100) and is the normal value.
    /// Its value is irrelevant for a purely visual track. Tracks may be composed by combining them according
    /// to their volume, and then using the overall <see cref="MovieHeaderBox"/>
    /// volume setting; or more complex audio composition (e.g. MPEG-4 BIFS) may be used. </summary>
    public FixedPoint8_8 Volume { get; set; }
    
    /// <summary> Transformation matrix for the video; (u,v,w) are restricted here to (0,0,1), hex(0,0,0x40000000) </summary>
    public TransformationMatrix TransformationMatrix { get; set; } = new();
    
    /// <summary> The width of the visual presentation.
    /// <para>For text and subtitle tracks, they may, depending on the coding format, describe the suggested size of the rendering area.
    /// For such tracks, the value 0x0 may also be used to indicate that the data maybe rendered at any size,
    /// that no preferred size has been indicated and that the actual size may be determined by the external context
    /// or by reusing the width and height of another track. For those tracks, the flag <see cref="TrackHeaderFlags.SizeIsAspectRatio"/> may also be used.</para>
    /// <para>For non-visual tracks (e.g. audio), they should be set to zero.</para>
    /// <para>For all other tracks, they specify the track's visual presentation size. These need not be the same as the
    /// pixel dimensions of the images, which is documented in the sample description(s); all images in the
    /// sequence are scaled to this size, before any overall transformation of the track represented by the
    /// matrix. The pixel dimensions of the images are the default values.</para> </summary>
    public UFixedPoint16_16 Width { get; set; }
    
    /// <summary> The height of the visual presentation.
    /// <para>For text and subtitle tracks, they may, depending on the coding format, describe the suggested size of the rendering area.
    /// For such tracks, the value 0x0 may also be used to indicate that the data maybe rendered at any size,
    /// that no preferred size has been indicated and that the actual size may be determined by the external context
    /// or by reusing the width and height of another track. For those tracks, the flag <see cref="TrackHeaderFlags.SizeIsAspectRatio"/> may also be used.</para>
    /// <para>For non-visual tracks (e.g. audio), they should be set to zero.</para>
    /// <para>For all other tracks, they specify the track's visual presentation size. These need not be the same as the
    /// pixel dimensions of the images, which is documented in the sample description(s); all images in the
    /// sequence are scaled to this size, before any overall transformation of the track represented by the
    /// matrix. The pixel dimensions of the images are the default values.</para> </summary>
    public UFixedPoint16_16 Height { get; set; }

    /// <inheritdoc />
    public void ChangeTimescale(uint oldTimeScale, uint newTimescale)
    {
        Duration = Duration * oldTimeScale / newTimescale;
    }
    
    /// <inheritdoc />
    public override ulong ActualDataSize => Version == 1 ? 92UL : 80UL;
}