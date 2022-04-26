using System.Diagnostics;
using Vipl.Base;
using Vipl.Base.Extensions;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary> This class extends <see cref="FullBox" /> to provide an implementation of a ISO/IEC 14496-12 SampleToGroupBox.
/// <para>This table can be used to find the group that a sample belongs to and the associated description of that
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
[HasBoxFactory("sbgp")]
public class SampleToGroupBox  : FullBoxWithData
{
    private SampleToGroupBox  (BoxHeader header, HandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data {
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            GroupingType = value[..4].ToUInt();
            var offset = 4;
            if (Version == 1)
            {
                GroupingTypeParameter = value[4..8].ToUInt();
                offset = 8;
            }
                
            var count = value[offset..(offset +4)].ToUInt();
            offset += 4;
            Entries.Clear();
            
            for (var i = 0U; i < count; i++)
            {
                var entry = value[offset..(offset + 8)];
                Entries.Add(new Entry()
                {
                    SampleCount = entry[..4].ToUInt(),
                    GroupDescriptionIndex = entry[4..8].ToUInt(),
                } );
                offset += 8;
            }
            
            Debug.Assert(Data == value);
        } 
    }

    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(GroupingType);
        if (Version == 1 )
            builder.Add(GroupingTypeParameter ?? 0);
        builder.Add(EntryCount);
        foreach (var entry in Entries)
        {
            builder.Add(entry.SampleCount)
                .Add(entry.GroupDescriptionIndex);
        }
        return builder;
    }
    /// <summary> An integer that identifies the type (i.e. criterion used to form the sample groups)
    /// of the sample grouping and links it to its sample group description table with the same value for
    /// <see cref="GroupingType"/>. At most one occurrence of this box with the same value for <see cref="GroupingType"/> (and,
    /// if used,  <see cref="GroupingTypeParameter"/>) shall exist for a track. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint GroupingType { get; set; }
    
    /// <summary> An indication of the sub-type of the grouping.</summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint? GroupingTypeParameter { get; set; }
    
    /// <summary>  Integer that gives the number of entries in the following table </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public uint EntryCount => (uint)Entries.Count;

    /// <summary>TimeToSampleBox entries.</summary>
    public class Entry
    {

        /// <summary>An integer that gives the number of consecutive samples with the same sample group
        /// descriptor. It is an error for the total in this box to be greater than the sample_count documented
        /// elsewhere, and the reader behaviour would then be undefined. If the sum of the sample count in
        /// this box is less than the total sample count, or there is no SampleToGroupBox that applies to some
        /// samples (e.g. it is absent from a track fragment), then those samples are associated with the group
        /// identified by the default_group_description_index in the SampleGroupDescriptionBox, if any, or
        /// else with no group.</summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public uint SampleCount { get; set; }
        
        /// <summary>An integer that gives the index of the sample group entry which describes
        /// the samples in this group. The index ranges from 1 to the number of sample group entries in the
        /// SampleGroupDescriptionBox, or takes the value 0 to indicate that this sample is a member of no
        /// group of this type. </summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public uint GroupDescriptionIndex { get; set; }
        
        /// <summary> Debug string use to print during debug. </summary>
        /// <returns>Debug string</returns>
        public  string DebugDisplay()
            => $"SC: {SampleCount}, GDI: {GroupDescriptionIndex}";
    }

    /// <summary> Segment entries in the SampleToChunkBox. </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public List<Entry> Entries { get;  } = new ();
    
    /// <inheritdoc />
    public override ulong ActualDataSize => 8UL + (Version == 1 ? 4UL : 0) + EntryCount * 8;
    
    /// <inheritdoc />
    public override string DebugDisplay(int level)
        => $"{base.DebugDisplay(level)} GT: {GroupingType} GTP: {(long?)GroupingTypeParameter ?? -1} C: {EntryCount} ({new string(Entries.Select(e=> e.DebugDisplay()).Join("), (").Take(50).ToArray())}...)";

}