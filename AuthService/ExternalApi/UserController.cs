using AuthService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.ExternalApi;

[ApiController]
[Route("externalapi/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("for-assign")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAllForAssign();
        return Ok(users);
    }
}