using Microsoft.Extensions.Options;
using MicroStore.Web.Core.Base;
using MicroStore.Web.Core.DTOs;
using MicroStore.Web.Core.Enums;
using MicroStore.Web.Core.Utilities;
using MicroStore.Web.Features.Auth.DTOs;
using MicroStore.Web.Features.Auth.Interfaces;

namespace MicroStore.Web.Features.Auth.Services;

public class AuthService(IBaseService baseService, IOptions<ServiceUrls> serviceUrls) :  IAuthService
{
    private readonly string _authApiBase = serviceUrls.Value.AuthApiBase;

    public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/login",
            Data = loginRequestDTO
        }, withBearer: false);
    }

    public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/register",
            Data = registrationRequestDTO
        }, withBearer: false);
    }

    public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_authApiBase}/api/auth/assignRole",
            Data = registrationRequestDTO
        });
    }
}
