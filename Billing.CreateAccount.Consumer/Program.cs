using Billing.Core.Extensions;
using Billing.Core.Settings;
using Microsoft.Extensions.Hosting;
using Template.FrontApi.Db.Configuration;

namespace Billing.CreateAccount.Consumer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddAsyncCourseProperties();
            builder.Services.AddAsyncCourseDbSettings<AppSettings>();

            var host = builder.Build();
            host.Run();
        }
    }
}