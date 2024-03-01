using Core.Db.ContextSupport;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Service.Domain.Startup;

public static class ServiceStartupExtensions
{
    public static IServiceCollection AddAsyncCourseDomain(this IServiceCollection services)
    {
        return services
            .AddSingleton(typeof(IDbContextFactory<>), typeof(DbContextFactory<>));
    }
}