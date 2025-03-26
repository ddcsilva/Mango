using MicroStore.Web.Core.DTOs;
using MicroStore.Web.Features.Auth.DTOs;

namespace MicroStore.Web.Features.Auth.Interfaces;

public interface IAuthService
{
    Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
    Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
    Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
}
