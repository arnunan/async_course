using AuthService.Authorization;
using AuthService.Models;
using AuthService.Service;

namespace AuthService.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService) =>
        _userService = userService;

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public IActionResult SignIn(SignInRequest model)
    {
        var response = _userService.SignIn(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public IActionResult SignUp(SignUpRequest model) =>
        Ok(_userService.SignUp(model));

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(string username)
    {
        var response = _userService.ForgotPassword(username);

        if (response == null)
            return BadRequest(new { message = "Username is incorrect" });

        return Ok(response);
    }
}