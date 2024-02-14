using Core.Db.Configuration;
using Core.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Template.FrontApi.Db.Configuration;

public static class DbServiceStartupExtensions
{
    public static IServiceCollection AddAsyncCourseDbSettings<TAppProperties>(this IServiceCollection services)
        where TAppProperties : IDbProperties
    {
        return services
            .AddSingleton(typeof(IDbSettings), typeof(DbSettings<TAppProperties>));
    }
}