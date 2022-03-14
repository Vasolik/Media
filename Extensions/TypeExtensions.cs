namespace Vipl.Base.Extensions;

/// <summary>
/// Extension for <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Check if type is simple type (Primitive, Enum, string, decimal, Guid, DateTime, Timespan) or nullable version of it.
    /// </summary>
    /// <param name="type">Type for checking.</param>
    /// <returns><![CDATA[true]]> if it is simple, false otherwise</returns>
    public static bool IsSimple(this Type type)
    {
        while (true)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                type = type.GetGenericArguments()[0];
                continue;
            }

            return type.IsPrimitive 
                   || type.IsEnum 
                   || type == typeof(string) 
                   || type == typeof(decimal) 
                   || type == typeof(Guid) 
                   || type == typeof(DateTime) 
                   || type == typeof(TimeSpan);
        }
    }
    /// <summary>
    /// Get generic type (with full specialization) of <paramref name="type"/> type.
    /// </summary>
    /// <param name="type">Type to be searched.</param>
    /// <param name="genericBase">Generic type to be searched for.</param>
    /// <returns>Generic type (with full specialization)</returns>
    public static Type? FindGenericBase(this Type type, Type genericBase)
    {
        var result = type;
        while (result is not null && !(result.IsGenericType && result.GetGenericTypeDefinition() == genericBase))
            result = result.BaseType;
        return result;
    }
}