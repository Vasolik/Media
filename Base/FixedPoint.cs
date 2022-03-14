using System.Diagnostics;
using System.Runtime.InteropServices;
using Vipl.Base.Extensions;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Vipl.Base;

[StructLayout(LayoutKind.Explicit)]
internal struct ShortUShort
{
    [FieldOffset(0)]
    public short ShortValue;
    [FieldOffset(0)]
    public ushort UShortValue;
}

/// <summary>
/// Fixed-point numbers are signed values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 8.8 fixed-point number is formed by dividing a 16-bit integer by 0x100
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct FixedPoint8_8
{
    private ShortUShort _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value.ShortValue / 0x100 ; set => _value.ShortValue = (short) (value * 0x100); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public ushort BinaryRepresentation { get => _value.UShortValue; set => _value.UShortValue = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.UShortValue.ToByteVector(); set => _value.UShortValue = value.ToUShort(); }
    /// <summary> Convert <see cref="FixedPoint8_8"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(FixedPoint8_8 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="FixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint16_16"/> represented with this fix point value</returns>
    /// 
    public static implicit operator FixedPoint8_8( decimal v) => new(){ Value = v};
    /// <summary> Convert <see cref="FixedPoint8_8"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (FixedPoint8_8 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="FixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint8_8( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="FixedPoint8_8"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (FixedPoint8_8 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="FixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint8_8"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint8_8( Span<byte> v)
    {
        var result = new FixedPoint8_8();
        result._value.UShortValue = v.ToUShort();
        return result;
    }
}
/// <summary>
/// Fixed-point numbers are signed or unsigned values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 8.8 fixed-point number is formed by dividing a 16-bit integer by 0x100
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct UFixedPoint8_8
{
    private ushort _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value /  0x100 ; set => _value = (ushort) (value *  0x100); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public ushort BinaryRepresentation { get => _value; set => _value = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.ToByteVector(); set => _value = value.ToUShort(); }
    
    /// <summary> Convert <see cref="UFixedPoint8_8"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(UFixedPoint8_8 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="UFixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="UFixedPoint8_8"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint8_8( decimal v) => new(){ Value = v};
    
    /// <summary> Convert <see cref="UFixedPoint8_8"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (UFixedPoint8_8 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="UFixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint8_8( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="UFixedPoint8_8"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (UFixedPoint8_8 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="UFixedPoint8_8"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="UFixedPoint8_8"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint8_8( Span<byte> v) => new(){ _value = v.ToUShort() };
}

[StructLayout(LayoutKind.Explicit)]
internal struct IntUint
{
    [FieldOffset(0)]
    public int IntValue;
    [FieldOffset(0)]
    public uint UIntValue;
}
/// <summary>
/// Fixed-point numbers are signed values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 16.16 fixed-point number is formed by dividing a 32-bit integer by 0x10000
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct FixedPoint16_16
{
    private IntUint _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value.IntValue / 0x10000 ; set => _value.IntValue = (int) (value * 0x10000); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public uint BinaryRepresentation { get => _value.UIntValue; set => _value.UIntValue = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.UIntValue.ToByteVector(); set => _value.UIntValue = value.ToUInt(); }
    /// <summary> Convert <see cref="FixedPoint16_16"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(FixedPoint16_16 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="FixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint16_16"/> represented with this fix point value</returns>
    /// 
    public static implicit operator FixedPoint16_16( decimal v) => new(){ Value = v};
    /// <summary> Convert <see cref="FixedPoint16_16"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (FixedPoint16_16 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="FixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint16_16( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="FixedPoint16_16"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (FixedPoint16_16 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="FixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint16_16"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint16_16( Span<byte> v)
    {
        var result = new FixedPoint16_16();
        result._value.UIntValue = v.ToUInt();
        return result;
    }
}
/// <summary>
/// Fixed-point numbers are signed or unsigned values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 30.2 fixed-point number is formed by dividing a 32-bit integer by 4
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct UFixedPoint16_16
{
    private uint _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value /  0x10000 ; set => _value = (uint) (value *  0x10000); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public uint BinaryRepresentation { get => _value; set => _value = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.ToByteVector(); set => _value = value.ToUInt(); }
    
    /// <summary> Convert <see cref="UFixedPoint16_16"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(UFixedPoint16_16 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="UFixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="UFixedPoint2_30"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint16_16( decimal v) => new(){ Value = v};
    
    /// <summary> Convert <see cref="UFixedPoint16_16"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (UFixedPoint16_16 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="UFixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint16_16( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="UFixedPoint16_16"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (UFixedPoint16_16 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="UFixedPoint16_16"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="UFixedPoint16_16"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint16_16( Span<byte> v) => new(){ _value = v.ToUInt() };
}


/// <summary>
/// Fixed-point numbers are signed values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 30.2 fixed-point number is formed by dividing a 32-bit integer by 4
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct FixedPoint2_30
{
    private IntUint _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value.IntValue / (1U<<30) ; set => _value.IntValue = (int) (value * (1U<<30)); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public uint BinaryRepresentation { get => _value.UIntValue; set => _value.UIntValue = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.UIntValue.ToByteVector(); set => _value.UIntValue = value.ToUInt(); }
    /// <summary> Convert <see cref="FixedPoint2_30"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(FixedPoint2_30 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="FixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint2_30"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint2_30( decimal v) => new(){ Value = v};
    
    /// <summary> Convert <see cref="FixedPoint2_30"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (FixedPoint2_30 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="FixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint2_30( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="FixedPoint2_30"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (FixedPoint2_30 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="FixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="FixedPoint2_30"/> represented with this fix point value</returns>
    public static implicit operator FixedPoint2_30( Span<byte> v)
    {
        var result = new FixedPoint2_30();
        result._value.UIntValue = v.ToUInt();
        return result;
    }
}
/// <summary>
/// Fixed-point numbers are signed or unsigned values resulting from dividing an integer by an appropriate power
/// of 2. For example, a 30.2 fixed-point number is formed by dividing a 32-bit integer by 4
/// </summary>
[DebuggerDisplay("{Value}")]
// ReSharper disable once InconsistentNaming
public struct UFixedPoint2_30
{
    private uint _value;
    /// <summary> Decimal value represented with this fix point value. </summary>
    public Decimal Value { get =>(decimal)_value / (1U<<30) ; set => _value = (uint) (value * (1U<<30)); }
    /// <summary>Binary representation (as unsigned integer) of this fix point value. </summary>
    public uint BinaryRepresentation { get => _value; set => _value = value; }
    /// <summary>Binary representation (as <see cref="ByteVector"/>) of this fix point value. </summary>
    public ByteVector ByteVectorValue { get => _value.ToByteVector(); set => _value = value.ToUInt(); }
    
    /// <summary> Convert <see cref="UFixedPoint2_30"/> to <see cref="decimal"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="Decimal"/> represented with this fix point value</returns>
    public static implicit operator decimal(UFixedPoint2_30 v) => v.Value;
    /// <summary> Convert <see cref="decimal"/> to <see cref="UFixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="UFixedPoint2_30"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint2_30( decimal v) => new(){ Value = v};
    
    /// <summary> Convert <see cref="UFixedPoint2_30"/> to <see cref="ByteVector"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator ByteVector (UFixedPoint2_30 v) => v.ByteVectorValue;
    /// <summary> Convert <see cref="ByteVector"/> to <see cref="UFixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="ByteVector"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint2_30( ByteVector v) => new(){ ByteVectorValue = v };
    
    /// <summary> Convert <see cref="UFixedPoint2_30"/> to <see cref="T:Span{Bytes}"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator Span<byte> (UFixedPoint2_30 v) => v.ByteVectorValue[..];
    /// <summary> Convert <see cref="T:Span{Bytes}"/> to <see cref="UFixedPoint2_30"/> value. </summary>
    /// <param name="v">Value to convert.</param>
    /// <returns><see cref="T:Span{Bytes}"/> represented with this fix point value</returns>
    public static implicit operator UFixedPoint2_30( Span<byte> v) => new(){ _value = v.ToUInt() };
}