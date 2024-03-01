using AuthService.Authorization;
using AuthService.Extensions;
using AuthService.Service;
using AuthService.Settings;
using Core.Db;
using Core.Db.Configuration;
using Core.Service.Domain.Startup;
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

    // configure strongly typed settings object
   // services.Configure<IDbSettings>(builder.Configuration.GetSection("AppSettings"));
    //services.Configure<IDbSettings>(builder AppSettings);
  //  services.AddSingleton(typeof(IDbSettings), typeof(DbSettings<AppSettings>));
    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
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

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.MapControllers();
}

app.Run("http://localhost:4000");