using System.Linq.Expressions;

namespace Vipl.Base.Extensions;

/// <summary>
/// Extensions for <see cref="IQueryable"/>.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Applying order to specific property name.
    /// </summary>
    /// <typeparam name="T">Underline queryable type.</typeparam>
    /// <param name="source">An instance of <see cref="IQueryable{T}"/>.</param>
    /// <param name="propertyName">Property name for ordering.</param>
    /// <param name="ascending">Indicates if ascending ordering.</param>
    /// <returns>An instance of <see cref="IQueryable{T}"/>.</returns>
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return source;
        }

        var method = ascending ? "OrderBy" : "OrderByDescending";
        var type = typeof(T);
        var property = type.GetProperty(propertyName);
        if (property == null)
            throw new MemberAccessException($"Property {propertyName} not found in {type.FullName}.");

        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var keySelector = Expression.Lambda(propertyAccess, parameter);
        var expression = Expression.Call(
            typeof(Queryable),
            method,
            new[] { type, property.PropertyType },
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(expression);
    }
}