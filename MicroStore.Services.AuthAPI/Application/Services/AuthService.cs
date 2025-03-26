using Microsoft.AspNetCore.Identity;
using MicroStore.Services.AuthAPI.Application.DTOs;
using MicroStore.Services.AuthAPI.Application.Interfaces;
using MicroStore.Services.AuthAPI.Domain.Models;

namespace MicroStore.Services.AuthAPI.Application.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager, 
    RoleManager<IdentityRole> roleManager,
    ITokenService jwtTokenGenerator) : IAuthService
{
    public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        var userExists = await userManager.FindByEmailAsync(registrationRequestDTO.Email);
        if (userExists != null) return "Usuário já existe com este e-mail.";

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

            if (!result.Succeeded) return result.Errors.FirstOrDefault()?.Description ?? "Erro desconhecido ao registrar o usuário.";

            return string.Empty;
        }
        catch (Exception)
        {
            // Logging futuro
        }

        return "Erro desconhecido";
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user = await userManager.FindByNameAsync(loginRequestDTO.UserName);

        if (user is null)
        {
            return new LoginResponseDTO(null!, string.Empty);
        }

        var isValid = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

        if (!isValid)
        {
            return new LoginResponseDTO(null!, string.Empty);
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenGenerator.GenerateToken(user, roles);

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber ?? string.Empty
        };

        return new LoginResponseDTO(userDTO, token);
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null) return false;

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var result = await userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }
}

