using Billing.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Template.FrontApi.Db.Configuration;
using AppSettings = Billing.AccrualMoney.Consumer.Settings.AppSettings;

namespace Billing.AccrualMoney.Consumer
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