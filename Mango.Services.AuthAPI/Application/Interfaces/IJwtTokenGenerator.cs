using Mango.Services.AuthAPI.Domain.Models;

namespace Mango.Services.AuthAPI.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
