using AuthService.Service;
using Microsoft.AspNetCore.Session;

namespace AuthService.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, ITokenHelper tokenHelper)
    {
       
        var token = context.Request.Headers["auth_token"].FirstOrDefault()?.Split(" ").Last();
        var userId = tokenHelper.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["userId"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
}