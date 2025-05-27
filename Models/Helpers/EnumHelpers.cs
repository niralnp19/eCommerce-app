using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumHelpers
{
    public static string GetEnumDisplayName<T>(T value) where T : Enum
    {
        var fieldName = Enum.GetName(typeof(T), value);
        var displayAttr = typeof(T)
            .GetField(fieldName)
            .GetCustomAttribute<DisplayAttribute>();
        return displayAttr?.Name ?? fieldName;
    }
}