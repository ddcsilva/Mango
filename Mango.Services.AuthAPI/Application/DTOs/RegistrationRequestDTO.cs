namespace Mango.Services.AuthAPI.Application.DTOs;

public record RegistrationRequestDTO(string Name, string Email, string PhoneNumber, string Password);
