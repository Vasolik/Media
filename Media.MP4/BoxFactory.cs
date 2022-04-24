using System.Reflection;
using Vipl.Base;
using Vipl.Media.Core;
using Vipl.Media.MP4.Boxes;
using Vipl.Media.MP4.Boxes.ISO_14496_12.DataEntries;

namespace Vipl.Media.MP4;

/// <summary>Attribute used to mark classes that can be build using factory. </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class HasBoxFactoryAttribute : Attribute
{
	/// <summary> Construct instance which describe which factory should be used. </summary>
	/// <param name="type">Type used to indicate factory.</param>
	/// <param name="parentType">In which parent this container is expected.</param>
	/// <param name="handlerType">Handler to understand which factory should be used.</param>
	public HasBoxFactoryAttribute(string? type, Type? parentType = null, string? handlerType = null)
	{
		if (type is not null)
		{
			Type = BoxType.FromString(type);
		}
		if (parentType is not null)
		{
			Parent = parentType;
		}

		if (handlerType is not null)
		{
			HandleType = ByteVector.FromString(handlerType);
		}
		
		
		
	}
	/// <summary>Type used to indicate factory.</summary>
	public BoxType? Type { get; }
	/// <summary>In which parent this container is expected.</summary>
	public ByteVector? HandleType { get; init; }
	/// <summary>Handler to understand which factory should be used.</summary>
	public Type? Parent { get; init; }
}

/// <summary> This static class provides support for reading boxes from a file. </summary>
public static class BoxFactory
{

	private static readonly IDictionary<(BoxType? Type, Type? Parent,  ByteVector? HandleType), Func<BoxHeader, MP4, IsoHandlerBox?, Task<Box>>>
		ConcreteAsyncFactories = new Dictionary<( BoxType? Type, Type? Parent, ByteVector? HandleType), Func<BoxHeader, MP4, IsoHandlerBox?, Task<Box>>>();
	
	static BoxFactory()
	{
		foreach (var type in typeof(Box).Assembly.GetTypes()
			         .Where(myType => myType.IsClass && typeof(Box).IsAssignableFrom(myType)))
		{
			var attributes = type.GetCustomAttributes().OfType<HasBoxFactoryAttribute>().ToArray();
			if(!attributes.Any())
				continue;
			
			var method = type.GetMethod(nameof( DataAtom.CreateAsync), BindingFlags.Static | BindingFlags.Public) ??
			             typeof(Box).GetMethod(nameof(Box.CreateAsync));

			var factory = (Func<BoxHeader, MP4, IsoHandlerBox?, Task<Box>>) Delegate.CreateDelegate(
				typeof(Func<BoxHeader, MP4, IsoHandlerBox?, Task<Box>>), null, method!.MakeGenericMethod(type));
			
			foreach (var attribute in attributes)
			{
				ConcreteAsyncFactories[(attribute.Type, attribute.Parent, attribute.HandleType)] = factory;
				
			}
		}
	}

	private static Func<BoxHeader, MP4, IsoHandlerBox?, Task<Box>>  GetConcreteFactory(BoxType type, Type? parent, ByteVector? handlerType)
	{
		if (ConcreteAsyncFactories.TryGetValue((type,parent, handlerType), out var factory)) 
			return factory;
		if (ConcreteAsyncFactories.TryGetValue((type,parent, null), out  factory)) 
			return factory;
		if (ConcreteAsyncFactories.TryGetValue((type,null, handlerType), out  factory)) 
			return factory;
		if (ConcreteAsyncFactories.TryGetValue((null,parent, handlerType), out  factory)) 
			return factory;
		if (ConcreteAsyncFactories.TryGetValue((null,parent, null), out  factory)) 
			return factory;
		return ConcreteAsyncFactories.TryGetValue((type, null, null), out factory) ? factory : Box.CreateAsync<UnknownBox>;
	}
	
	/// <summary> Creates a box by reading it from a file given its header,
	/// parent header, handler, and index in its parent. </summary>
	/// <param name="file">A <see cref="MediaFile" /> object containing the file to read from. </param>
	/// <param name="header">A <see cref="BoxHeader" /> object containing the header of the box to create. </param>
	/// <param name="parent"> A <see cref="BoxHeader" /> object containing the header of the parent box. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies to the new box. </param>
	/// <returns> A newly created subtype <see cref="Box" /> object. </returns>
	static async Task<Box> CreateBoxAsync (
		MP4 file,
		BoxHeader header,
		BoxHeader? parent,
		IsoHandlerBox? handler)
	{
		return await GetConcreteFactory(header.BoxType, parent?.Box?.GetType(), handler?.HandlerType)(header, file, handler);

	}

	/// <summary> Creates a box by reading it from a file given its position in the file, parent header, handler, and index in its parent. </summary>
	/// <param name="file"> A <see cref="MediaFile" /> object containing the file to read from. </param>
	/// <param name="position"> A <see cref="long" /> value specifying at what seek position in <paramref name="file" /> to start reading. </param>
	/// <param name="parent"> A <see cref="BoxHeader" /> object containing the header of the parent box. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies to the new box. </param>
	/// <returns> A newly created subtype <see cref="Box" /> object. </returns>
	internal static async Task<Box> CreateBoxAsync(MP4 file, long position, BoxHeader? parent, IsoHandlerBox? handler)
	{
		return await CreateBoxAsync(file, await BoxHeader.CreateAsync(file, position), parent, handler);
	}

	/// <summary> Creates a box by reading it from a file given its position in the file and handler. </summary>
	/// <param name="file"> A <see cref="MediaFile" /> object containing the file to read from. </param>
	/// <param name="position"> A <see cref="long" /> value specifying at what seek position in <paramref name="file" /> to start reading. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies to the new box. </param>
	/// <returns> A newly created subtype <see cref="Box" /> object. </returns>
	// ReSharper disable once MemberCanBePrivate.Global
	public static async Task<Box> CreateBoxAsync(MP4 file, long position, IsoHandlerBox? handler)
	{
		return await CreateBoxAsync (file, position, null, handler);
	}

	/// <summary> Creates a box by reading it from a file given its position in the file. </summary>
	/// <param name="file">A <see cref="MediaFile" /> object containing the file to read from. </param>
	/// <param name="position"> A <see cref="long" /> value specifying at what seek position in <paramref name="file" /> to start reading. </param>
	/// <returns> A newly created subtype <see cref="Box" /> object. </returns>
	public static async Task<Box> CreateBoxAsync(MP4 file, long position)
	{
		return await CreateBoxAsync(file, position, null);
	}

	/// <summary> Creates a box by reading it from a file given its header and handler. </summary>
	/// <param name="file"> A <see cref="MP4" /> object containing the file to read from.</param>
	/// <param name="header">A <see cref="BoxHeader" /> object containing the header of the box to create. </param>
	/// <param name="handler"> A <see cref="IsoHandlerBox" /> object containing the handler that applies to the new box. </param>
	/// <returns> A newly created subtype <see cref="Box" /> object. </returns>
	public static async Task<Box> CreateBoxAsync(MP4 file, BoxHeader header, IsoHandlerBox? handler)
	{
		return await CreateBoxAsync(file, header, null, handler);
	}

	/// <summary> Creates a box by reading it from a file given its header and handler. </summary>
	/// <param name="file">A <see cref="MP4" /> object containing the file to read from. </param>
	/// <param name="header">A <see cref="BoxHeader" /> object containing the header of the box to create.</param>
	/// <returns> A newly created <see cref="Box" /> object. </returns>
	public static async Task<Box> CreateBoxAsync(MP4 file, BoxHeader header)
	{
		return await CreateBoxAsync(file, header, null);
	}
}