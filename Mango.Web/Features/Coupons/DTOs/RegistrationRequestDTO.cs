namespace Mango.Web.Features.Coupons.DTOs;

public record RegistrationRequestDTO(string Name, string Email, string PhoneNumber, string Password, string Role = "Customer");
