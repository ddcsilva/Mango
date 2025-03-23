using Mango.Services.AuthAPI.Application.DTOs;
using Mango.Services.AuthAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
    {
        var response = new ResponseDTO();

        string errorMessage = await authService.Register(registrationRequestDTO);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            response.IsSuccess = false;
            response.Message = errorMessage;
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var response = new ResponseDTO();

        var loginResponse = await authService.Login(model);

        if (loginResponse.User == null)
        {
            response.IsSuccess = false;
            response.Message = "Usuário ou senha incorretos.";
            return BadRequest(response);
        }

        response.Result = loginResponse;
        return Ok(response);
    }

    [HttpPost("assignRole")]
    public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
    {
        var response = new ResponseDTO();

        bool assignRoleSuccessful = await authService.AssignRole(model.Email, model.Role.ToUpper());

        if (!assignRoleSuccessful)
        {
            response.IsSuccess = false;
            response.Message = "Erro ao atribuir função.";
            return BadRequest(response);
        }

        return Ok(response);
    }

}
