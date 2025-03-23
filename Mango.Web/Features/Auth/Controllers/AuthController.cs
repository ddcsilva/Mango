using Mango.Web.Core.DTOs;
using Mango.Web.Core.Extensions;
using Mango.Web.Core.Helpers;
using Mango.Web.Features.Auth.DTOs;
using Mango.Web.Features.Auth.Enums;
using Mango.Web.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Features.Auth.Controllers;

public class AuthController(IAuthService authService, ITokenService tokenService) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.RoleList = EnumHelper.ToSelectList<UserRole>();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.RoleList = EnumHelper.ToSelectList<UserRole>();

            return View(registrationRequestDTO);
        }

        var registerResponse = await authService.RegisterAsync(registrationRequestDTO);

        if (registerResponse is null || !registerResponse.IsSuccess)
        {
            TempData["error"] = registerResponse?.Message ?? "Erro ao registrar o usuário.";
            ViewBag.RoleList = EnumHelper.ToSelectList<UserRole>();
            return View(registrationRequestDTO);
        }

        registrationRequestDTO.Role ??= UserRole.CUSTOMER.ToString();

        var assignResponse = await authService.AssignRoleAsync(registrationRequestDTO);

        if (assignResponse is null || !assignResponse.IsSuccess)
        {
            TempData["error"] = "Registro criado, mas falha ao atribuir a role.";
            return RedirectToAction(nameof(Login));
        }

        TempData["success"] = "Conta criada com sucesso!";
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
    {
        if (!ModelState.IsValid) return View(loginRequestDTO);

        var response = await authService.LoginAsync(loginRequestDTO);
        var loginData = response?.DeserializeResult<LoginResponseDTO>();

        if (loginData is not null && !string.IsNullOrEmpty(loginData.Token))
        {
            tokenService.SetToken(loginData.Token);
            TempData["success"] = "Login realizado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        TempData["error"] = response?.Message ?? "Erro ao efetuar login";
        return View(loginRequestDTO);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        tokenService.ClearToken();
        TempData["success"] = "Você saiu com sucesso!";
        return RedirectToAction("Index", "Home");
    }
}
