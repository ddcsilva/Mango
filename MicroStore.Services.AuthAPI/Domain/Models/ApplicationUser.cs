using Microsoft.AspNetCore.Identity;

namespace MicroStore.Services.AuthAPI.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
