﻿using Mango.Web.Core.Helpers;
using Mango.Web.Features.Auth.DTOs;
using Mango.Web.Features.Auth.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Mango.Web.Features.Auth.Controllers;

public class AuthController(IAuthService authService) : Controller
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

        if (response is not null && response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        TempData["error"] = response?.Message ?? "Erro ao efetuar login";
        return View(loginRequestDTO);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        // Será implementado mais tarde
        return RedirectToAction("Index", "Home");
    }
}
