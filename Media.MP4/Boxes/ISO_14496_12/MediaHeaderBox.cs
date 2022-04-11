using System.Diagnostics;
using System.Globalization;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 MediaHeaderBox.
/// <para>The media header declares overall information that is media-independent, and relevant to
/// characteristics of the media in a track.</para> </summary>
[HasBoxFactory("mdhd")]
public class MediaHeaderBox : FullBoxWithData
{
    private MediaHeaderBox (BoxHeader header, IsoHandlerBox? handler)
        : base (header,  handler)
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
                Timescale = value[16..20].ToUInt();
                Duration = value[20..28].ToTimeSpan(Timescale);
                offset = 28;
            }
            else
            {
                CreationTime = value[..4].ToDateTime();
                ModificationTime = value[4..8].ToDateTime();
                Timescale = value[8..12].ToUInt();
                Duration = value[12..16].ToTimeSpan(Timescale);
                offset = 16;
            }
            LanguageAsPacked3LetterCode = value[offset..(offset + 2)].ToShort();
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
                .Add(Timescale)
                .AddLong(Duration, Timescale);
        }
        else
        {
            builder.Add(CreationTime)
                .Add(ModificationTime)
                .Add(Timescale)
                .Add(Duration, Timescale);
        }

        return builder.Add(LanguageAsPacked3LetterCode)
            .Clear(2);
    }

    /// <summary> Creation time of the presentation. </summary>
    public DateTime CreationTime { get; set; }
	
    /// <summary> Most recent time the presentation was modified. </summary>
    public DateTime ModificationTime { get; set; }

    private uint _timescale;
    /// <summary>
    ///	The timescale for the media. This is the number of time units that pass in one second. For example,
    /// if the timescale is "10000000", one second is equal to 100,000 units. A time coordinate measured in units
    /// is multiplied by the timescale to obtain the number of time units. For example, a time coordinate of 35 would be
    /// 35 * (10000000) = 35 seconds. The timescale is a required field in the header of either an Initial Object
    /// Descriptor or a Media Descriptor.
    /// </summary>
    public uint Timescale
    {
        get => _timescale;
        set
        {
            Duration = Duration * _timescale / (value == 0 ? 1 : value);
            UpdateScaling(Header.File?.Boxes ?? new List<Box>(), _timescale, value );
            _timescale = value;
        }
    }
    private void UpdateScaling(IList<Box> boxes, uint old, uint @new)
    {
        foreach (var box in boxes)
        {
            if (box is ContainerBox boxWithChildren)
            {
                UpdateScaling(boxWithChildren.Children, old, @new);
            }

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (box is IBoxMediaHeaderScalableProperties boxWithScalableProperties)
            {
                boxWithScalableProperties.ChangeTimescale(old, @new);
            }
        }
    }

    /// <summary>The duration of the movie, in movie timescale units. </summary>
    public TimeSpan Duration { get; set; }
	
    /// <inheritdoc />
    public override ulong ActualDataSize => Version == 1 ? 32UL : 20UL;
	
    /// <summary> Language code for this media, as a packed three-character code defined in
    /// ISO 639-2. Each character is packed as the difference between its ASCII value and 0x60. Since the
    /// code is confined to being three lower-case letters, these values are strictly positive. </summary>
    public short LanguageAsPacked3LetterCode { get; set; }
    /// <summary> Language code for this media. </summary>
    public CultureInfo? Language
    {
        get
        {
            var lang = new char[3];
            lang[2] = (char)((LanguageAsPacked3LetterCode & 0x1F) + 0x60);
            lang[1] = (char)(((LanguageAsPacked3LetterCode >> 5) & 0x1F) + 0x60);
            lang[0] = (char)(((LanguageAsPacked3LetterCode >> 10) & 0x1F) + 0x60);
            return CultureInfo
                .GetCultures(CultureTypes.NeutralCultures)
                .FirstOrDefault(ci => string.Equals(ci.ThreeLetterISOLanguageName, new string(lang), StringComparison.OrdinalIgnoreCase));
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            LanguageAsPacked3LetterCode = (short)((value.ThreeLetterISOLanguageName[2] - 0x60) |
                                                  ((value.ThreeLetterISOLanguageName[1] - 0x60) << 5) |
                                                  ((value.ThreeLetterISOLanguageName[0] - 0x60) << 10));
        }
    }
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
       => $"{base.DebugDisplay(level)} L: {Language?.ThreeLetterISOLanguageName ?? ""}, D: {Duration}, T: {Timescale}  C={CreationTime}, M={ModificationTime}";

}