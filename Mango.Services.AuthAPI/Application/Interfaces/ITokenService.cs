using Mango.Services.AuthAPI.Domain.Models;

namespace Mango.Services.AuthAPI.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}
