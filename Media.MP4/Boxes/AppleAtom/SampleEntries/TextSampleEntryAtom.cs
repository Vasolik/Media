using System.Diagnostics;
using System.Drawing;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.AppleAtom.SampleEntries;

/// <summary> This class extends <see cref="Box" /> to provide an implementation of a Apple QuickTime ChapterTrackReferenceTypeBox.
/// <para>The text sample description contains information that defines how to interpret text media data.</para></summary>
[HasBoxFactory("text",  typeof(SampleDescriptionBox))]
public class TextSampleEntryAtom : SampleEntryBox
{
    private TextSampleEntryAtom (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    /// <summary> Flags that describe how the text should be drawn. </summary>
    [Flags]
    public enum DisplayFlagsType : uint
    {
        /// <summary> No flags are set. </summary>
        NoFlags = 0x0001,
        /// <summary> Controls text scaling. If this flag is set to 1, the text media handler
        /// re-flows the text instead of scaling when the track is scaled. This flag’s value is 0x0002 </summary>
        DontAutoScale = 0x0002,
        /// <summary> Controls background color. If this flag is set to 1, the text media handler ignores the background
        /// color field in the text sample description and uses the movie’s background color instead. This
        /// flag’s value is 0x0008. </summary>
        UseMovieBackgroundColor = 0x0008,
        /// <summary> Controls text scrolling. If this flag is set to 1, the text media handler scrolls the text until the last
        /// of the text is in view. This flag’s value is 0x0020. </summary>
        ScrollIn = 0x0020,
        /// <summary>Controls text scrolling. If this flag is set to 1, the text media handler scrolls the text until the last
        /// of the text is gone. This flag’s value is 0x0040. </summary>
        ScrollOut = 0x0040,
        /// <summary>Controls text scrolling. If this flag is set to 1, the text media handler scrolls the text horizontally;
        /// otherwise, it scrolls the text vertically. This flag’s value is 0x0080. </summary>
        HorizontalScroll = 0x0080,
        /// <summary>Controls text scrolling. If this flag is set to 1, the text media handler scrolls down (if scrolling
        /// vertically) or backward (if scrolling horizontally; note that horizontal scrolling also depends upon
        /// text justification). This flag’s value is 0x0100. </summary>
        ReverseScroll = 0x0100,
        /// <summary>Controls text scrolling. If this flag is set to 1, the text media handler displays new samples by
        /// scrolling out the old ones. This flag’s value is 0x0200. </summary>
        ContinuousScroll = 0x0200,
        /// <summary> Controls drop shadow. If this flag is set to 1, the text media handler displays the text with a drop
        /// shadow. This flag’s value is 0x1000. </summary>
        DropShadow = 0x1000,
        /// <summary>Controls anti-aliasing. If this flag is set to 1, the text media handler uses anti-aliasing when drawing
        /// text. This flag’s value is 0x2000. </summary>
        AntiAlias = 0x2000,
        /// <summary>Controls background color. If this flag is set to 1, the text media handler does not display the
        /// background color, so that the text overlay background tracks. This flag’s value is 0x4000. </summary>
        KeyedText = 0x4000
    }
    
    /// <summary> Indicates how the text should be aligned. Set this field to 0 for left-justified text,
    /// to 1 for centered tex   t, and to –1 for right-justified text. </summary>
    public enum HorizontalAlignmentType : sbyte
    {
        /// <summary> Left-justified text. </summary>
        Left = 0,
        /// <summary> Centered text. </summary>
        Center = 1,
        /// <summary> Right-justified text. </summary>
        Right = -1
    }
    /// <summary> Indicates how the text should be aligned. Set this field to 0 for top-justified text,
    /// to 1 for centered text, and to –1 for bottom-justified text. </summary>
    public enum VerticalAlignmentType : sbyte
    {
        /// <summary> Top-justified text. </summary>
        Top = 0,
        /// <summary> Centered text. </summary>
        Center = 1,
        /// <summary> Bottom-justified text. </summary>
        Bottom = -1
    }
    /// <summary> Font styles. </summary>
    [Flags]
    public enum FontStyleType : sbyte
    {
        /// <summary> Regular font style. </summary>
        Regular = 0x00,
        /// <summary> Bold font style. </summary>
        Bold = 0x01,
        /// <summary> Italic font style. </summary>
        Italic = 0x02,
        /// <summary> Underline font style. </summary>
        Underline = 0x04,
        /// <summary> Outline font style. </summary>
        Outline = 0x08,
        /// <summary> Shadow font style. </summary>
        Shadow = 0x10,
        /// <summary> Condensed font style. </summary>
        Condensed = 0x20,
        /// <summary> Extended font style. </summary>
        Extended = 0x40
    }
    
    /// <summary> Flags that describe how the text should be drawn. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public DisplayFlagsType DisplayFlags { get; set; }
    
    /// <summary> Indicates how the text should be aligned. Set this field to 0 for left-justified text,
    /// to 1 for centered tex   t, and to –1 for right-justified text. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public HorizontalAlignmentType HorizontalAlignment { get; set; }
    
    /// <summary> Indicates how the text should be aligned. Set this field to 0 for top-justified text,
    /// to 1 for centered text, and to –1 for bottom-justified text. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public VerticalAlignmentType VerticalAlignment { get; set; }
    
    /// <summary> Color that specifies the text’s background color </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public Color BackgroundColor { get; set; }
    
    /// <summary> Rectangle that specifies an area to receive text (top, left, bottom, right). Typically this field is set
    /// to all zeros. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort TopTextBox { get; set; }
    
    /// <summary> Rectangle that specifies an area to receive text (top, left, bottom, right). Typically this field is set
    /// to all zeros. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort LeftTextBox { get; set; }
    
    /// <summary> Rectangle that specifies an area to receive text (top, left, bottom, right). Typically this field is set
    /// to all zeros. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort BottomTextBox { get; set; }
    
    /// <summary> Rectangle that specifies an area to receive text (top, left, bottom, right). Typically this field is set
    /// to all zeros. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort RightTextBox { get; set; }
    
    /// <summary> Start character of the text to be displayed. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort StartChar { get; set; }
    
    /// <summary>End character of the text to be displayed. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort EndChar { get; set; }
    
    /// <summary> Font ID. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ushort FontID { get; set; }
    
    /// <summary> Font style. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public FontStyleType FontStyle { get; set; }
    
    /// <summary> Font size. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public byte FontSize { get; set; }
    
    /// <summary> Font color. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public Color FontColor { get; set; }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            DisplayFlags = (DisplayFlagsType)value[8..12].ToUInt();
            HorizontalAlignment = (HorizontalAlignmentType)value[12];
            VerticalAlignment = (VerticalAlignmentType)value[13];
            BackgroundColor = Color.FromArgb( value[15], value[16], value[17], value[14]);
            TopTextBox = value[18..20].ToUShort();
            LeftTextBox = value[20..22].ToUShort();
            BottomTextBox = value[22..24].ToUShort();
            RightTextBox = value[24..26].ToUShort();
            StartChar = value[26..28].ToUShort();
            EndChar = value[28..30].ToUShort();
            FontID = value[30..32].ToUShort();
            FontStyle = (FontStyleType)value[32];
            FontSize = value[33];
            FontColor = Color.FromArgb( value[35], value[36], value[37], value[34]); 
            base.Data = value;
            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        return base.RenderData(builder)
            .Add((uint) DisplayFlags)
            .Add((byte) HorizontalAlignment)
            .Add((byte) VerticalAlignment)
            .Add(BackgroundColor.R).Add(BackgroundColor.G).Add(BackgroundColor.B).Add(BackgroundColor.A)
            .Add(TopTextBox)
            .Add(LeftTextBox)
            .Add(BottomTextBox)
            .Add(RightTextBox)
            .Add(StartChar)
            .Add(EndChar)
            .Add(FontID)
            .Add((byte) FontStyle)
            .Add(FontSize)
            .Add(FontColor.R).Add(FontColor.G).Add(FontColor.B).Add(FontColor.A);
    }
    
    /// <summary> Actual size of the box in the file. This is the size of the header plus the size of the data.
    /// Compared to <see cref="Box.DataSize"/>, this value is calculated after every change of data. </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public override ulong ActualDataSize => 38UL;
    
    /// <inheritdoc />
    public override ulong ChildrenPosition => DataPosition + 38UL;
    
    /// <inheritdoc />
    public override ulong  ChildrenSize => Header.DataSize - 38UL;
    
    /// <inheritdoc />
    public override ulong DataSize => 38UL;
    
    /// <inheritdoc />
    protected override string DebugDisplayMoreData() 
        => $"DF: {DisplayFlags}, HA: {HorizontalAlignment}, VA: {VerticalAlignment}, BG: 0x{BackgroundColor.ToArgb():x8}, " +
           $"T: {TopTextBox}, L: {LeftTextBox}, B: {BottomTextBox}, R: {RightTextBox}, S: {StartChar}, E: {EndChar}, " +
           $"FID: {FontID}, FS: {FontStyle}, FSz: {FontSize}, FC: 0x{FontColor.ToArgb():x8}";


}