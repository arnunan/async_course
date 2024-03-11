using AuthService.Authorization;
using AuthService.Service;

namespace AuthService.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("user")]
    public IActionResult GetUser([ModelBinder] Session.Session session)
    {
        var users = _userService.GetById(session.UserId);
        return Ok(users);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}