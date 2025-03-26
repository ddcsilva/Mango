using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MicroStore.Web.Features.Auth.Interfaces;
using MicroStore.Web.Core.Helpers;
using MicroStore.Web.Features.Auth.Enums;
using MicroStore.Web.Features.Auth.DTOs;
using MicroStore.Web.Core.Extensions;

namespace MicroStore.Web.Features.Auth.Controllers;

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
            await SignInUserAsync(loginData);
            tokenService.SetToken(loginData.Token);
            return RedirectToAction("Index", "Home");
        }

        TempData["error"] = response?.Message ?? "Erro ao efetuar login";
        return View(loginRequestDTO);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        tokenService.ClearToken();
        return RedirectToAction("Index", "Home");
    }

    private async Task SignInUserAsync(LoginResponseDTO loginResponse)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(loginResponse.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value ?? ""));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value ?? ""));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value ?? ""));

        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value ?? ""));

        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwtToken.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value ?? ""));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}
