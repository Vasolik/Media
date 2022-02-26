using System;
using System.Linq;
using System.Linq.Expressions;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Vipl.Base.Extensions;

/// <summary>
///  Class containing extension methods to <see cref="Expression"/> class.
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// Generate lambda expression to accessing property of then object by path t.
    /// </summary>
    /// <param name="propertyPath">Path to the property</param>
    /// <typeparam name="T">Type of the input parameter of produced expresion.</typeparam>
    /// <typeparam name="TResult">Type of the result of produces expression.</typeparam>
    /// <returns>Expression returning specific property.</returns>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/>  does not have specific property on give path.</exception>
    public static Expression<Func<T, TResult>> MakeMemberPathExpression<T, TResult>(string propertyPath)
    {
        try
        {
            var type = typeof(T);
            var (current, next) = Split(propertyPath, '.');
            
            var parameter = Expression.Parameter(type, "obj");
            var declaringType = type.GetProperty(current)?.DeclaringType;
            var child = declaringType?.GetProperty(current);
            if(child is null)
                throw new ArgumentException($"Invalid property path \"{propertyPath}\"", nameof(propertyPath));
                
            var (path, resultType) = next == null
                ? (Expression.MakeMemberAccess(parameter, child), child.PropertyType)
                : GeneratePropertyExpression(Expression.MakeMemberAccess(parameter, child), next);

            return Expression.Lambda<Func<T, TResult>>(typeof(TResult).IsAssignableFrom(resultType) ? path : Expression.Convert(path, typeof(TResult)), parameter);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Invalid property path \"{propertyPath}\"", nameof(propertyPath), e);
        }
    }

    private static (Expression path, Type resultType) GeneratePropertyExpression(Expression parent, string path)
    {
        var (current, next) = Split(path, '.');
        var child = parent.Type.GetProperty(current);
        if(child is null)
            throw new ArgumentException($"Invalid property path \"{path}\"");

        return next == null
            ? (Expression.MakeMemberAccess(parent, child!), child!.PropertyType)
            : GeneratePropertyExpression(Expression.Property(parent, child!), next);
    }

    private static (string, string?) Split(string value, char separator)
    {
        var items = value.Split(separator, 2);
        return (items.First(), items.Skip(1).FirstOrDefault());
    }
}