using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.MP4.Boxes.ISO_14496_12;

namespace Vipl.Media.MP4.Boxes.Adobe;

/// <summary>  This class extends <see cref="FullBox" /> to provide an implementation of a Adobe FLV ChapterBox.
/// <para>The optional chpl box allows an F4V file to specify individual chapters along the main
/// timeline of an F4V file. The information in this box is provided to ActionScript. The <see cref="ChapterBox"/> box
/// occurs within a <see cref="MovieBox"/> box.</para>
/// </summary>
[HasBoxFactory("chpl")]
public class ChapterBox : FullBoxWithData
{
    
    private ChapterBox (BoxHeader header,  HandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            var count = value[4];
            var currentReadPosition = 5;
            Chapters.Clear();
            for (var i = 0; i < count; i++)
            {
                var size = value[currentReadPosition + 8];
                Chapters.Add((TimeSpan.FromTicks(value.Mid(currentReadPosition, 8).ToLong()), value.Mid(currentReadPosition + 9, size).ToString(Encoding.UTF8)));
                currentReadPosition += 9 + size;
            }

            Debug.Assert(value == Data);
        } 
    }
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Clear(4)
            .Add((byte) Chapters.Count);
        foreach (var (location, name) in Chapters)
        {
            byte size = (byte)(Encoding.UTF8.GetByteCount(name));
            builder.Add(location.Ticks)
                .Add(size)
                .Add(name);
        }

        return builder;
    }
    /// <inheritdoc />
    public override ulong ActualDataSize => 5UL + Chapters.Aggregate(0UL, (sum, chapter) => sum + (ulong)Encoding.UTF8.GetByteCount(chapter.Name) + 9);

    /// <summary> Chapters in this media file. </summary>
    public IList<(TimeSpan Location, string Name)> Chapters { get; set; } = new List<(TimeSpan, string)>();
    
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
    {
        return base.DebugDisplay(level) + "\n" + Chapters.Select(c => $"{c.Item1} {c.Item2}").Join("\n").Intend(level + 1, true);
    }
}