using Mango.Services.AuthAPI.Application.DTOs;
using Mango.Services.AuthAPI.Application.Interfaces;
using Mango.Services.AuthAPI.Domain.Models;
using Mango.Services.AuthAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Application.Services;

public class AuthService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IAuthService
{
    public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        try
        {
            var user = new ApplicationUser
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, registrationRequestDTO.Password);

            if (result.Succeeded)
            {
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault()?.Description ?? "Erro desconhecido";
            }
        }
        catch (Exception)
        {
            // Logging futuro
        }

        return "Erro desconhecido";
    }
    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        throw new NotImplementedException();
    }
}

