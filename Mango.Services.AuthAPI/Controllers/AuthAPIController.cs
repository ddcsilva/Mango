using Mango.Services.AuthAPI.Application.DTOs;
using Mango.Services.AuthAPI.Application.Interfaces;
using Mango.Services.AuthAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        var response = new ResponseDTO();

        string errorMessage = await authService.Register(model);

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
            response.Message = "Username or password is incorrect.";
            return BadRequest(response);
        }

        response.Result = loginResponse;
        return Ok(response);
    }

}
