﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace Vipl.Base.Extensions;

/// <summary>
/// Extensions to enums
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Get an enum value based of <see cref="DescriptionAttribute.Description"/>. If enum value does not have <see cref="DescriptionAttribute.Description"/>
    /// it is taking <![CDATA[enumValue.ToString("G")]]> value for search.
    /// </summary>
    /// <typeparam name="T">Type of the enum</typeparam>
    /// <param name="enumDescription">Description of this Enum</param>
    /// <returns>EnumValue with given description</returns>
    public static T? GetEnumByDescription<T>(this string enumDescription) where T : struct, Enum
    {
        return EnumExtensionHelper<T>.DescriptionToEnum.TryGetValue(enumDescription, out var value) ? value : (T?)null;
    }

    /// <summary>
    /// Get an enum value based of <![CDATA[enumValue.ToString("G")]]> value of given enum..
    /// </summary>
    /// <typeparam name="T">Type of the enum</typeparam>
    /// <param name="enumStringValue">Description of this Enum</param>
    /// <returns>EnumValue with given description</returns>
    public static T? GetEnumByStringValue<T>(this string enumStringValue) where T : struct, Enum
    {
        return EnumExtensionHelper<T>.StringValueToEnum.TryGetValue(enumStringValue, out var value) ? value : (T?)null;
    }

    /// <summary>
    /// Get <see cref="DescriptionAttribute.Description"/> for given <paramref name="enumValue"/>. If <paramref name="enumValue"/> value does not have <see cref="DescriptionAttribute.Description"/>
    /// <![CDATA[enumValue.ToString("G")]]> returned.
    /// </summary>
    /// <typeparam name="T">Type of the enum.</typeparam>
    /// <param name="enumValue">Enum value for which Description in needed.</param>
    /// <returns><see cref="String"/> description for given <paramref name="enumValue"/> value.</returns>
    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
        return EnumExtensionHelper<T>.EnumToDescription[enumValue];
    }

    /// <summary>
    /// Gets an attribute on an enum field value
    /// </summary>
    /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
    /// <param name="enumVal">The enum value</param>
    /// <returns>The attribute of type T that exists on the enum value</returns>
    /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
    public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }
}

internal static class EnumExtensionHelper<T>
    where T : Enum
{
    internal static IDictionary<T, string> EnumToDescription =>
        typeof(T).GetEnumValues().OfType<T>().ToDictionary(e => e,
            e => e.GetAttributeOfType<DescriptionAttribute>()?.Description ?? e.ToString("G"));

    internal static IDictionary<string, T> DescriptionToEnum =>
        typeof(T).GetEnumValues().OfType<T>().ToDictionary(
            e => e.GetAttributeOfType<DescriptionAttribute>()?.Description ?? e.ToString("G"), e => e);

    internal static IDictionary<string, T> StringValueToEnum =>
        typeof(T).GetEnumValues().OfType<T>().ToDictionary(e => e.ToString("G"), e => e);
}