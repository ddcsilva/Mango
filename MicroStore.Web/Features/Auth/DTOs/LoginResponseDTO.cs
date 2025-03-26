namespace MicroStore.Web.Features.Auth.DTOs;

public record LoginResponseDTO(UserDTO User, string Token);
