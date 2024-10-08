using System.Collections.Concurrent;
using System.Text;
using Ardalis.SmartEnum;
using Vipl.Base;
using Vipl.Media.MP4.Boxes.ISO_14496_1;
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
    private BoxType(string name, int value) : this(CreateFourCharacterCode(name.ToCharArray().Select(c => (byte)c).ToArray()), name, value) { }
    
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
    
    /// <summary> This box contains all the objects that declare characteristic information of the media in the track.</summary>
    public static readonly BoxType MediaInformation = new("minf", 27);
    
    /// <summary><para>The sample table contains all the time and data indexing of the media samples in a track. Using the tables
    /// here, it is possible to locate samples in time, determine their type (e.g. I-frame or not), and determine
    /// their size, container, and offset into that container.</para>
    /// <para>If the track that contains the <see cref="SampleTableBox"/>  references no data, then the <see cref="SampleTableBox"/>  does not
    /// need to contain any sub-boxes (this is not a very useful media track).</para> </summary>
    public static readonly BoxType SampleTable = new("stbl", 538);
    
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
    
    /// <summary>  <para>This box contains a compact version of a table that allows indexing from decoding timestamp to sample
    /// number. Other tables give sample sizes and pointers, from the sample number. Each entry in the table
    /// gives the number of consecutive samples with the same sample duration, and that sample duration. By
    /// adding the sample durations a complete time-to-sample map may be built.</para>
    /// <para>The <see cref="TimeToSampleBox"/> contains sample durations, the differences in decoding timestamps (DT):</para>
    /// <c>DT[n+1] = DT[n] + sample_delta[n]</c>
    /// <para>The sample entries are ordered by decoding timestamps; therefore all the values of sample_delta shall
    /// be non-negative.</para>
    /// <para>The DT axis has a zero origin; DT[i] = SUM[for j=0 to i-1 of sample_delta[j]], and in the absence of
    /// composition offsets, the sum of all sample durations gives the duration of the media in the track (not
    /// mapped to the overall timescale, and not considering any edit list).</para> </summary>
    public static readonly BoxType TimeToSample = new("stts", 132);

    /// <summary>The <see cref="DataInformationBox"/> contains objects that declare the location of the media information in a track. </summary>
    public static readonly BoxType DataInformation = new("dinf", 250);
    
    /// <summary> <para>The data reference object contains a table of data references (normally URLs) that declare the location(s)
    /// of the media data used within the presentation. The data reference index in the sample description ties
    /// entries in this table to the samples in the track. A track may be split over several sources in this way.</para>
    /// <para>If the flag is set indicating that the data is in the same file as this box, then no string (not even an empty
    /// one) shall be supplied in the entry field.</para>
    /// <para>The <see cref="DataReferenceBox.EntryCount"/> in the DataReferenceBox shall be 1 or greater.</para>
    /// <para>When a file that has data entries with the flag set indicating that the media data is in the same file,
    /// is split into segments for transport, the value of this flag does not change, as the file is (logically)
    /// reassembled after the transport operation.</para> </summary>
    public static readonly BoxType DataReference = new("dref", 270);
    
    /// <summary> Url data box.</summary>  
    public static readonly BoxType DataEntryUrl = new("url", 271);
    
    /// <summary> Url data box. Misspelled</summary>  
    public static readonly BoxType DataEntryUrlMisspelled = new("url ", 271);
    
    /// <summary> <para>This box contains the sample count and a table giving the size in bytes of each sample. This allows the
    /// media data itself to be unframed. The total number of samples in the media is always indicated in the
    /// sample count.</para>
    /// <para>There are two variants of the sample size box. The first variant has a fixed size 32-bit field for
    /// representing the sample sizes; it permits defining a constant size for all samples in a track. The second
    /// variant permits smaller size fields, to save space when the sizes are varying but small. One of these
    /// boxes shall be present; the first version is preferred for maximum compatibility.</para>
    /// <para>A sample size of zero is not prohibited in general, but it must be valid and defined for the coding
    /// system, as defined by the sample entry, that the sample belongs to.</para></summary>
    public static readonly BoxType SampleSize = new("stsz", 280);
    
    /// <summary> <para>Samples within the media data are grouped into chunks. Chunks can be of different sizes, and the
    /// samples within a chunk can have different sizes. This table can be used to find the chunk that contains
    /// a sample, its position, and the associated sample description.</para>
    /// <para>The table is compactly coded. Each entry gives the index of the first chunk of a run of chunks with the
    /// same characteristics. By subtracting one entry here from the previous one, it is possible to compute how
    /// many chunks are in this run. This can be converted to a sample count by multiplying by the appropriate
    /// samples-per-chunk.</para></summary>
    public static readonly BoxType SampleToChunk = new("stsc", 281);
    
    /// <summary>  <para>The chunk offset table gives the index of each chunk into the containing file. There are two variants,
    /// permitting the use of 32-bit or 64-bit offsets. The latter is useful when managing very large
    /// presentations. At most one of these variants will occur in any single instance of a sample table.</para>
    /// <para>When the referenced data reference entry is not DataEntryImdaBox or DataEntrySeqNumImdaBox, offsets
    /// are file offsets, not the offset into any box within the file (e.g. <see cref="MediaDataBox"/> ). This permits referring
    /// to media data in files without any box structure. It does also mean that care must be taken when
    /// constructing a self-contained ISO file with its structure-data (<see cref="MovieBox"/>) at the front, as the size of the
    /// MovieBox will affect the chunk offsets to the media data.</para>
    /// <para>When the referenced data reference entry is DataEntryImdaBox or DataEntrySeqNumImdaBox, offsets
    /// are relative to the first byte of the payload of the IdentifiedMediaDataBox corresponding to the data
    /// reference entry. This permits reordering file-level boxes and receiving a subset of file-level boxes but
    /// could require traversing the file-level boxes until the referenced IdentifiedMediaDataBox is found.</para></summary>
    public static readonly BoxType ChunkOffset = new("stco", 282);
    
    /// <summary> <para>This table can be used to find the group that a sample belongs to and the associated description of that
    /// sample group. The table is compactly coded with each entry giving the index of the first sample of a run
    /// of samples with the same sample group descriptor. The sample group description ID is an index that
    /// refers to a  SampleGroupDescriptionBox, which contains entries describing the characteristics of each
    /// sample group.</para>
    /// <para>TThere may be multiple instances of this box if there is more than one sample grouping for the samples in
    /// a track or track fragment. Each instance of the SampleToGroup box has a type that distinguishes different
    /// sample groupings. Within a track, whether declared in the SampleTableBox or in TrackFragmentBox,
    /// there shall be at most one instance of this box with a particular grouping_type, and, if present, a
    /// grouping_type_parameter. The associated SampleGroupDescriptionBox shall indicate the same
    /// value for the grouping_type. When there are multiple SampleToGroupBoxes with a particular value
    /// of grouping_type in a container box, the version of all the SampleToGroupBoxes shall be 1. When the
    /// version of a SampleToGroupBox is 0, there shall be only one occurrence of SampleToGroupBox with this
    /// grouping_type in a container box.</para>
    /// <para>Version 1 of this box should only be used if a grouping_type_parameter is needed. When the
    /// grouping_ type_parameter is not explicitly defined in this standard, its semantics may be overridden by derived
    /// specifications.</para>
    /// <para>For a SampleGroupDescriptionBox with a given grouping_type, there may be more than one
    /// SampleToGroupBox with the same grouping_type if and only if each SampleToGroupBox has a different
    /// value of grouping_type_parameter; there may also be no SampleToGroupBox with the given grouping_type
    /// if no samples are mapped to a description of that grouping_type, or if all samples are mapped to
    /// the default entry identified by the SampleGroupDescriptionBox.</para></summary>
    public static readonly BoxType SampleToGroup = new("sbgp", 283);


    /// <summary> <para>Audio tracks use the <see cref="SoundMediaHeaderBox"/>  in the MediaInformationBox as defined in 8.4.5. The sound
    /// media header contains general presentation information, independent of the coding, for audio media.
    /// This header is used for all tracks containing audio.</para> </summary>
    public static readonly BoxType SoundMediaHeader = new("smhd", 300);
    
    /// <summary><para>This box contains objects that declare user information about the containing box and its data (presentation or track).</para>
    /// <para>The User Data Box is a container box for informative user-data. This user data is formatted as a set of
    /// boxes with more specific box types, which declare more precisely their content. The contained boxes
    /// are normal boxes, using a defined, registered, or UUID extension box type.</para> </summary>
    public static readonly BoxType UserData = new("udta", 400);

    /// <summary>Sample entry for audio streams.</summary>
    public static readonly BoxType AudioSampleEntry = new("mp4a", 700);
    
    /// <summary>The <see cref="ElementaryStreamDescriptionBox"/> conveys all information related to a particular
    /// elementary stream and has three major parts.</summary>
    public static readonly BoxType ElementaryStreamDescription = new("esds", 701);
    
    /// <summary>Chapter or scene list. Usually references a text track.</summary>
    public static readonly BoxType ChapterTrackReference = new("chap", 1000);
    
    /// <summary> The text sample description contains information that defines how to interpret text media data.</summary>
    public static readonly BoxType TextSampleEntry = new("text", 1001);
    /// <summary>This atom specifies the font used to display the subtitle.</summary>
    public static readonly BoxType FontTable = new("ftab", 1002);

    
    /// <summary> Apple QuickTime UserDataBox.
    /// <para>he metadata item list atom holds a list of actual metadata values that are present in the metadata atom.
    /// The metadata items are formatted as a list of items. The metadata item list atom is of type ‘ilst’ and contains
    /// a number of metadata items, each of which is an atom.</para> </summary>
    public static readonly BoxType MetadataItemList = new("ilst", 1100);
    
    /// <summary>Data box can have multiply types of data, including text and images. </summary>
    public static readonly BoxType Data = new("data", 1101);
    
    /// <summary> Title of the content metadata.</summary>  
    public static readonly BoxType ContentTitle = new("©nam", 1102);
    
    /// <summary> Artist metadata.</summary>  
    public static readonly BoxType Artist = new("©ART", 1103);
    
    /// <summary> Album Artist metadata.</summary>  
    public static readonly BoxType AlbumArtist = new("aART", 1104);
    
    /// <summary> Comment metadata.</summary>  
    public static readonly BoxType Comment = new("©cmt", 1105);
    
    /// <summary> Copyright metadata.</summary>  
    public static readonly BoxType Copyright = new("cprt", 1106);
    
    /// <summary> Covert metadata.</summary>  
    public static readonly BoxType Covert = new("covr", 1107);
    
    /// <summary> Created Day metadata.</summary>  
    public static readonly BoxType CreatedDay = new("©day", 1108);
    
    /// <summary> Custom metadata.</summary>  
    public static readonly BoxType CustomMeta = new("----", 1109);
    
    /// <summary> Encoder metadata.</summary>  
    public static readonly BoxType Encoder = new("©too", 1110);
    
    /// <summary> Genre metadata.</summary>  
    public static readonly BoxType Genre = new("©gen", 1111);
    
    /// <summary> Source of custom metadata.</summary>  
    public static readonly BoxType MetadataSource = new("mean", 1112);
    
    /// <summary> Name of custom metadata. </summary>
    public new static readonly BoxType Name = new("name", 1112);

    /// <summary>  Adobe FLV ChapterBox.
    /// <para>The optional chpl box allows an F4V file to specify individual chapters along the main
    /// timeline of an F4V file. The information in this box is provided to ActionScript. The chpl box
    /// occurs within a moov box.</para></summary>
    public static readonly BoxType Chapters = new("chpl", 2000);
    

    /// <summary> </summary>
    public static readonly BoxType Cond = new("cond", 504);
    /// <summary> </summary>
    public static readonly BoxType IsoChunkLargeOffset = new("co64", 506);
    /// <summary> </summary>
    public static readonly BoxType Cpil = new("cpil", 507);

    
    /// <summary> </summary>
    public static readonly BoxType Desc = new("desc", 511);
    /// <summary> </summary>
    public static readonly BoxType Disk = new("disk", 512);
    /// <summary> </summary>
    public static readonly BoxType Dtag = new("dtag", 513);
    
    /// <summary> </summary>
    public static readonly BoxType Gnre = new("gnre", 518);
    /// <summary> </summary>
    public static readonly BoxType Grp = new("grp", 519);

    /// <summary> </summary>
    public static readonly BoxType Lyr = new("lyr", 521);
    
    

    


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
    public static readonly BoxType Subt = new("Subt", 541);

    /// <summary> </summary>
    public static readonly BoxType Tmpo = new("tmpo", 543);
    /// <summary> </summary>
    public static readonly BoxType Trkn = new("trkn", 545);


    /// <summary> </summary>
    public static readonly BoxType Uuid = new("uuid", 548);
    /// <summary> </summary>
    public static readonly BoxType Wrt = new("wrt", 549);
    /// <summary> </summary>
    // Handler types.
    public static readonly BoxType SoundIsoHandler = new("soun", 551);
    /// <summary> </summary>
    public static readonly BoxType VideoIsoHandler = new("vide", 552);
    /// <summary> </summary>
    // Another handler typeBoxType audio 
    public static readonly BoxType Alis = new("alis", 553);
    
    private static int ValueCounter { get; set; } = 5000;
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
        
        if(type.Any(c => c < 0x20 || c > 0x7E && c != 0xa9 && c != '-'))
            throw new ArgumentException("Every character in type must have value between 0x20 and 0x7E.");

        return !All.ContainsKey(type) 
            ? new BoxType(type, type.ToString(Encoding.UTF8), ValueCounter++) 
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
        return FromByteVector( CreateFourCharacterCode(type.ToCharArray().Select(c => (byte)c).ToArray()) );
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