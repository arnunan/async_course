using Core.Db.ContextSupport;
using Core.WarmUp;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using Template.FrontApi.Configuration;
using Template.FrontApi.Db;

namespace Template.FrontApi.Extensions;

public static class FrontApiExtensions
{
    public static IServiceCollection AddAsyncCourseProperties(this IServiceCollection services)
    {
        return services.AddSingleton(ReadSettingsJson());
    }
    
    public static IServiceCollection AddAsyncCourseDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<TemplateApiDbContext>()
            .AddSingleton(typeof(IDbContextCreator<TemplateApiDbContext>), typeof(TemplateApiDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<TemplateApiDbContext>), typeof(DesignTimeDbContextFactory))
            .AddSingleton<IWarmUp, TemplateApiDbWarmUp>();
    }

    private static TemplateApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/template_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<TemplateApiApplicationSettings>(json);
        return configuration;
    }
}