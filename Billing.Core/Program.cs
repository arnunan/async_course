using Billing.Core.Extensions;
using Billing.Core.Settings;
using Microsoft.Extensions.Hosting;
using Template.FrontApi.Db.Configuration;

namespace Billing.Core;

public class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddAsyncCourseProperties();
        builder.Services.AddAsyncCourseDbSettings<AppSettings>();

        var host = builder.Build();
        host.Run();
    }
}