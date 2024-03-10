using AuthService.Authorization;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Service;

namespace AuthService.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthController(
        IUserService userService,
        ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public IActionResult SignIn(SignInRequest model)
    {
        var user = _userService.SignIn(model);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });
        SetCookie(user);
        return Ok(new SignInResponseModel(user));
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public IActionResult SignUp(SignUpRequest model)
    {
        var user = _userService.SignUp(model);
        SetCookie(user);
        return Ok(new SignUpResponseModel(user));
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(string username)
    {
        var response = _userService.ForgotPassword(username);

        if (response == null)
            return BadRequest(new { message = "Username is incorrect" });

        return Ok(response);
    }

    private void SetCookie(User user)
    {
        var token = _tokenHelper.GenerateJwtToken(user);
        var cookieHeaders = new List<Cookie>
        {
            new("token", token, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true
            }),
            new("userId", user.Id.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true
            })
        };

        foreach (var cookie in cookieHeaders)
            Response.Cookies.Append(cookie.Name, cookie.Value, cookie.Options);
    }
}