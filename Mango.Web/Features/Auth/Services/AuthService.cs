using Mango.Web.Core.Base;
using Mango.Web.Core.DTOs;
using Mango.Web.Core.Enums;
using Mango.Web.Core.Utilities;
using Mango.Web.Features.Auth.DTOs;
using Microsoft.Extensions.Options;

namespace Mango.Web.Features.Auth.Services;

public class AuthService(IHttpClientFactory clientFactory, IOptions<ServiceUrls> serviceUrls) : BaseService(clientFactory), IAuthService
{
    private readonly string _authApiBase = serviceUrls.Value.AuthApiBase;

    public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        return await SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/login",
            Data = loginRequestDTO
        });
    }

    public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        return await SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/register",
            Data = registrationRequestDTO
        });
    }

    public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        return await SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/assignRole",
            Data = registrationRequestDTO
        });
    }
}
