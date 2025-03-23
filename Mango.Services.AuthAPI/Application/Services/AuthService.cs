using Mango.Services.AuthAPI.Application.DTOs;
using Mango.Services.AuthAPI.Application.Interfaces;
using Mango.Services.AuthAPI.Domain.Models;
using Mango.Services.AuthAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Application.Services;

public class AuthService(
    AppDbContext context, 
    UserManager<ApplicationUser> userManager, 
    RoleManager<IdentityRole> roleManager,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
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

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user = context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

        bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

        if (user is null || !isValid)
        {
            return new LoginResponseDTO(User: null, Token: "");
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        var loginResponse = new LoginResponseDTO(User: userDTO, Token: token);

        return loginResponse;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = context.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user == null) return false;

        if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
        }

        await userManager.AddToRoleAsync(user, roleName);

        return true;
    }
}

