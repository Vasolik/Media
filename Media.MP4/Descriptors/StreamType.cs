namespace Vipl.Media.MP4.Descriptors;

/// <summary> Conveys the type of this elementary stream. </summary>
public enum StreamType
{
    /// <summary> Forbidden. </summary>
    Forbidden = 0x00,
    /// <summary> ObjectDescriptorStream.
    /// <para>Similar to the scene description, object descriptors are transported in a dedicated elementary stream, termed
    /// object descriptor stream. Within such a stream, it is possible to dynamically convey, update and remove
    /// complete object descriptors, or their component descriptors, the ES_Descriptors, and IPMP descriptors. The
    /// update mechanism allows, for example, to advertise new elementary streams for an audio-visual object as
    /// they become available, or to remove references to streams that are no longer available. Updates are time
    /// stamped to indicate the instant in time they take effect.</para></summary>
    ObjectDescriptorStream = 0x01,
    /// <summary> ClockReferenceStream.
    /// <para>An elementary stream of streamType = ClockReferenceStream may be declared by means of the object
    /// descriptor. It is used for the sole purpose of conveying Object Clock Reference time stamps. Multiple
    /// elementary streams in a name scope may make reference to such a ClockReferenceStream by means of the
    /// OCR_ES_ID syntax element in the SLConfigDescriptor to avoid redundant transmission of Clock
    /// Reference information. Note, however, that circular references between elementary streams using
    /// OCR_ES_ID are not permitted.</para></summary>
    ClockReferenceStream = 0x02,
    /// <summary> SceneDescriptionStream (see ISO/IEC 14496-11). </summary>
    SceneDescriptionStream = 0x03,
    /// <summary> VisualStream. </summary>
    VisualStream = 0x04,
    /// <summary> AudioStream. </summary>
    AudioStream = 0x05,
    /// <summary> MPEG7Stream. </summary>
    Mpeg7Stream = 0x06,
    /// <summary> IPMPStream.
    /// <para>The Intellectual Property Management And Protection (IPMP) stream is an elementary stream that passes time-varying information to one or more IPMP Systems
    /// or Tools. This is accomplished by periodically sending a sequence of IPMP messages along with the content
    /// at a period determined by the IPMP System(s) or Tool(s).</para></summary>
    IntellectualPropertyManagementAndProtectionStream = 0x07,
    /// <summary> ObjectContentInfoStream.
    /// <para>The OCI stream is an elementary stream that conveys time-varying object content information, termed OCI
    /// events. Each OCI event consists of a number of OCI descriptors.</para></summary>
    ObjectContentInfoStream = 0x08,
    /// <summary> MPEGJStream.</summary>
    MpegJStream = 0x09,
    /// <summary> Interaction Stream. </summary>
    InteractionStream = 0x0A,
    /// <summary> IPMPToolStream (see [ISO/IEC 14496-13]). </summary>
    IntellectualPropertyManagementAndProtectionToolStream = 0x0B,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse0 = 0x0C,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse1 = 0x0D,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse2 = 0x0E,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse3 = 0x0F,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse4 = 0x10,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse5 = 0x11,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse6 = 0x12,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse7 = 0x13,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse8 = 0x14,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse9 = 0x15,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse10 = 0x16,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse11 = 0x17,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse12 = 0x18,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse13 = 0x19,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse14 = 0x1A,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse15 = 0x1B,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse16 = 0x1C,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse17 = 0x1D,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse18 = 0x1E,
    /// <summary> Reserved for ISO use. </summary>
    ReservedForIsoUse19 = 0x1F,
    /// <summary> User private. </summary>
    UserPrivate0 = 0x20,
    /// <summary> User private. </summary>
    UserPrivate1 = 0x21,
    /// <summary> User private. </summary>
    UserPrivate2 = 0x22,
    /// <summary> User private. </summary>
    UserPrivate3 = 0x23,
    /// <summary> User private. </summary>
    UserPrivate4 = 0x24,
    /// <summary> User private. </summary>
    UserPrivate5 = 0x25,
    /// <summary> User private. </summary>
    UserPrivate6 = 0x26,
    /// <summary> User private. </summary>
    UserPrivate7 = 0x27,
    /// <summary> User private. </summary>
    UserPrivate8 = 0x28,
    /// <summary> User private. </summary>
    UserPrivate9 = 0x29,
    /// <summary> User private. </summary>
    UserPrivate10 = 0x2A,
    /// <summary> User private. </summary>
    UserPrivate11 = 0x2B,
    /// <summary> User private. </summary>
    UserPrivate12 = 0x2C,
    /// <summary> User private. </summary>
    UserPrivate13 = 0x2D,
    /// <summary> User private. </summary>
    UserPrivate14 = 0x2E,
    /// <summary> User private. </summary>
    UserPrivate15 = 0x2F,
    /// <summary> User private. </summary>
    UserPrivate16 = 0x30,
    /// <summary> User private. </summary>
    UserPrivate17 = 0x31,
    /// <summary> User private. </summary>
    UserPrivate18 = 0x32,
    /// <summary> User private. </summary>
    UserPrivate19 = 0x33,
    /// <summary> User private. </summary>
    UserPrivate20 = 0x34,
    /// <summary> User private. </summary>
    UserPrivate21 = 0x35,
    /// <summary> User private. </summary>
    UserPrivate22 = 0x36,
    /// <summary> User private. </summary>
    UserPrivate23 = 0x37,
    /// <summary> User private. </summary>
    UserPrivate24 = 0x38,
    /// <summary> User private. </summary>
    UserPrivate25 = 0x39,
    /// <summary> User private. </summary>
    UserPrivate26 = 0x3A,
    /// <summary> User private. </summary>
    UserPrivate27 = 0x3B,
    /// <summary> User private. </summary>
    UserPrivate28 = 0x3C,
    /// <summary> User private. </summary>
    UserPrivate29 = 0x3D,
    /// <summary> User private. </summary>
    UserPrivate30 = 0x3E,
    /// <summary> User private. </summary>
    UserPrivate31 = 0x3F,
}