using System.Diagnostics;

namespace Vipl.Base;
/// <summary>
/// Matrix values which occur in the headers specify a transformation of video images for presentation. Not all
/// derived specifications use matrices; if they are not used, they shall be set to the identity matrix, If a matrix is
/// used, the point (p,q) is transformed into (p', q') using the matrix as follows
/// <code>
///           | a b u | 
/// (p q 1) * | c d v | = (m n z)
///           | x y w |
/// m = ap + cq + x; n = bp + dq + y; z = up + vq + w;
/// p' = m/z; q' = n/z
///</code>
/// The coordinates {p,q} are on the decompressed frame, and {p’, q’} are at the rendering output. Therefore, for
/// example, the matrix {2,0,0, 0,2,0, 0,0,1} exactly doubles the pixel dimension of an image. The co-ordinates
/// transformed by the matrix are not normalized in any way, and represent actual sample locations. Therefore
/// {x,y} can, for example, be considered a translation vector for the image. <br/>
/// <br/>
/// The co-ordinate origin is located at the upper left corner, and X values increase to the right, and Y values
/// increase downwards. {p,q} and {p’,q’} are to be taken as absolute pixel locations relative to the upper left
/// hand corner of the original image (after scaling to the size determined by the track header's width and height)
/// and the transformed (rendering) surface, respectively.
/// </summary>
[DebuggerDisplay("[\\{{A.Value},{B.Value},{U.Value}\\},\\{{C.Value},{D.Value},{V.Value}\\}, \\{{X.Value},{Y.Value},{W.Value}\\}]")]
public struct TransformationMatrix
{
    /// <summary>
    /// Create identity transformation matrix.
    /// </summary>
    public TransformationMatrix()
    {
        A = 1M; B = 0; U = 0;
        C = 0; D = 1M; V = 0;
        X = 0; Y = 0; W = 1M;
    }
    /// <summary>
    /// A coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 A { get; set; }
    /// <summary>
    /// B coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 B { get; set; }
    /// <summary>
    /// U coordinate in identity matrix.
    /// </summary>
    public FixedPoint2_30 U { get; set; }
    /// <summary>
    /// C coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 C { get; set; }
    /// <summary>
    /// D coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 D { get; set; } 
    /// <summary>
    /// V coordinate in identity matrix.
    /// </summary>
    public FixedPoint2_30 V { get; set; }
    /// <summary>
    /// X coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 X { get; set; }
    /// <summary>
    /// Y coordinate in identity matrix.
    /// </summary>
    public FixedPoint16_16 Y { get; set; }
    /// <summary>
    /// W coordinate in identity matrix.
    /// </summary>
    public FixedPoint2_30 W { get; set; }

    /// <summary> Transformation matrix as <see cref="ByteVector"/> value. </summary>
    public ByteVector ByteVectorValue
    {
        get => new ByteVectorBuilder(36){this}.Build();
        set
        {
            A = value[..4];    B = value[4..8];   U = value[8..12];
            C = value[12..16]; D = value[16..20]; V = value[20..24];
            X = value[24..28]; Y = value[28..32]; W = value[32..36];
        }
    }
    /// <summary> Convert <see cref="TransformationMatrix"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this transformation matrix value</returns>
    public static implicit operator Span<byte> (TransformationMatrix v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="TransformationMatrix"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="TransformationMatrix"/> represented with this transformation matrix value</returns>
    public static implicit operator TransformationMatrix( Span<byte> v) => new()
    {
        A = v[..4],    B = v[4..8],   U = v[8..12],
        C = v[12..16], D = v[16..20], V = v[20..24],
        X = v[24..28], Y = v[28..32], W =v[32..36]
    };
}