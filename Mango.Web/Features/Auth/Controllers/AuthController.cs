using Mango.Web.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Features.Auth.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginRequestDTO());
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegistrationRequestDTO());
    }

    [HttpGet]
    public IActionResult Logout()
    {
        // Será implementado mais tarde
        return RedirectToAction("Index", "Home");
    }
}
