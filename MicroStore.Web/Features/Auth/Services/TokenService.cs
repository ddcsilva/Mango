using MicroStore.Web.Core.Constants;
using MicroStore.Web.Features.Auth.Interfaces;

namespace MicroStore.Web.Features.Auth.Services;

public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
{
    public void SetToken(string token)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append(GlobalConstants.JwtTokenCookie, token);
    }

    public string? GetToken()
    {
        var context = httpContextAccessor.HttpContext;

        if (context != null && context.Request.Cookies.TryGetValue(GlobalConstants.JwtTokenCookie, out var token))
        {
            return token;
        }

        return null;
    }

    public void ClearToken()
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(GlobalConstants.JwtTokenCookie);
    }
}

