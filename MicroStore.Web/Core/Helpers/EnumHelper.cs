using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MicroStore.Web.Core.Helpers;

public static class EnumHelper
{
    public static List<SelectListItem> ToSelectList<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(value => new SelectListItem
            {
                Text = value.GetDisplayName(),
                Value = value.ToString()
            }).ToList();
    }

    private static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DisplayAttribute>();
        return attr?.Name ?? value.ToString();
    }
}
