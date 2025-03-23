namespace AuthAPI.Application.DTOs;

public record LoginResponseDTO(UserDTO User, string Token);
