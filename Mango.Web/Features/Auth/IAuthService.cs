using Mango.Web.Core.DTOs;
using Mango.Web.Features.Auth.DTOs;

namespace Mango.Web.Features.Auth;

public interface IAuthService
{
    Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
    Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
    Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
}
