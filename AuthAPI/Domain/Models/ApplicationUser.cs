using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
