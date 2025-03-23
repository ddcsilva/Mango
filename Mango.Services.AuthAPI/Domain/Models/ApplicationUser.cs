using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
