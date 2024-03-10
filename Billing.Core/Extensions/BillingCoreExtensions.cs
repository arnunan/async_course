using Billing.Core.DB;
using Billing.Core.Settings;
using Core.Db.ContextSupport;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Billing.Core.Extensions;

public static class BillingCoreExtensions
{
    public static void AddAsyncCourseProperties(this IServiceCollection services)
    {
        services.AddSingleton(ReadSettingsJson());
    }

    public static void AddAsyncCourseDbContext(this IServiceCollection services)
    {
        services
            .AddSingleton<AccountDbContext>()
            .AddSingleton<OperationsLogDbContext>()
            .AddSingleton(typeof(IDbContextCreator<AccountDbContext>), typeof(AccountDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<AccountDbContext>),
                typeof(AuthDesignTimeDbContextFactory<AccountDbContext, AccountDbContextCreator>))
            .AddSingleton(typeof(IDbContextCreator<OperationsLogDbContext>), typeof(OperationsLogDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<OperationsLogDbContext>),
                typeof(AuthDesignTimeDbContextFactory<OperationsLogDbContext, OperationsLogDbContextCreator>));
    }

    private static AppSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<AppSettings>(json);
        return configuration;
    }
}