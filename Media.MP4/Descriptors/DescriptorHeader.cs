using Vipl.Base;

namespace Vipl.Media.MP4.Descriptors;

/// <summary> DescriptorHeader for identifying descriptors. </summary>
public class DescriptorHeader
{
    /// <summary> Tag of the descriptor. </summary>
    public enum DescriptorTag : byte
    {
        /// <summary> Forbidden1 tag is not allowed. </summary>
        Forbidden1 = 0x00,
        /// <summary> ObjectDescriptor tag. </summary>
        ObjectDescriptorTag = 0x01,
        /// <summary> The InitialObjectDescriptor is a variation of the ObjectDescriptor
        /// that allows to signal profile and level information for the content referred ObjectDescriptor. It shall be used to
        /// gain initial access to ISO/IEC 14496 content </summary>
        InitialObjectDescriptorTag = 0x02,
        /// <summary>The <see cref="ElementaryStreamDescriptor"/> conveys all information related to a particular elementary stream and has three major parts. </summary>
        ElementaryStreamDescriptorTag = 0x03,
        /// <summary> The DecoderConfigDescriptor provides information about the decoder type and the required decoder
        /// resources needed for the associated elementary stream. This is needed at the receiving terminal to determine
        /// whether it is able to decode the elementary stream. A stream type identifies the category of the stream while
        /// the optional decoder specific information descriptor contains stream specific information for the set up of the
        /// decoder in a stream specific format that is opaque to this layer </summary>
        DecoderConfigDescriptorTag = 0x04,
        /// <summary> The decoder specific information constitutes an opaque container with information for a specific media decoder.
        /// The existence and semantics of decoder specific information depends on the values of
        /// DecoderConfigDescriptor.streamType and DecoderConfigDescriptor.objectTypeIndication. </summary>
        DecSpecificInfoTag = 0x05,
        /// <summary> The SynchronizationLayerConfigDescriptor provides information about the synchronization layer of the elementary stream.
        /// The synchronization layer is the layer that provides the timing information for the elementary stream. </summary>
        SynchronizationLayerConfigDescriptorTag = 0x06,
        /// <summary> The content identification descriptor is used to identify content. All types of elementary streams carrying
        /// content can be identified using this mechanism. The content types include audio, visual and scene description
        /// data. Multiple content identification descriptors may be associated to one elementary stream. These
        /// descriptors shall never be detached from the <see cref="ElementaryStreamDescriptor"/>. </summary>
        ContentIdentificationDescriptorTag = 0x07,
        /// <summary>The supplementary content identification descriptor is used to provide extensible identifiers for content that are
        /// qualified by a language code. Multiple supplementary content identification descriptors may be associated to
        /// one elementary stream. These descriptors shall never be detached from the <see cref="ElementaryStreamDescriptor"/>. </summary>
        SupplementaryContentIdentificationDescriptorTag = 0x08,
        /// <summary> The IntellectualPropertyIdentificationDescriptorPointer class contains a reference to the elementary stream that includes the
        /// IP_IdentificationDataSets that are valid for this stream. This indirect reference mechanism allows to
        /// convey such descriptors only in one elementary stream while making references to it from any
        /// <see cref="ElementaryStreamDescriptor"/> that shares the same information. </summary>
        IntellectualPropertyIdentificationDescriptorPointerTag = 0x09,
        /// <summary> TThe IntellectualPropertyManagementAndProtectionDescriptorPointer appears in the ipmpDescPtr section of an object descriptor or Elementary Stream descriptor structures.
        ///The presence of this descriptor in an object descriptor indicates that all streams referred to by embedded
        ///  <see cref="ElementaryStreamDescriptor"/> are subject to protection and management by the Intellectual Property Management And Protection System
        /// or  Intellectual Property Management And Protection Tool specified in the referenced IntellectualPropertyManagementAndProtectionDescriptor. </summary>
        IntellectualPropertyManagementAndProtectionDescriptorPointerTag = 0x0A,
        /// <summary> The IntellectualPropertyManagementAndProtectionDescriptor carries Intellectual Property Management And Protection information for one or
        /// more Intellectual Property Management And Protection System or Intellectual Property Management And Protection Tool
        /// instances. It shall also contain optional instantiation information for one
        /// or more Intellectual Property Management And Protection Tool instances. </summary>
        IntellectualPropertyManagementAndProtectionDescriptorTag = 0x0B,
        /// <summary> The QoS_descriptor conveys the requirements that the ES has on the transport channel and a description of
        /// the traffic that this Elementary Stream will generate. A set of predefined values is to be determined; customized values can be
        /// used by setting the predefined field to 0. </summary>
        QualityOfServiceDescriptorTag = 0x0C,
        /// <summary> The registration descriptor provides a method to uniquely and unambiguously identify formats of private data streams. </summary>
        RegistrationDescriptorTag = 0x0D,
        /// <summary> <para>The initial object descriptor and object descriptor streams are handled specially within the file format. Object
        /// descriptors contain Elementary Stream descriptors, which in turn contain TransMux specific information. In addition, to facilitate
        /// editing, the information about a track is stored as an ElementaryStreamDescriptor in the sample description within that track. It
        /// must be taken from there, re-written as appropriate, and transmitted as part of the OD stream when the
        /// presentation is streamed</para>
        /// <para>As a consequence, ES descriptors are not stored within the ObjectDescriptor track or initial object descriptor. Instead, the initial
        /// object descriptor has a descriptor used only in the file, containing solely the track ID of the elementary stream.
        /// When used, an appropriately re-written ElementaryStreamDescriptor from the referenced track replaces this descriptor. Likewise,
        /// ObjectDescriptor tracks are linked to Elementary Stream tracks by track references. Where an ElementaryStreamDescriptor would be used within the ObjectDescriptor track,
        /// another descriptor is used, which again occurs only in the file. It contains the index into the set of mpod track
        /// references that this OD track owns. A suitably re-written ESDescriptor replaces it by the hinting of this track.</para>
        ///<para>The ElementaryStreamIDIncTag is used in the initial object descriptor box</para> </summary>
        ElementaryStreamIDIncTag = 0x0E,
        /// <summary> <para>The initial object descriptor and object descriptor streams are handled specially within the file format. Object
        /// descriptors contain Elementary Stream descriptors, which in turn contain TransMux specific information. In addition, to facilitate
        /// editing, the information about a track is stored as an ElementaryStreamDescriptor in the sample description within that track. It
        /// must be taken from there, re-written as appropriate, and transmitted as part of the OD stream when the
        /// presentation is streamed</para>
        /// <para>As a consequence, ES descriptors are not stored within the ObjectDescriptor track or initial object descriptor. Instead, the initial
        /// object descriptor has a descriptor used only in the file, containing solely the track ID of the elementary stream.
        /// When used, an appropriately re-written ElementaryStreamDescriptor from the referenced track replaces this descriptor. Likewise,
        /// ObjectDescriptor tracks are linked to Elementary Stream tracks by track references. Where an ElementaryStreamDescriptor would be used within the ObjectDescriptor track,
        /// another descriptor is used, which again occurs only in the file. It contains the index into the set of mpod track
        /// references that this OD track owns. A suitably re-written ESDescriptor replaces it by the hinting of this track.</para>
        ///<para>The ElementaryStreamIDIncTag is used in the initial object descriptor stream</para> </summary>
        ElementaryStreamIDRefTag = 0x0F,
        /// <summary> When an InitialObjectDescriptor is used in the OD track in an MP4 file, the <see cref="InitialObjectDescriptorTag"/> is replaced by MP4InitialObjectDescriptorTag. </summary>
        MP4InitialObjectDescriptorTag = 0x10,
        /// <summary> When an ObjectDescriptor is used in the object descriptor track of an MP4 file, the <see cref="ObjectDescriptorTag"/> is replaced by MP4ObjectDescriptorTag. </summary>
        MP4ObjectDescriptorTag = 0x11,
        /// <summary>
        /// 
        /// </summary>
        InlineProfileLeveLDescriptionPointerRefTag = 0x12,
        /// <summary> The ExtensionProfileLevelDescriptor conveys profile and level extension information. This
        /// descriptor is used to signal a profile and level indication set and its unique index and can be extended by ISO
        /// to signal any future set of profiles and levels. </summary>
        ExtensionProfileLevelDescriptorTag = 0x13,
        /// <summary> A unique identifier for the set of profile and level indications described in this descriptor within the name scope defined by the Initial object descriptor </summary>
        ProfileLevelIndicationIndexDescriptorTag = 0x14,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag0 = 0x15,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag1 = 0x16,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag2 = 0x17,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag3 = 0x18,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag4 = 0x19,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag5 = 0x1A,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag6 = 0x1B,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag7 = 0x1C,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag8 = 0x1D,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag9 = 0x1E,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag10 = 0x1F,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag11 = 0x20,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag12 = 0x21,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag13 = 0x22,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag14 = 0x23,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag15 = 0x24,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag16 = 0x25,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag17 = 0x26,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag18 = 0x27,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag19 = 0x28,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag20 = 0x29,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag21 = 0x2A,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag22 = 0x2B,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag23 = 0x2C,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag24 = 0x2D,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag25 = 0x2E,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag26 = 0x2F,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag27 = 0x30,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag28 = 0x31,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag29 = 0x32,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag30 = 0x33,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag31 = 0x34,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag32 = 0x35,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag33 = 0x36,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag34 = 0x37,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag35 = 0x38,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag36 = 0x39,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag37 = 0x3A,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag38 = 0x3B,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag39 = 0x3C,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag40 = 0x3D,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag41 = 0x3E,
        /// <summary> Reserved for use by ISO </summary>
        ReservedTag42 = 0x3F,
        /// <summary> ObjectContentInformationDescriptorTagStartRange is the first tag in the range of tags that are reserved for use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTagStartRange = 0x40,
        /// <summary> The content classification descriptor provides one or more classifications of the event information. The
        /// classificationEntity field indicates the organization that classifies the content. The possible values
        /// have to be registered with a registration authority to be identified. </summary>
        ContentClassificationDescriptorTag = 0x40,
        /// <summary> The key word descriptor allows the Object content information (OCI) creator/provider to indicate a set of key words that characterize the
        /// content. The choice of the key words is completely free but each time the key word descriptor appears, all the
        /// key words given are for the language indicated in languageCode. This means that, for a certain event, the
        /// key word descriptor must appear as many times as the number of languages for which key words are to be
        /// provided. </summary>
        KeyWordDescriptorTag = 0x41,
        /// <summary> This descriptor gives one or more ratings, originating from corresponding rating entities, valid for a specified
        /// country. The ratingEntity field indicates the organization which is rating the content. The possible values
        /// have to be registered with a registration authority to be identified. This registration authority shall make the
        /// semantics of the rating descriptor publicly available. </summary>
        RatingDescriptorTag = 0x42,
        /// <summary> This descriptor identifies the language of the corresponding audio/speech or text object that is being described. </summary>
        LanguageDescriptorTag = 0x43,
        /// <summary> The short textual descriptor provides the name of the event and a short description of the event in text form.  </summary>
        ShortTextualDescriptorTag = 0x44,
        /// <summary> The expanded textual descriptor provides a detailed description of an event, which may be used in addition to,
        /// or independently from, the short event descriptor. In addition to direct text, structured information in terms of
        /// pairs of description and text may be provided. An example application for this structure is to give a cast list,
        /// where for example the item description field might be “Producer” and the item field would give the name of the producer. </summary>
        ExpandedTextualDescriptorTag = 0x45,
        /// <summary> The content creator name descriptor indicates the name(s) of the content creator(s).
        /// Each content creator name may be in a different language. </summary>
        ContentCreatorNameDescriptorTag = 0x46,
        /// <summary> This descriptor identifies the date of the content creation. </summary>
        ContentCreationDateDescriptorTag = 0x47,
        /// <summary> The name of Object content information (OCI) creators descriptor indicates the name(s) of the
        /// Object content information (OCI) description creator(s). Each Object content information (OCI) creator name may be in a different language. </summary>
        ObjectContentInformationCreatorNameDescriptorTag = 0x48,
        /// <summary> This descriptor identifies the creation date of the Object content information (OCI) description. </summary>
        ObjectContentInformationCreationDateDescriptorTag = 0x49,
        /// <summary> The Society of Motion Picture and Television Engineers (SMPTE) metadata descriptor provides metadata
        /// defined by the Proposed SMPTE Standard 315M of “camera positioning information conveyed by ancillary data packets.” </summary>
        SmpteCameraPositionDescriptorTag = 0x4A,
        /// <summary> <para>The segment descriptor defines labeled segments within a media stream with respect to the media time line. A
        /// segment for a given media stream is declared by conveying a segment descriptor with appropriate values as
        /// part of the object descriptor that declares that media stream. Conversely, when a segment descriptor exists in
        /// an object descriptor, it refers to all the media streams in that object descriptor. Segments can be referenced
        /// from the scene description through url fields of media nodes.</para>
        /// <para>In order to use segment descriptors for the declaration of segments within a media stream, the notion of a
        /// media time line needs to be established. The media time line of a media stream may be defined through use
        /// of media time descriptor. In the absence of such explicit definitions, media time of the first
        /// composition unit of a media stream is assumed to be zero. In applications where random access into a media
        /// stream is supported, the media time line is undefined unless the media time descriptor mechanism is used.</para></summary>
        SegmentDescriptorTag = 0x4B,
        /// <summary> The media time descriptor conveys a media time stamp. The descriptor establishes the mapping between the
        /// object time base and the media time line of a media stream. This descriptor shall only be conveyed within an
        /// Object content information (OCI) stream. The startingTime, absoluteTimeFlag and duration fields of the Object
        /// content information (OCI) event carrying this descriptor shall be set to 0. The association between the
        /// Object content information (OCI) stream and the corresponding media stream is defined by an object descriptor
        /// that aggregates ES descriptors for both of them. </summary>
        MediaTimeDescriptorTag = 0x4C,
        /// <summary> ObjectContentInformationDescriptorTag0 is the first tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa0 = 0x40,
        /// <summary> ObjectContentInformationDescriptorTag1 is the second tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa1 = 0x41,
        /// <summary> ObjectContentInformationDescriptorTag2 is the third tag in the range of tags that are reserved
        ///  for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa2 = 0x42,
        /// <summary> ObjectContentInformationDescriptorTag3 is the fourth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa3 = 0x43,
        /// <summary> ObjectContentInformationDescriptorTag4 is the fifth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa4 = 0x44,
        /// <summary> ObjectContentInformationDescriptorTag5 is the sixth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa5 = 0x45,
        /// <summary> ObjectContentInformationDescriptorTag6 is the seventh tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa6 = 0x46,
        /// <summary> ObjectContentInformationDescriptorTag7 is the eighth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa7 = 0x47,
        /// <summary> ObjectContentInformationDescriptorTag8 is the ninth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa8 = 0x48,
        /// <summary> ObjectContentInformationDescriptorTag9 is the tenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa9 = 0x49,
        /// <summary> ObjectContentInformationDescriptorTag10 is the eleventh tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa10 = 0x4A,
        /// <summary> ObjectContentInformationDescriptorTag11 is the twelfth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa11 = 0x4B,
        /// <summary> ObjectContentInformationDescriptorTag12 is the thirteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa12 = 0x4C,
        /// <summary> ObjectContentInformationDescriptorTag13 is the fourteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa13 = 0x4D,
        /// <summary> ObjectContentInformationDescriptorTag14 is the fifteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa14 = 0x4E,
        /// <summary> ObjectContentInformationDescriptorTag15 is the sixteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa15 = 0x4F,
        /// <summary> ObjectContentInformationDescriptorTag16 is the seventeenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa16 = 0x50,
        /// <summary> ObjectContentInformationDescriptorTag17 is the eighteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa17 = 0x51,
        /// <summary> ObjectContentInformationDescriptorTag18 is the nineteenth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa18 = 0x52,
        /// <summary> ObjectContentInformationDescriptorTag19 is the twentieth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa19 = 0x53,
        /// <summary> ObjectContentInformationDescriptorTag20 is the twenty-first tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa20 = 0x54,
        /// <summary> ObjectContentInformationDescriptorTag21 is the twenty-second tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa21 = 0x55,
        /// <summary> ObjectContentInformationDescriptorTag22 is the twenty-third tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa22 = 0x56,
        /// <summary> ObjectContentInformationDescriptorTag23 is the twenty-fourth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa23 = 0x57,
        /// <summary> ObjectContentInformationDescriptorTag24 is the twenty-fifth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa24 = 0x58,
        /// <summary> ObjectContentInformationDescriptorTag25 is the twenty-sixth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa25 = 0x59,
        /// <summary> ObjectContentInformationDescriptorTag26 is the twenty-seventh tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa26 = 0x5A,
        /// <summary> ObjectContentInformationDescriptorTag27 is the twenty-eighth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa27 = 0x5B,
        /// <summary> ObjectContentInformationDescriptorTag28 is the twenty-ninth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa28 = 0x5C,
        /// <summary> ObjectContentInformationDescriptorTag29 is the thirtieth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa29 = 0x5D,
        /// <summary> ObjectContentInformationDescriptorTag30 is the thirtieth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa30 = 0x5E,
        /// <summary> ObjectContentInformationDescriptorTag31 is the thirtieth tag in the range of tags that are reserved
        /// for future use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTa31 = 0x5F,
        /// <summary> ObjectContentInformationDescriptorTagEndRange is the last tag in the range of tags that are reserved for use by ISO for Object content information (OCI). </summary>
        ObjectContentInformationDescriptorTagEndRange = 0x5F,
        /// <summary> Describing a logical Intellectual Property Management and Protection (IPMP) Tool required to access the content </summary>
        IntellectualPropertyManagementAndProtectionToolsListDescriptorTag = 0x60,
        /// <summary> Each instance of Class IPMP_Tool identifies one Intellectual Property Management and Protection (IPMP) Tool that is required by the Terminal to Consume
        /// the Content. This Tool shall be specified either as a unique implementation, as one of a list of alternatives, or
        /// through a parametric description. </summary>
        IntellectualPropertyManagementAndProtectionToolTag = 0x61,
        /// <summary> This descriptor class defines FCR_ES_ID, FCRResolution, FCRLength, FmxRateLength. </summary>
        M4MuxTimingDescriptorTag = 0x62,
        /// <summary> This class defines the M4Mux configuration of one M4Mux channel. </summary>
        M4MuxCodeTableDescriptorTag = 0x63,
        /// <summary> The synchronization layer (SL) packet header may be configured according to the needs of each individual elementary stream.
        /// Parameters that can be selected include the presence, resolution and accuracy of time stamps and clock
        /// references. This flexibility allows, for example, a low bitrate elementary stream to incur very little overhead on
        /// SL packet headers. </summary>
        ExtendedSynchronizationLayerConfigDescriptorTag = 0x64,
        /// <summary> The default size of multiplex buffers for each individual channel in a
        /// M4Mux stream is signalled by the DefaultM4MuxBufferDescriptor class. </summary>
        M4MuxBufferSizeDescriptorTag = 0x65,
        /// <summary> This class defines MuxID, MuxType, MuxManagement </summary>
        M4MuxIdentDescriptorTag = 0x66,
        /// <summary> DependencyPointer extends SynchronizationLayerExtensionDescriptor and specifies that access units from this stream depend
        /// on another stream. </summary>
        DependencyPointerTag = 0x67,
        /// <summary> MarkerDescriptor extends SynchronizationLayerExtensionDescriptor and allows to tag access units so as to be able to refer to
        /// them independently from their decodingTimeStamp. </summary>
        DependencyMarkerTag = 0x68,
        /// <summary> M4MuxChannelDescrTag extends BaseDescriptor and specifies the M4Mux channel configuration of one M4Mux channel. </summary>
        M4MuxChannelDescriptorTag = 0x69,
        /// <summary> ExtDescriptorTagStartRange is the first value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTagStartRange = 0x6A,
        /// <summary> ExtDescriptorTag0 is the first value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag0 = 0x6A,
        /// <summary> ExtDescriptorTag1 is the second value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag1 = 0x6B,
        /// <summary> ExtDescriptorTag2 is the third value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag2 = 0x6C,
        /// <summary> ExtDescriptorTag3 is the fourth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag3 = 0x6D,
        /// <summary> ExtDescriptorTag4 is the fifth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag4 = 0x6E,
        /// <summary> ExtDescriptorTag5 is the sixth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag5 = 0x6F,
        /// <summary> ExtDescriptorTag6 is the seventh value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag6 = 0x70,
        /// <summary> ExtDescriptorTag7 is the eighth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag7 = 0x71,
        /// <summary> ExtDescriptorTag8 is the ninth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag8 = 0x72,
        /// <summary> ExtDescriptorTag9 is the tenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag9 = 0x73,
        /// <summary> ExtDescriptorTag10 is the eleventh value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag10 = 0x74,
        /// <summary> ExtDescriptorTag11 is the twelfth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag11 = 0x75,
        /// <summary> ExtDescriptorTag12 is the thirteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag12 = 0x76,
        /// <summary> ExtDescriptorTag13 is the fourteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag13 = 0x77,
        /// <summary> ExtDescriptorTag14 is the fifteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag14 = 0x78,
        /// <summary> ExtDescriptorTag15 is the sixteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag15 = 0x79,
        /// <summary> ExtDescriptorTag16 is the seventeenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag16 = 0x7A,
        /// <summary> ExtDescriptorTag17 is the eighteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag17 = 0x7B,
        /// <summary> ExtDescriptorTag18 is the nineteenth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag18 = 0x7C,
        /// <summary> ExtDescriptorTag19 is the twentieth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag19 = 0x7D,
        /// <summary> ExtDescriptorTag20 is the twenty-first value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag20 = 0x7E,
        /// <summary> ExtDescriptorTag21 is the twenty-second value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag21 = 0x7F,
        /// <summary> ExtDescriptorTag22 is the twenty-third value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag22 = 0x80,
        /// <summary> ExtDescriptorTag23 is the twenty-fourth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag23 = 0x81,
        /// <summary> ExtDescriptorTag24 is the twenty-fifth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag24 = 0x82,
        /// <summary> ExtDescriptorTag25 is the twenty-sixth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag25 = 0x83,
        /// <summary> ExtDescriptorTag26 is the twenty-seventh value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag26 = 0x84,
        /// <summary> ExtDescriptorTag27 is the twenty-eighth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag27 = 0x85,
        /// <summary> ExtDescriptorTag28 is the twenty-ninth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag28 = 0x86,
        /// <summary> ExtDescriptorTag29 is the thirtieth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag29 = 0x87,
        /// <summary> ExtDescriptorTag30 is the thirty-first value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag30 = 0x88,
        /// <summary> ExtDescriptorTag31 is the thirty-second value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag31 = 0x89,
        /// <summary> ExtDescriptorTag32 is the thirty-third value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag32 = 0x8A,
        /// <summary> ExtDescriptorTag33 is the thirty-fourth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag33 = 0x8B,
        /// <summary> ExtDescriptorTag34 is the thirty-fifth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag34 = 0x8C,
        /// <summary> ExtDescriptorTag35 is the thirty-sixth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag35 = 0x8D,
        /// <summary> ExtDescriptorTag36 is the thirty-seventh value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag36 = 0x8E,
        /// <summary> ExtDescriptorTag37 is the thirty-eighth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag37 = 0x8F,
        /// <summary> ExtDescriptorTag38 is the thirty-ninth value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag38 = 0x90,
        /// <summary> ExtDescriptorTag39 is the 40th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag39 = 0x91,
        /// <summary> ExtDescriptorTag40 is the 41st value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag40 = 0x92,
        /// <summary> ExtDescriptorTag41 is the 42nd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag41 = 0x93,
        /// <summary> ExtDescriptorTag42 is the 43rd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag42 = 0x94,
        /// <summary> ExtDescriptorTag43 is the 44th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag43 = 0x95,
        /// <summary> ExtDescriptorTag44 is the 45th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag44 = 0x96,
        /// <summary> ExtDescriptorTag45 is the 46th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag45 = 0x97,
        /// <summary> ExtDescriptorTag46 is the 47th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag46 = 0x98,
        /// <summary> ExtDescriptorTag47 is the 48th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag47 = 0x99,
        /// <summary> ExtDescriptorTag48 is the 49th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag48 = 0x9A,
        /// <summary> ExtDescriptorTag49 is the 50th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag49 = 0x9B,
        /// <summary> ExtDescriptorTag50 is the 51st value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag50 = 0x9C,
        /// <summary> ExtDescriptorTag51 is the 52nd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag51 = 0x9D,
        /// <summary> ExtDescriptorTag52 is the 53rd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag52 = 0x9E,
        /// <summary> ExtDescriptorTag53 is the 54th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag53 = 0x9F,
        /// <summary> ExtDescriptorTag54 is the 55th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag54 = 0xA0,
        /// <summary> ExtDescriptorTag55 is the 56th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag55 = 0xA1,
        /// <summary> ExtDescriptorTag56 is the 57th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag56 = 0xA2,
        /// <summary> ExtDescriptorTag57 is the 58th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag57 = 0xA3,
        /// <summary> ExtDescriptorTag58 is the 59th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag58 = 0xA4,
        /// <summary> ExtDescriptorTag59 is the 60th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag59 = 0xA5,
        /// <summary> ExtDescriptorTag60 is the 61st value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag60 = 0xA6,
        /// <summary> ExtDescriptorTag61 is the 62nd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag61 = 0xA7,
        /// <summary> ExtDescriptorTag62 is the 63rd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag62 = 0xA8,
        /// <summary> ExtDescriptorTag63 is the 64th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag63 = 0xA9,
        /// <summary> ExtDescriptorTag64 is the 65th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag64 = 0xAA,
        /// <summary> ExtDescriptorTag65 is the 66th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag65 = 0xAB,
        /// <summary> ExtDescriptorTag66 is the 67th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag66 = 0xAC,
        /// <summary> ExtDescriptorTag67 is the 68th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag67 = 0xAD,
        /// <summary> ExtDescriptorTag68 is the 69th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag68 = 0xAE,
        /// <summary> ExtDescriptorTag69 is the 70th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag69 = 0xAF,
        /// <summary> ExtDescriptorTag70 is the 71st value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag70 = 0xB0,
        /// <summary> ExtDescriptorTag71 is the 72nd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag71 = 0xB1,
        /// <summary> ExtDescriptorTag72 is the 73rd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag72 = 0xB2,
        /// <summary> ExtDescriptorTag73 is the 74th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag73 = 0xB3,
        /// <summary> ExtDescriptorTag74 is the 75th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag74 = 0xB4,
        /// <summary> ExtDescriptorTag75 is the 76th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag75 = 0xB5,
        /// <summary> ExtDescriptorTag76 is the 77th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag76 = 0xB6,
        /// <summary> ExtDescriptorTag77 is the 78th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag77 = 0xB7,
        /// <summary> ExtDescriptorTag78 is the 79th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag78 = 0xB8,
        /// <summary> ExtDescriptorTag79 is the 80th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag79 = 0xB9,
        /// <summary> ExtDescriptorTag80 is the 81st value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag80 = 0xBA,
        /// <summary> ExtDescriptorTag81 is the 82nd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag81 = 0xBB,
        /// <summary> ExtDescriptorTag82 is the 83rd value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag82 = 0xBC,
        /// <summary> ExtDescriptorTag83 is the 84th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag83 = 0xBD,
        /// <summary> ExtDescriptorTag84 is the 85th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag84 = 0xBE,
        /// <summary> ExtDescriptorTag85 is the 86th value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTag85 = 0xBF,
        /// <summary> ExtDescriptorTagEndRange is the last value of the range of descriptor tags that are reserved for future use. </summary>
        ExtDescriptorTagEndRange = 0xBF,
        /// <summary> PrivateUseDescriptorTagStartRange is the first value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTagStartRange = 0xC0,
        /// <summary> PrivateUseDescriptorTag0 is the first value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag0 = 0xC0,
        /// <summary> PrivateUseDescriptorTag1 is the second value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag1 = 0xC1,
        /// <summary> PrivateUseDescriptorTag2 is the third value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag2 = 0xC2,
        /// <summary> PrivateUseDescriptorTag3 is the fourth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag3 = 0xC3,
        /// <summary> PrivateUseDescriptorTag4 is the fifth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag4 = 0xC4,
        /// <summary> PrivateUseDescriptorTag5 is the sixth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag5 = 0xC5,
        /// <summary> PrivateUseDescriptorTag6 is the seventh value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag6 = 0xC6,
        /// <summary> PrivateUseDescriptorTag7 is the eighth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag7 = 0xC7,
        /// <summary> PrivateUseDescriptorTag8 is the ninth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag8 = 0xC8,
        /// <summary> PrivateUseDescriptorTag9 is the tenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag9 = 0xC9,
        /// <summary> PrivateUseDescriptorTag10 is the eleventh value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag10 = 0xCA,
        /// <summary> PrivateUseDescriptorTag11 is the twelfth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag11 = 0xCB,
        /// <summary> PrivateUseDescriptorTag12 is the thirteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag12 = 0xCC,
        /// <summary> PrivateUseDescriptorTag13 is the fourteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag13 = 0xCD,
        /// <summary> PrivateUseDescriptorTag14 is the fifteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag14 = 0xCE,
        /// <summary> PrivateUseDescriptorTag15 is the sixteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag15 = 0xCF,
        /// <summary> PrivateUseDescriptorTag16 is the seventeenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag16 = 0xD0,
        /// <summary> PrivateUseDescriptorTag17 is the eighteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag17 = 0xD1,
        /// <summary> PrivateUseDescriptorTag18 is the nineteenth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag18 = 0xD2,
        /// <summary> PrivateUseDescriptorTag19 is the twentieth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag19 = 0xD3,
        /// <summary> PrivateUseDescriptorTag20 is the twenty-first value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag20 = 0xD4,
        /// <summary> PrivateUseDescriptorTag21 is the twenty-second value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag21 = 0xD5,
        /// <summary> PrivateUseDescriptorTag22 is the twenty-third value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag22 = 0xD6,
        /// <summary> PrivateUseDescriptorTag23 is the twenty-fourth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag23 = 0xD7,
        /// <summary> PrivateUseDescriptorTag24 is the twenty-fifth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag24 = 0xD8,
        /// <summary> PrivateUseDescriptorTag25 is the twenty-sixth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag25 = 0xD9,
        /// <summary> PrivateUseDescriptorTag26 is the twenty-seventh value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag26 = 0xDA,
        /// <summary> PrivateUseDescriptorTag27 is the twenty-eighth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag27 = 0xDB,
        /// <summary> PrivateUseDescriptorTag28 is the twenty-ninth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag28 = 0xDC,
        /// <summary> PrivateUseDescriptorTag29 is the thirtieth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag29 = 0xDD,
        /// <summary> PrivateUseDescriptorTag30 is the thirty-first value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag30 = 0xDE,
        /// <summary> PrivateUseDescriptorTag31 is the thirty-second value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag31 = 0xDF,
        /// <summary> PrivateUseDescriptorTag32 is the thirty-third value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag32 = 0xE0,
        /// <summary> PrivateUseDescriptorTag33 is the thirty-fourth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag33 = 0xE1,
        /// <summary> PrivateUseDescriptorTag34 is the thirty-fifth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag34 = 0xE2,
        /// <summary> PrivateUseDescriptorTag35 is the thirty-sixth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag35 = 0xE3,
        /// <summary> PrivateUseDescriptorTag36 is the thirty-seventh value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag36 = 0xE4,
        /// <summary> PrivateUseDescriptorTag37 is the thirty-eighth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag37 = 0xE5,
        /// <summary> PrivateUseDescriptorTag38 is the thirty-ninth value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag38 = 0xE6,
        /// <summary> PrivateUseDescriptorTag39 is the 40th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag39 = 0xE7,
        /// <summary> PrivateUseDescriptorTag40 is the 41st value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag40 = 0xE8,
        /// <summary> PrivateUseDescriptorTag41 is the 42nd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag41 = 0xE9,
        /// <summary> PrivateUseDescriptorTag42 is the 43rd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag42 = 0xEA,
        /// <summary> PrivateUseDescriptorTag43 is the 44th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag43 = 0xEB,
        /// <summary> PrivateUseDescriptorTag44 is the 45th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag44 = 0xEC,
        /// <summary> PrivateUseDescriptorTag45 is the 46th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag45 = 0xED,
        /// <summary> PrivateUseDescriptorTag46 is the 47th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag46 = 0xEE,
        /// <summary> PrivateUseDescriptorTag47 is the 48th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag47 = 0xEF,
        /// <summary> PrivateUseDescriptorTag48 is the 49th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag48 = 0xF0,
        /// <summary> PrivateUseDescriptorTag49 is the 50th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag49 = 0xF1,
        /// <summary> PrivateUseDescriptorTag50 is the 51st value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag50 = 0xF2,
        /// <summary> PrivateUseDescriptorTag51 is the 52nd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag51 = 0xF3,
        /// <summary> PrivateUseDescriptorTag52 is the 53rd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag52 = 0xF4,
        /// <summary> PrivateUseDescriptorTag53 is the 54th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag53 = 0xF5,
        /// <summary> PrivateUseDescriptorTag54 is the 55th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag54 = 0xF6,
        /// <summary> PrivateUseDescriptorTag55 is the 56th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag55 = 0xF7,
        /// <summary> PrivateUseDescriptorTag56 is the 57th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag56 = 0xF8,
        /// <summary> PrivateUseDescriptorTag57 is the 58th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag57 = 0xF9,
        /// <summary> PrivateUseDescriptorTag58 is the 59th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag58 = 0xFA,
        /// <summary> PrivateUseDescriptorTag59 is the 60th value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag59 = 0xFB,
        /// <summary> PrivateUseDescriptorTag60 is the 61st value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag60 = 0xFC,
        /// <summary> PrivateUseDescriptorTag61 is the 62nd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag61 = 0xFD,
        /// <summary> PrivateUseDescriptorTag62 is the 63rd value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTag62 = 0xFE,
        /// <summary> PrivateUseDescriptorTagEndRange is the last value of the range of descriptor tags that are reserved for private use. </summary>
        PrivateUseDescriptorTagEndRange = 0xFE,
        /// <summary> Forbidden2 tag is not allowed. </summary>
        Forbidden2 = 0xFF,
    }

    /// <summary> Creates a new instance of the <see cref="DescriptorTag"/> enumeration. </summary>
    /// <param name="data">Data present in the header.</param>
    /// <exception cref="ArgumentException">If there is not enough data in provided data.</exception>
    public DescriptorHeader(Span<byte> data)
    {     
        Tag = (DescriptorTag)data[0];
        SizeOfLength = 0;
        Length = 0;
        do 
        {
            SizeOfLength++;
            Length = (Length << 7) + (data[SizeOfLength] & 0x7F);
        } while ((data[SizeOfLength] & 0x80) != 0);
        
    }

    /// <summary> Each descriptor constitutes a self-describing class, identified by a unique class tag. </summary>
    public DescriptorTag Tag { get; set; }
    /// <summary> Size of the descriptor in bytes. </summary>
    public int Length { get; set; }

    /// <summary> Size of length field in bytes. </summary>
    public int SizeOfLength { get; set; }
    
    /// <summary> Size of header in bytes. </summary>
    public int SizeOfHeader=> SizeOfLength + 1;
    
    /// <summary> Renders the current instance as a byte vector to byte vector builder. </summary>
    /// <param name="builder">Builder to store current instance.</param>
    /// <returns>Builder instance for chaining.</returns>
    public virtual IByteVectorBuilder Render(IByteVectorBuilder builder)
    {
        builder.Add((byte)Tag);
        for (var i = 0; i < SizeOfLength; i++)
        {
            var mask = 0x7F << (8 * (SizeOfLength - i - 1));
            var sizeByte = (byte)( Length & mask | (i < SizeOfLength - 1 ? 0x80: 0x00));
            builder.Add(sizeByte);
        }
        return builder;
    }
}