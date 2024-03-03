using AuthService.Authorization;
using AuthService.Extensions;
using AuthService.Service;
using AuthService.Settings;
using Core.Service.Domain.Startup;
using Microsoft.AspNetCore.Authentication.Cookies;
using Template.FrontApi.Db.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add settings
builder.Services.AddAsyncCourseProperties();

builder.Services.AddAsyncCourseDbSettings<AppSettings>();
builder.Services.AddAsyncCourseDomain();
builder.Services.AddAsyncCourseDbContext();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = new PathString(""));
// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();
    services.AddScoped<ITokenHelper, TokenHelper>();
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    app.UseAuthentication();
    app.UseAuthorization();
    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
}

app.Run("http://localhost:4000");