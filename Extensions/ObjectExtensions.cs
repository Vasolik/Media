using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Vipl.Base.Extensions;

/// <summary> Extensions for any <see cref="object"/>. </summary>
public static class ObjectExtensions
{
    /// <summary> Check if <paramref name="obj"/> is equal to any value inside of <paramref name="values"/> </summary>
    public static bool EqualsAny<T>(this T obj, params T[] values)
    {
        return Array.IndexOf(values, obj) != -1;
    }
    /// <summary> Check if <typeparamref name="T"/> contains property on <paramref name="path"/>. </summary>
    /// <typeparam name="T">Type for which we are doing a checking</typeparam>
    /// <param name="path">Path witch we are searching.</param>
    /// <returns>Returns true if path is found, throw <see cref="ArgumentException"/> if not found</returns>
    public static bool DoesTypeContainPath<T>(string path)
    {
        var pp = path.Split('.');
        var t = typeof(T);
        foreach (var prop in pp)
        {
            var propInfo = t.GetProperty(prop);
            if (propInfo != null)
            {
                t = propInfo.PropertyType;
            }
            else throw new ArgumentException("Properties path is not correct");
        }
        return true;
    }
    /// <summary> Make <see cref="IEnumerable{T}"/> of type <typeparamref name="T"/> with just element <paramref name="item"/> </summary>
    /// <typeparam name="T">Type of <paramref name="item"/></typeparam>
    /// <param name="item">Only element in new <see cref="IEnumerable{T}"/></param>
    /// <returns><see cref="IEnumerable{T}"/> of type <typeparamref name="T"/> with just element <paramref name="item"/></returns>
    public static IEnumerable<T> WrapAsEnumerable<T>(this T item)
    {
        yield return item;
    }
    private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance)!;
    
    /// <summary> Deep clone <paramref  name="originalObject"/>. Does not work with <see cref="Delegate"/> inside an object. Be careful with more complex objects. </summary>
    /// <param name="originalObject">Object to be cloned</param>
    /// <typeparam name="T">Type of the cloned object</typeparam>
    /// <returns>Deep clone of <paramref name="originalObject"/> object</returns>
    public static T DeepCloneSimple<T>(this T originalObject)
    {
        return InternalClone(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
    }
    private static T InternalClone<T>(T originalObject, IDictionary<object, object> visited)
    {
        if (originalObject == null) return originalObject;
        var typeToReflect = originalObject.GetType();
        if (typeToReflect.IsSimple()) return originalObject;
        if (visited.ContainsKey(originalObject)) return (T)visited[originalObject];
        if (typeof(Delegate).IsAssignableFrom(typeToReflect)) throw new InvalidOperationException("Cloning delegates is not supported.");
        var cloneObject = (T)CloneMethod.Invoke(originalObject, null)!;
        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType()!;
            if (arrayType.IsSimple() == false)
            {
                var clonedArray = (Array)(object)cloneObject;
                for (var i = 0L; i < clonedArray.LongLength; i++)
                {
                    clonedArray.SetValue(InternalClone(clonedArray.GetValue(i), visited), i);
                }
            }

        }
        visited.Add(originalObject, cloneObject);
        CopyProperties(originalObject, visited, cloneObject, typeToReflect);
        return cloneObject;
    }

    private static void CopyProperties(object originalObject, IDictionary<object, object> visited, object cloneObject, IReflect typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
    {
        foreach (var propertyInfo in typeToReflect.GetProperties(bindingFlags))
        {
            if (!propertyInfo.CanWrite) continue;
            if (propertyInfo.PropertyType.IsSimple()) continue;
            var originalFieldValue = propertyInfo.GetValue(originalObject);
            var clonedFieldValue = InternalClone(originalFieldValue, visited);
            propertyInfo.SetValue(cloneObject, clonedFieldValue);
        }
    }
#nullable disable
    /// <summary>
    /// Check if value is equal to default value for <typeparamref name="T"/>
    /// </summary>
    /// <param name="obj">Object to check.</param>
    /// <typeparam name="T">Type of the object.</typeparam>
    /// <returns>True if value is default value for the object.</returns>
    public static bool IsDefault<T>([MaybeNull]this T obj)
    {
        return Equals(obj, default(T));
    }
#nullable restore        

}
internal class ReferenceEqualityComparer : EqualityComparer<object>
{
    public override bool Equals(object? x, object? y) => ReferenceEquals(x, y);

    public override int GetHashCode(object? obj)
    {
        return obj == null ? 0 : RuntimeHelpers.GetHashCode(obj);
    }
}