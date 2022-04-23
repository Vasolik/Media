namespace Vipl.Media.MP4.Descriptors;

/// <summary> ObjectTypeIndicationType is an indication of the object or scene description type
/// that needs to be supported by the decoder for this elementary stream </summary>
public enum ObjectTypeIndicationType : byte
{
    /// <summary> Forbidden value. </summary>
    Forbidden = 0x00,
    /// <summary> Systems ISO/IEC 14496-1
    /// <para>This type is used for all 14496-1 streams unless specifically indicated to the contrary. Scene Description
    /// scenes, which are identified with StreamType=0x03, using this object type value shall use the BIFSConfig
    /// specified in ISO/IEC 14496-11.</para></summary>
    IsoIec144961System = 0x01,
    /// <summary> Systems ISO/IEC 14496-1
    /// <para>This object type shall be used, with StreamType=0x03, for Scene Description streams that use the
    /// BIFSv2Config specified in ISO/IEC 14496-11. Its use with other StreamTypes is reserved.</para></summary>
    IsoIec144961System2 = 0x02,
    /// <summary>Interaction Stream</summary>
    InteractionStream = 0x03,
    /// <summary> Systems ISO/IEC 14496-1 Extended BIFS Configuration
    /// <para>This object type shall be used, with StreamType=0x03, for Scene Description streams that use the
    /// BIFSv1Config specified in ISO/IEC 14496-11. Its use with other StreamTypes is reserved.</para></summary>
    IsoIec144961ExtendedBifsConfiguration = 0x04,
    /// <summary> Systems ISO/IEC 14496-1 AFX Configuration
    /// <para>This object type shall be used, with StreamType=0x03, for Scene Description streams that use the
    /// AFXv1Config specified in ISO/IEC 14496-11. Its use with other StreamTypes is reserved.</para></summary>
    IsoIec144961AfxConfiguration = 0x05,
    /// <summary> Font Data Stream. </summary>
    FontDataStream = 0x06,
    /// <summary> Synthesized Texture Stream. </summary>
    SynthesizedTextureStream = 0x07,
    /// <summary> Streaming Text Stream. </summary>
    StreamingTextStream = 0x08,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved0 = 0x09,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved1 = 0x0A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved2 = 0x0B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved3 = 0x0C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved4 = 0x0D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved5 = 0x0E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved6= 0x0F,
    /// <summary> Visual ISO/IEC 14496-2.
    /// <para>Includes associated Amendment(s) and Corrigendum(a). The actual object types are defined in
    /// ISO/IEC 14496-2 and are conveyed in the DecoderSpecificInfo as specified in ISO/IEC 14496-2, Annex K.</para></summary>
    VisualIsoIec144962 = 0x20,
    /// <summary> Visual ITU-T Recommendation H.264 | ISO/IEC 14496-10
    /// <para>Includes associated Amendment(s) and Corrigendum(a). The actual object types are defined in ITU-T
    /// Recommendation H.264 | ISO/IEC 14496-10 and are conveyed in the DecoderSpecificInfo as specified in this
    /// amendment, I.2.</para></summary>
    VisualItuTRecommendationH264 = 0x21,
    /// <summary> Parameter Sets for ITU-T Recommendation H.264 | ISO/IEC 14496-10
    /// <para>Includes associated Amendment(s) and Corrigendum(a). The actual object types are defined in ITU-T
    /// Recommendation H.264 | ISO/IEC 14496-10 and are conveyed in the DecoderSpecificInfo as specified in this
    /// amendment, I.2.</para></summary>
    ParameterSetsForItuTRecommendationH264 = 0x22,
    /// <summary>  	Visual ISO/IEC 23008-2 | ITU-T Recommendation H.265. </summary>
    VisualIsoIec230082H265 = 0x23,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved8 = 0x24,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved9 = 0x25,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved10 = 0x26,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved11 = 0x27,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved12 = 0x28,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved13 = 0x29,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved14 = 0x2A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved15 = 0x2B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved16 = 0x2C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved17 = 0x2D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved18 = 0x2E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved19 = 0x2F,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved20 = 0x30,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved21 = 0x31,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved22 = 0x32,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved23 = 0x33,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved24 = 0x34,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved25 = 0x35,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved26 = 0x35,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved27 = 0x36,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved28 = 0x37,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved29 = 0x38,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved30 = 0x39,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved31 = 0x3A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved32 = 0x3B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved33 = 0x3C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved34 = 0x3D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved35 = 0x3E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved36 = 0x3F,
    /// <summary> Audio ISO/IEC 14496-3 </summary>
    AudioIsoIec144963 = 0x40,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved37 = 0x41,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved38 = 0x42,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved39 = 0x43,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved40 = 0x44,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved41 = 0x45,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved42 = 0x46,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved43 = 0x47,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved44 = 0x48,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved45 = 0x49,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved46 = 0x4A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved47 = 0x4B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved48 = 0x4C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved49 = 0x4D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved50 = 0x4E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved51 = 0x4F,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved52 = 0x50,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved53 = 0x51,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved54 = 0x52,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved55 = 0x53,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved56 = 0x54,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved57 = 0x55,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved58 = 0x56,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved59 = 0x57,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved60 = 0x58,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved61 = 0x59,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved62 = 0x5A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved63 = 0x5B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved64 = 0x5C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved65 = 0x5D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved66 = 0x5E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved67 = 0x5F,
    /// <summary> Visual ISO/IEC 13818-2 Simple Profile. </summary>
    VisualIsoIec138182SimpleProfile = 0x60,
    /// <summary> Visual ISO/IEC 13818-2 Main Profile. </summary>
    VisualIsoIec138182MainProfile = 0x61,
    /// <summary> Visual ISO/IEC 13818-2 SNR Profile. </summary>
    VisualIsoIec138182SnrProfile = 0x62,
    /// <summary> Visual ISO/IEC 13818-2 Spatial Profile. </summary>
    VisualIsoIec138182SpatialProfile = 0x63,
    /// <summary> Visual ISO/IEC 13818-2 High Profile. </summary>
    VisualIsoIec138182HighProfile = 0x64,
    /// <summary> Visual ISO/IEC 13818-2 422 Profile. </summary>
    VisualIsoIec138182FourTwoTwoProfile = 0x65,
    /// <summary> Audio ISO/IEC 13818-7 Main Profile. </summary>
    AudioIsoIec138187MainProfile = 0x66,
    /// <summary> Audio ISO/IEC 13818-7 LowComplexity Profile. </summary>
    AudioIsoIec138187LowComplexityProfile = 0x67,
    /// <summary> Audio ISO/IEC 13818-7 Scaleable Sampling Rate Profile. </summary>
    AudioIsoIec138187ScaleableSamplingRateProfile = 0x68,
    /// <summary> Audio ISO/IEC 13818-3. </summary>
    AudioIsoIec138183 = 0x69,
    /// <summary> Visual ISO/IEC 11172-2. </summary>
    VisualIsoIec111722 = 0x6A,
    /// <summary> Audio ISO/IEC 11172-3. </summary>
    AudioIsoIec111723 = 0x6B,
    /// <summary> Visual ISO/IEC 10918-1. </summary>
    VisualIsoIec109181 = 0x6C,
    /// <summary> Portable Network Graphics. </summary>
    PortableNetworkGraphics = 0x6D,
    /// <summary> Visual ISO/IEC 15444-1 (JPEG 2000) . </summary>
    VisualIsoIec154441Jpeg2000 = 0x6E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved68 = 0x6F,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved69 = 0x70,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved70 = 0x71,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved71 = 0x72,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved72 = 0x73,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved73 = 0x74,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved74 = 0x75,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved75 = 0x76,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved76 = 0x77,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved77 = 0x78,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved78 = 0x79,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved79 = 0x7A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved80 = 0x7B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved81 = 0x7C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved82 = 0x7D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved83 = 0x7E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved84 = 0x7F,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved85 = 0x80,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved86 = 0x81,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved87 = 0x82,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved88 = 0x83,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved89 = 0x84,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved90 = 0x85,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved91 = 0x86,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved92 = 0x87,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved93 = 0x88,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved94 = 0x89,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved95 = 0x8A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved96 = 0x8B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved97 = 0x8C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved98 = 0x8D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved99 = 0x8E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved100 = 0x8F,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved101 = 0x90,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved102 = 0x91,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved103 = 0x92,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved104 = 0x93,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved105 = 0x94,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved106 = 0x95,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved107 = 0x96,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved108 = 0x97,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved109 = 0x98,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved110 = 0x99,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved111 = 0x9A,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved112 = 0x9B,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved113 = 0x9C,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved114 = 0x9D,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved115 = 0x9E,
    /// <summary> Reserved for ISO use. </summary>
    IsoIec14496Reserved116 = 0x9F,
    /// <summary> Enhanced Variable Rate Codec (EVRC) Voice </summary>
    EvrcVoice = 0xA0,
    /// <summary> Selectable Mode Vocoder (SMV) Voice  </summary>
    SmvVoice = 0xA1,
    /// <summary> 3GPP2 Compact Multimedia Format (CMF) </summary>
    Cmf = 0xA2,
    /// <summary> Society of Motion Picture and Television Engineers (SMPTE) VC-1 Video.</summary>
    SmpteVcVideo = 0xA3,
    /// <summary> Dirac Video Coder. </summary>
    DiracVideo = 0xA4,
    /// <summary> Withdrawn, unused, do not use (was AC-3)  </summary>
    Ac3Withdrawn = 0xA5,
    /// <summary> Withdrawn, unused, do not use (was Enhanced AC-3) </summary>
    EnhancedAc3Withdrawn = 0xA6,
    /// <summary> Dynamic Resolution Adaptation (DRA) Audio. </summary>
    DynamicResolutionAdaptationAudio = 0xA7,
    /// <summary> International Telecommunication Union (ITU) G.719 Audio. </summary>
    G719Audio = 0xA8,
    /// <summary> DTS-HD Core Substream.</summary>
    DtsHdCoreSubstream = 0xA9,
    /// <summary> DTS-HD Core Substream + Extension Substream.</summary>
    DtsHdCoreSubstreamExtensionSubstream = 0xAA,
    /// <summary> DTS-HD Extension Substream containing only XLL.</summary>
    DtsHdExtensionSubstreamXll = 0xAB,
    /// <summary> DTS-HD Extension Substream containing only LBR.</summary>
    DtsHdExtensionSubstreamLbr = 0xAC,
    /// <summary>Opus audio.</summary>
    OpusAudio = 0xAD,
    /// <summary> Withdrawn, unused, do not use (was AC-4).</summary>
    Ac4Withdrawn = 0xAE,
    /// <summary> Auro-Cx 3D audio.</summary>
    AuroCx3DAudio = 0xAF,
    /// <summary> RealVideo Codec 11.</summary>
    RealVideo11 = 0xB0,
    /// <summary> VP9 Video.</summary>
    Vp9Video = 0xB1,
    /// <summary> DTS-UHD profile 2. </summary>
    DtsUhdProfile2 = 0xB2,
    /// <summary> DTS-UHD profile 3 or higher.</summary>
    DtsUhdProfile3OrHigher = 0xB3,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority1 = 0xB4,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority2 = 0xB5,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority3 = 0xB6,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority4 = 0xB7,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority5 = 0xB8,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority6 = 0xB9,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority7 = 0xBA,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority8 = 0xBB,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority9 = 0xBC,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority10 = 0xBD,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority11 = 0xBE,
    /// <summary> Reserved for registration authority.</summary>
    ReservedForRegistrationAuthority12 = 0xBF,
    /// <summary> User private.</summary>
    UserPrivate1 = 0xC0,
    /// <summary> User private.</summary>
    UserPrivate2 = 0xC3,
    /// <summary> User private.</summary>
    UserPrivate3 = 0xC4,
    /// <summary> User private.</summary>
    UserPrivate4 = 0xC5,
    /// <summary> User private.</summary>
    UserPrivate6 = 0xC7,
    /// <summary> User private.</summary>
    UserPrivate7 = 0xC8,
    /// <summary> User private.</summary>
    UserPrivate8 = 0xC9,
    /// <summary> User private.</summary>
    UserPrivate9 = 0xCA,
    /// <summary> User private.</summary>
    UserPrivate10 = 0xCB,
    /// <summary> User private.</summary>
    UserPrivate11 = 0xCC,
    /// <summary> User private.</summary>
    UserPrivate12 = 0xCD,
    /// <summary> User private.</summary>
    UserPrivate13 = 0xCE,
    /// <summary> User private.</summary>
    UserPrivate14 = 0xCD,
    /// <summary> User private.</summary>
    UserPrivate15 = 0xCE,
    /// <summary> User private.</summary>
    UserPrivate16 = 0xCF,
    /// <summary> User private.</summary>
    UserPrivate17 = 0xD0,
    /// <summary> User private.</summary>
    UserPrivate18 = 0xD1,
    /// <summary> User private.</summary>
    UserPrivate19 = 0xD2,
    /// <summary> User private.</summary>
    UserPrivate20 = 0xD3,
    /// <summary> User private.</summary>
    UserPrivate21 = 0xD4,
    /// <summary> User private.</summary>
    UserPrivate22 = 0xD5,
    /// <summary> User private.</summary>
    UserPrivate23 = 0xD6,
    /// <summary> User private.</summary>
    UserPrivate24 = 0xD7,
    /// <summary> User private.</summary>
    UserPrivate25 = 0xD8,
    /// <summary> User private.</summary>
    UserPrivate26 = 0xD9,
    /// <summary> User private.</summary>
    UserPrivate27 = 0xDA,
    /// <summary> User private.</summary>
    UserPrivate28 = 0xDB,
    /// <summary> User private.</summary>
    UserPrivate29 = 0xDC,
    /// <summary> User private.</summary>
    UserPrivate30 = 0xDD,
    /// <summary> User private.</summary>
    UserPrivate31 = 0xDE,
    /// <summary> User private.</summary>
    UserPrivate32 = 0xDF,
    /// <summary> User private.</summary>
    UserPrivate33 = 0xE0,
    /// <summary>13K Voice. </summary>
    Voice13K = 0xE1,
    /// <summary> User private.</summary>
    UserPrivate34 = 0xE2,
    /// <summary> User private.</summary>
    UserPrivate35 = 0xE3,
    /// <summary> User private.</summary>
    UserPrivate36 = 0xE4,
    /// <summary> User private.</summary>
    UserPrivate37 = 0xE5,
    /// <summary> User private.</summary>
    UserPrivate38 = 0xE6,
    /// <summary> User private.</summary>
    UserPrivate39 = 0xE7,
    /// <summary> User private.</summary>
    UserPrivate40 = 0xE8,
    /// <summary> User private.</summary>
    UserPrivate41 = 0xE9,
    /// <summary> User private.</summary>
    UserPrivate42 = 0xEA,
    /// <summary> User private.</summary>
    UserPrivate43 = 0xEB,
    /// <summary> User private.</summary>
    UserPrivate44 = 0xEC,
    /// <summary> User private.</summary>
    UserPrivate45 = 0xED,
    /// <summary> User private.</summary>
    UserPrivate46 = 0xEE,
    /// <summary> User private.</summary>
    UserPrivate47 = 0xEF,
    /// <summary> User private.</summary>
    UserPrivate48 = 0xF0,
    /// <summary> User private.</summary>
    UserPrivate49= 0xF1,
    /// <summary> User private.</summary>
    UserPrivate50 = 0xF2,
    /// <summary> User private.</summary>
    UserPrivate51 = 0xF3,
    /// <summary> User private.</summary>
    UserPrivate52 = 0xF4,
    /// <summary> User private.</summary>
    UserPrivate53 = 0xF5,
    /// <summary> User private.</summary>
    UserPrivate54 = 0xF6,
    /// <summary> User private.</summary>
    UserPrivate55 = 0xF7,
    /// <summary> User private.</summary>
    UserPrivate56 = 0xF8,
    /// <summary> User private.</summary>
    UserPrivate57 = 0xF9,
    /// <summary> User private.</summary>
    UserPrivate58 = 0xFA,
    /// <summary> User private.</summary>
    UserPrivate59 = 0xFB,
    /// <summary> User private.</summary>
    UserPrivate60 = 0xFC,
    /// <summary> User private.</summary>
    UserPrivate61 = 0xFD,
    /// <summary> User private.</summary>
    UserPrivate62 = 0xFE,
    /// <summary> No object type specified.
    /// <para>Streams with this value with a StreamType indicating a systems stream (values 1,2,3, 6, 7, 8, 9)
    /// shall be treated as if the ObjectTypeIndication had been set to 0x01.</para></summary>
    None = 0xFF

}