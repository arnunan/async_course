using AuthService.DB;
using AuthService.Settings;
using Core.Db.ContextSupport;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace AuthService.Extensions;

public static class AuthServiceExtensions
{
    public static void AddAsyncCourseProperties(this IServiceCollection services)
    {
        services.AddSingleton(ReadSettingsJson());
    }

    public static void AddAsyncCourseDbContext(this IServiceCollection services)
    {
        services
            .AddSingleton<UserDbContext>()
            .AddSingleton<RoleDbContext>()
            .AddSingleton(typeof(IDbContextCreator<UserDbContext>), typeof(UserDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<UserDbContext>),
                typeof(AuthDesignTimeDbContextFactory<UserDbContext, UserDbContextCreator>))
            .AddSingleton(typeof(IDbContextCreator<RoleDbContext>), typeof(RoleDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<RoleDbContext>),
                typeof(AuthDesignTimeDbContextFactory<RoleDbContext, RoleDbContextCreator>));
    }

    private static AppSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<AppSettings>(json);
        return configuration;
    }
}