using System.Collections.Concurrent;
using System.Text;
using Ardalis.SmartEnum;
using Vipl.Base;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Vipl.Media.MP4;

/// <summary> <see cref="BoxType" /> provides references to different box types used by the library. </summary>
public sealed class BoxType : SmartEnum<BoxType>
{
    private static readonly IDictionary<ByteVector, BoxType> All = new ConcurrentDictionary<ByteVector, BoxType>();
    
   
    /// <summary> Four bytes header found in start of box. Size of string must be 3 or 4
    /// characters and every character must be between 0x20 and 0x7E included.</summary>
    public ByteVector Header { get; }
    private BoxType(string name, int value) : this(CreateFourCharacterCode((ByteVector)name), name, value) { }
    
    private BoxType(ByteVector header, string name, int value) : base(name, value)
    {
        if (header.Count != 4)
        {
            throw new NotSupportedException($"Box type {header} is not supported.");
        }
        Header = header;
        All.Add(Header, this);
    }
    /// <summary> /// The FileTypeBox contains a major brand and minor version number.
    /// It also contains a list of compatible brands.  The compatible brands are used to identify compatible renderers. </summary>
    public static readonly BoxType FileType = new("ftyp", 0);
    
    /// <summary>This box warns readers that there might be MovieFragmentBoxes in this file. To know of all samples
    /// in the tracks, these MovieFragmentBoxes must be found and scanned in order, and their information
    /// logically added to that found in the Movie Box.</summary>
    public static readonly BoxType MovieBox = new("moov", 1);
    
    /// <summary><para>This box contains the media data. In video tracks, this box would contain video frames.
    /// A presentation may contain zero or more MediaDataBoxes. The actual media data follows the type field;
    /// its structure is described by the structure-data</para> </summary>
    public static readonly BoxType MediaData = new("mdat", 2);
    
    /// <summary> <para>The contents of a <see cref="FreeSpaceBox"/>  are irrelevant and may be ignored, or the box deleted, without affecting
    /// the presentation. (Care should be exercised when deleting the box, as this may invalidate offsets used
    /// to refer to other data, unless this box is after all the media data).</para> </summary>
    public static readonly BoxType Skip = new("skip", 3);
    
    /// <summary> <para>The contents of a <see cref="FreeSpaceBox"/>  are irrelevant and may be ignored, or the box deleted, without affecting
    /// the presentation. (Care should be exercised when deleting the box, as this may invalidate offsets used
    /// to refer to other data, unless this box is after all the media data).</para> </summary>
    public static readonly BoxType Free = new("free", 4);
    
    /// <summary>This box defines overall information which is media-independent, and relevant to the entire
    /// presentation considered as a whole.</summary>
    public static readonly BoxType MovieHeader = new("mvhd", 5);
    
    /// <summary><para>This is a container box for a single track of a presentation. A presentation consists of one or more tracks.
    /// Each track carries its own temporal and spatial information. Each track will contain its associated <see cref="MediaDataBox"/> .</para>
    /// <para>Tracks are used for a number of purposes, including: (a) to contain media data (media tracks) and (b) to
    /// contain packetization information for streaming protocols (hint tracks).</para>
    /// <para>There shall be at least one media track within a MovieBox, and all the media tracks that contributed to
    /// the hint tracks shall remain in the file, even if the media data within them is not referenced by the hint
    /// tracks; after deleting all hint tracks, the entire un-hinted presentation shall remain.</para> </summary>
    public static readonly BoxType Track = new("trak", 6);
    
    /// <summary>  <para>This box specifies the characteristics of a single track. Exactly one <see cref="TrackHeaderBox"/>  is contained in track.</para>
    /// <para>In the absence of an edit list, the presentation of a track starts at the beginning of the overall
    /// presentation. An ‘empty’ edit is used to offset the start time of a track</para>
    /// <para>The tracks marked with the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag set to 1 are those that are intended by the file writer
    /// for direct presentation. Thus a track that is used as input to another track — either before or after
    /// decoding — but that is not presented by itself — should have the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag set to 0. Tracks
    /// having the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag set are candidates for playback, regardless of whether they are media
    /// tracks or reception hint tracks. A track may be used as input and also have the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag set to 1.</para>
    /// <para>If a track is a member of a group that presents alternatives for presentation, either by using the
    /// <see cref="TrackHeaderBox.AlternateGroup"/> field in the track header, or by using the <see cref="TrackGroupBox"/>  with a group type that defines
    /// alternatives, then either only the preferred or default choice track should have the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag
    /// set to 1, or all tracks should have the <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> flag set to 1 (if no default is to be indicated).</para>
    /// <para>If an 'altr' entity group contains entities for playing, <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> should be equal to 1 in all the tracks of the entity group.</para>
    /// <para>If in a presentation no tracks have <see cref="TrackHeaderBox.TrackHeaderFlags.InMovie"/> set, and therefore it appears that there is nothing to present, then
    /// a player may enable a track from each group for presentation; derived specifications can give further guidance and/or restrictions.</para>
    /// <para>Tracks that are marked as not enabled (<see cref="TrackHeaderBox.TrackHeaderFlags.Enabled"/> set to 0) shall be ignored and treated as if not present.
    /// Application environments may offer a way to enable/disable tracks at run-time and dynamically alter the state of this flag.</para> </summary>
    public static readonly BoxType TrackHeader = new("tkhd", 7);
    
    /// <summary><para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>  </summary>
    public static readonly BoxType TrackReference = new("tref", 8);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>The referenced track(s) contain the original media for this hint track.</para></summary>
    public static readonly BoxType HintTrackReference = new("hint", 9);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>Links a descriptive or metadata track to the content which it describes.</para></summary>
    public static readonly BoxType MetadataTrackReference = new("cdsc", 10);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track uses fonts carried/defined in the referenced track.</para></summary>
    public static readonly BoxType FontTrackReference = new("font", 11);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>Indicates that the referenced track(s) may contain media data required for decoding
    /// of the track containing the track reference, i.e., it should only be used if the referenced
    /// hint track is used. The referenced tracks shall be hint tracks. The 'hind'
    /// dependency can, for example, be used for indicating the dependencies between
    /// hint tracks documenting layered IP multicast over RTP.</para></summary>
    public static readonly BoxType DecodingTrackReference = new("hind", 12);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track contains auxiliary depth video information for the referenced video track.</para></summary>
    public static readonly BoxType DeptVideoInformationTrackReference = new("vdep", 13);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track contains auxiliary parallax video information for the referenced video track.</para></summary>
    public static readonly BoxType ParallaxVideoInformationTrackReference = new("vplx", 14);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track contains subtitle, timed text or overlay graphical information for the referenced
    /// track or any track in the alternate group to which the track belongs, if any.</para></summary>
    public static readonly BoxType SubtitleTrackReference = new("subt", 15);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track contains thumbnail images for the referenced track. A thumbnail track
    /// shall not be linked to another thumbnail track with the 'thmb' item reference.</para></summary>
    public static readonly BoxType ThumbnailTrackReference = new("thmb", 16);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>This track contains auxiliary media for the indicated track (e.g. depth map or alpha plane for video).</para></summary>
    public static readonly BoxType AuxiliaryMediaTrackReference = new("auxl", 17);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>Describes the referenced media tracks and track groups collectively; the 'cdtg'
    /// track reference shall only be present in timed metadata tracks.</para></summary>
    public static readonly BoxType GroupTrackReference = new("cdtg", 18);
    
    /// <summary>  <para>This box includes a set of <see cref="TrackReferenceTypeBox"/>es, each of which indicates, by its type, that the
    /// enclosing track has one of more references of that type. Each reference type shall occur at most once.
    /// Within each <see cref="TrackReferenceTypeBox"/>es there is an array of track IDs; within a given array, a given value
    /// shall occur at most once. Other structures in the file formats index through these arrays; index values start at 1.</para>
    /// <para>Exactly one <see cref="TrackReferenceBox"/> can be contained within the <see cref="TrackBox"/> .</para>
    /// <para>If this box is not present, the track is not referencing any other track in any way. The reference array is
    /// sized to fill the reference type box.</para>
    /// <para>Links a shadow sync track to a main track.</para></summary>
    public static readonly BoxType ShadowSyncTrackReference = new("shsc", 19);
    
    /// <summary>The media declaration container contains all the objects that declare information about the media data within a track.</summary>
    public static readonly BoxType Media = new("mdia", 20);
    
    /// <summary> <para>The media header declares overall information that is media-independent, and relevant to
    /// characteristics of the media in a track.</para>  </summary>
    public static readonly BoxType MediaHeader = new("mdhd", 25);
    
    /// <summary> <para>This box within a <see cref="MediaDataBox"/>  declares media type of the track, and thus the process by which the media-
    /// data in the track is presented. For example, a format for which the decoder delivers video would be
    /// stored in a video track, identified by being handled by a video handler. The documentation of the
    /// storage of a media format identifies the media type which that format uses.</para>
    /// <para> This box when present within a <see cref="MetaBox"/> , declares the structure or format of the <see cref="MetaBox"/> contents.</para>
    /// <para> There is a general handler for metadata streams of any type; the specific format is identified by the
    /// sample entry, as for video or audio, for example.</para> </summary>
    public static readonly BoxType Handler = new("hdlr", 26);
    
    /// <summary> <para> An EditBox maps the presentation timeline to the media timeline as it is stored
    /// in the file. The EditBox is a container for the edit lists. </para>
    /// <para> The EditBox is optional. In the absence of this box, there is an implicit one-to-one
    /// mapping of these timelines, and the presentation of a track starts at the beginning of the
    /// presentation. An empty edit is used to offset the start time of a track.</para>  </summary>
    public static readonly BoxType Edit = new("edts", 50);
    
    /// <summary> <para>The length of the whole track in an EditListBox might be the overall duration of the
    /// whole movie excluding fragments of a fragment movie. Since edit lists cannot occur in movie fragments, there is
    /// an implied edit at the end of the current explicit or implied edit list, that inserts the new media material and the
    /// presentation of fragments starts after the presentation of the movie in the MovieBox.</para> </summary>
    public static readonly BoxType EditList = new("elst", 51);
    
    /// <summary> <para>A common base structure is used to contain general untimed metadata. This structure is called the
    /// <see cref="MetaBox"/> as it was originally designed to carry metadata, i.e. data that is annotating other data. However,
    /// it is now used for a variety of purposes including the carriage of data that is not annotating other data,
    /// especially when present at ‘file level’.</para>
    /// <para>The MetaBox is required to contain a HandlerBox indicating the structure or format of the MetaBox contents.</para> </summary>
    public static readonly BoxType Meta = new("meta", 100);
    
    /// <summary><para>The sample description table gives detailed information about the coding type used, and any
    /// initialization information needed for that coding. The syntax of the sample entry used is determined by
    /// both the format field and the media handler type.</para>
    /// <para>The information stored in the <see cref="SampleDescriptionBox"/>  after the entry-count is both track-type specific
    /// as documented here, and can also have variants within a track type (e.g. different codings may use
    /// different specific information after some common fields, even within a video track).</para>
    /// <para>Which type of sample entry form is used is determined by the media handler, using a suitable form,
    /// such as one defined in Clause 12, or defined in a derived specification, or registration.</para>
    /// <para>Multiple descriptions may be used within a track.</para> </summary>
    public static readonly BoxType SampleDescription = new("stsd", 130);
    
    /// <summary><para>An optional <see cref="BitRateBox"/> may be present in any <see cref="SampleEntryBox"/> to signal
    /// the bit rate information of a stream. This can be used for buffer configuration.</para></summary>
    public static readonly BoxType BitRate = new("btrt", 131);

    /// <summary>Chapter or scene list. Usually references a text track.</summary>
    public static readonly BoxType ChapterTrackReference = new("chap", 1000);
    
    /// <summary> </summary>
    public static readonly BoxType Aart = new("aART", 500);
    /// <summary> </summary>
    public static readonly BoxType Alb = new("alb", 501);
    /// <summary> </summary>
    public static readonly BoxType Art = new("ART", 502);
    /// <summary> </summary>
    public static readonly BoxType Cmt = new("cmt", 503);
    /// <summary> </summary>
    public static readonly BoxType Cond = new("cond", 504);
    /// <summary> </summary>
    public static readonly BoxType Covr = new("covr", 505);
    /// <summary> </summary>
    public static readonly BoxType IsoChunkLargeOffset = new("co64", 506);
    /// <summary> </summary>
    public static readonly BoxType Cpil = new("cpil", 507);
    /// <summary> </summary>
    public static readonly BoxType Cprt = new("cprt", 508);
    /// <summary> </summary>
    public static readonly BoxType AppleData = new("data", 509);
    /// <summary> </summary>
    public static readonly BoxType Day = new("day", 510);
    /// <summary> </summary>
    public static readonly BoxType Desc = new("desc", 511);
    /// <summary> </summary>
    public static readonly BoxType Disk = new("disk", 512);
    /// <summary> </summary>
    public static readonly BoxType Dtag = new("dtag", 513);
    /// <summary> </summary>
    public static readonly BoxType Esds = new("esds", 514);
    /// <summary> </summary>
    public static readonly BoxType AppleItemList = new("ilst", 515);
    /// <summary> </summary>
    public static readonly BoxType Gen = new("gen", 517);
    /// <summary> </summary>
    public static readonly BoxType Gnre = new("gnre", 518);
    /// <summary> </summary>
    public static readonly BoxType Grp = new("grp", 519);

    /// <summary> </summary>
    public static readonly BoxType Lyr = new("lyr", 521);
    


    /// <summary> </summary>
    public static readonly BoxType Mean = new("mean", 525);
    /// <summary> </summary>
    public static readonly BoxType Minf = new("minf", 526);
    
    /// <summary> </summary>
    public static readonly BoxType Nam = new("nam", 529);
    /// <summary> </summary>
    public new static readonly BoxType Name = new("name", 530);
    /// <summary> </summary>
    public static readonly BoxType Role = new("role", 531);
    /// <summary> </summary>
    public static readonly BoxType Soaa = new("soaa", 533); // Album Artist Sort
    /// <summary> </summary>
    public static readonly BoxType Soar = new("soar", 534); // Performer Sort
    /// <summary> </summary>
    public static readonly BoxType Soco = new("soco", 535); // Composer Sort
    /// <summary> </summary>
    public static readonly BoxType Sonm = new("sonm", 536); // Track Title Sort
    /// <summary> </summary>
    public static readonly BoxType Soal = new("soal", 537); // Album Title Sort
    /// <summary> </summary>
    public static readonly BoxType IsoSampleTable = new("stbl", 538);
    /// <summary> </summary>
    public static readonly BoxType IsoChunkOffset = new("stco", 539);

    /// <summary> </summary>
    public static readonly BoxType Subt = new("Subt", 541);
    /// <summary> </summary>
    public static readonly BoxType Text = new("text", 542);
    /// <summary> </summary>
    public static readonly BoxType Tmpo = new("tmpo", 543);
    /// <summary> </summary>
    public static readonly BoxType Trkn = new("trkn", 545);
    /// <summary> </summary>
    public static readonly BoxType IsoUserData = new("udta", 546);
    /// <summary> </summary>
    public static readonly BoxType Url = new("url", 547);
    /// <summary> </summary>
    public static readonly BoxType Uuid = new("uuid", 548);
    /// <summary> </summary>
    public static readonly BoxType Wrt = new("wrt", 549);
    /// <summary> </summary>
    public static readonly BoxType Dash = new("----", 550);
    /// <summary> </summary>
    // Handler types.
    public static readonly BoxType SoundIsoHandler = new("soun", 551);
    /// <summary> </summary>
    public static readonly BoxType VideoIsoHandler = new("vide", 552);
    /// <summary> </summary>
    // Another handler typeBoxType audio 
    public static readonly BoxType Alis = new("alis", 553);
    /// <summary> </summary>
    public static readonly BoxType Chapterype = new("chpl", 556);
    
    private static int ValueCounter { get; set; } = 557;
    /// <summary> Convert box type to <see cref="ByteVector"/> representation. </summary>
    /// <param name="type"><see cref="BoxType"/> representing type of the box.</param>
    /// <returns><see cref="ByteVector"/> representation of type.</returns>
    public static implicit operator ByteVector(BoxType type) => type.Header;
    /// <summary> Get box type from <see cref="ByteVector"/>. </summary>
    /// <param name="type"><see cref="ByteVector"/> representing type of the box. Size of string
    /// must be 3 or 4 characters and every character must be between 0x20 or 0x7F included.</param>
    /// <returns>Smart enum with type of the box.</returns>
    /// <exception cref="ArgumentException">If byte vector does not have size of 3 or 4 or if
    /// any of byte does not have value between 0x20 and 0x7E included.</exception>
    public static implicit operator BoxType(ByteVector type) => FromByteVector(type);
    /// <summary> Get box type from string name. </summary>
    /// <param name="type">string representing type of the box.Size of string
    /// must be 4 characters and every character must be between 0x20 or 0x7F included.</param>
    /// <returns>Smart enum with type of the box.</returns>
    /// <exception cref="ArgumentException">If byte vector does not have size of 3 or 4 or if
    /// any of byte does not have value between 0x20 and 0x7E included.</exception>
    public static implicit operator BoxType(string type) => FromString(type);
    /// <summary> Get box type from <see cref="ByteVector"/>.
    /// <para>  To permit ease of identification, the 32-bit compact type can be expressed as four characters from the
    /// range 0020 to 007E, inclusive, of ASCII.  The four individual byte values of the field are placed in order in the file. </para> </summary>
    /// <param name="type"><see cref="ByteVector"/> representing type of the box.</param>
    /// <returns>Smart enum with type of the box.</returns>
    /// <exception cref="ArgumentException">If byte vector does not have size of 4 or if
    /// any of byte does not have value between 0x20 and 0x7E included.</exception>
    public static BoxType FromByteVector(ByteVector type)
    {
        if (type.Count != 4)
            throw new ArgumentException("Type must be four-character code.");
        
        if(type.Any(c => c < 0x20 || c > 0x7E))
            throw new ArgumentException("Every character in type must have value between 0x20 and 0x7E.");
        
        return !All.ContainsKey(type) 
            ? new BoxType(type,type.ToString(Encoding.UTF8), ValueCounter++) 
            : All[type];
    }
    /// <summary> Get box type from string name. </summary>
    /// <param name="type">string representing type of the box. Size of string
    /// must be 3 or 4 characters and every character must be between 0x20 and 0x7E included.</param>
    /// <returns>Smart enum with type of the box.</returns>
    /// <exception cref="ArgumentException">If byte vector does not have size of 3 or 4 or if
    /// any of byte does not have value between 0x20 and 0x7E included.</exception>
    public static BoxType FromString(string type)
    {
        return FromByteVector( CreateFourCharacterCode((ByteVector)type));
    }
    
    /// <summary> Converts the provided ID into a readonly ID and fixes a 3 byte ID.</summary>
    /// <param name="id">A <see cref="ByteVector" /> object containing an ID to fix. </param>
    /// <returns> A fixed <see cref="ByteVector" /> or empty <see cref="ByteVector" /> if the ID could not be fixed.</returns>
    internal static ByteVector CreateFourCharacterCode (ByteVector id)
    {
        if (id.Count != 4)
            return id.Count == 3
                ? new ByteVectorBuilder(4){(byte)0xa9, id}.Build()
                : new ByteVector();
        return id;
    }
    
    /// <summary> Convert 4CC (four character code) as <see cref="ByteVector"/> to string representation.</summary>
    /// <param name="id">A <see cref="ByteVector" /> object containing an ID to convert to string. </param>
    /// <returns> <see cref="string"/> representation of four character code.</returns>
    internal static string ReadFourCharacterCode (ByteVector id)
    {
        return id[0] == 0xa9 ?
            id.ToString(Encoding.UTF8, 1, 3) 
            : id.ToString(Encoding.UTF8);
    }
}