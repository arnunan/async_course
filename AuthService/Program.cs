using AuthService.Authorization;
using AuthService.Extensions;
using AuthService.Service;
using AuthService.Session;
using AuthService.Settings;
using AuthService.Tokens;
using Core.Service.Domain.Startup;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Template.FrontApi.Db.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add settings
builder.Services.AddAsyncCourseProperties();

builder.Services.AddAsyncCourseDbSettings<AppSettings>();
builder.Services.AddAsyncCourseDomain();
builder.Services.AddAsyncCourseDbContext();
// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<ITokenHelper, TokenHelper>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ISessionFactory, SessionFactory>();
    services.AddScoped<ITokenReaderWriter, TokenReaderWriter>();
    services.AddScoped<IDistributedCache, MemoryDistributedCache>();

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(
            CookieAuthenticationDefaults.AuthenticationScheme,
            options =>
            {
                options.LoginPath = new PathString("/login");
                options.AccessDeniedPath = new PathString("/auth/denied");
            });
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .WithOrigins("http://localhost.dev.course:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run("http://localhost.dev.course:4000");