using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Application.Services.Authenticate;

namespace Terreiro.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IUserRepository userRepository,
    IAuthenticateService authenticateService
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userRepository.GetByCpf(request.CPF);
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_CPF.InsertParams(request.CPF));

        var token = authenticateService.GenerateToken(user);
        return Ok(token);
    }
}
