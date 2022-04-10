using System.Diagnostics;
using System.Text;
using Vipl.Base;
using Vipl.Base.Extensions;
using Vipl.Media.Core;

namespace Vipl.Media.MP4.Boxes.ISO_14496_12;

/// <summary>  This class extends <see cref="Box" /> to provide an implementation of a ISO/IEC 14496-12 FileTypeBox.
/// The FileTypeBox contains a major brand and minor version number. It also contains a list of compatible brands.
/// The compatible brands are used to identify compatible renderers.   </summary>
[HasBoxFactory("ftyp")]
public class FileTypeBox : BoxWithData
{
    private FileTypeBox (BoxHeader header,  IsoHandlerBox? handler)
        : base (header, handler)
    {
    }
    
    /// <inheritdoc />
    public override ByteVector Data 
    { 
        get => RenderData(new ByteVectorBuilder((int)ActualDataSize)).Build();
        set
        {
            MajorBrand = value[..4].ToInt();
            MinorVersion = value[4..8].ToInt();
            CompatibleBrands.Clear();
            for (var i = 8; i < value.Count; i += 4)
            {
                CompatibleBrands.Add(value.Mid(i, 4).ToInt());
            }
            Debug.Assert(Data == value);
        } 
    }

    /// <summary>Brand identifier. </summary>
    public int MajorBrand { get; set; }
    /// <summary>Brand identifier as string. </summary>
    public string MajorBrandAsString
    {
        get => MajorBrand.ToByteVector().ToString(Encoding.UTF8).Trim();
        set
        {
            if (value.Length != 3 || value.Length != 4)
            {
                throw new ArgumentException("Brand must be 3 or 4 bytes");
            }
            MajorBrand = BoxType.FixId(value.ToByteVector(Encoding.UTF8)).ToInt();
        }
    }
    /// <summary>An informative integer for the minor version of the major brand. </summary>
    public int MinorVersion { get; set; }
    /// <summary>A list of compatible brands.</summary>
    public IList<int> CompatibleBrands { get; set; } = new List<int>();
    /// <summary>A list of compatible brands as list of strings string.</summary>
    public IList<string> CompatibleBrandsAsString
    {
        get =>
            new ListTypeConversionWrapper<int, string>(CompatibleBrands, 
                i => i.ToByteVector().ToString(Encoding.UTF8).Trim(), 
                s =>
                {
                    if (s.Length != 3 || s.Length != 4)
                    {
                        throw new ArgumentException("Brand must be 3 or 4 bytes");
                    }
                        
                    return BoxType.FixId(s.ToByteVector(Encoding.UTF8)).ToInt();
                });
        set
        {
            if (value.Any(b => b.Length != 3 || b.Length != 4))
            {
                throw new ArgumentException("Brand must be 3 or 4 bytes");
            }

            CompatibleBrands = value.Select(b => BoxType.FixId(b.ToByteVector(Encoding.UTF8)).ToInt()).ToArray();
        }
    }
    /// <inheritdoc />
    public override IByteVectorBuilder RenderData(IByteVectorBuilder builder)
    {
        builder.Add(MajorBrand)
            .Add(MinorVersion);
        CompatibleBrands.ForEach(c => builder.Add(c));
        return builder;
    }
    /// <inheritdoc />
    public override ulong ActualDataSize => (ulong) (8 + CompatibleBrands.Count * 4);
}