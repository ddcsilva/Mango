using Mango.Services.AuthAPI.Application.DTOs;

namespace Mango.Services.AuthAPI.Application.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
}
