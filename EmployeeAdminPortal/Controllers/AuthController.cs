using EmployeeAdminPortal.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly TokenService _tokenService;

  public AuthController(TokenService tokenService)
  {
    _tokenService = tokenService;
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] User user)
  {
    if (user.Username == "admin" && user.Password == "password") // Replace with real user validation
    {
      var token = _tokenService.GenerateToken(user.Username);
      return Ok(new { token });
    }
    return Unauthorized();
  }
}
