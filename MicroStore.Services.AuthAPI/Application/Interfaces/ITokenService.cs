using MicroStore.Services.AuthAPI.Domain.Models;

namespace MicroStore.Services.AuthAPI.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}
