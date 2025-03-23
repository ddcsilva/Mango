using Mango.Web.Core.DTOs;
using Newtonsoft.Json;

namespace Mango.Web.Core.Extensions;

public static class ResponseExtensions
{
    public static T? DeserializeResult<T>(this ResponseDTO response)
    {
        if (response?.Result == null) return default;

        var json = response.Result.ToString();

        if (string.IsNullOrWhiteSpace(json)) return default;

        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch
        {
            return default;
        }
    }
}

