using MicroStore.Services.AuthAPI.Application.DTOs;

namespace MicroStore.Services.AuthAPI.Application.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    Task<bool> AssignRole(string email, string roleName);
}
