using Mango.Web.Core.Enums;

namespace Mango.Web.Core.DTOs;

public class RequestDTO
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; } = string.Empty;
    public object? Data { get; set; } 
    public string AccessToken { get; set; } = string.Empty;
}
