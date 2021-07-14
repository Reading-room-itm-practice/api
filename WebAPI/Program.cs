using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Storage.DataAccessLayer;
using WebAPI.HosBuilderExtensions;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
                CreateHostBuilder(args).Build().MigrateDatabase<ApiDbContext>().SeedData().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((context, logging) =>
                 {
                     logging.ClearProviders();
                     logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                     logging.AddDebug();
                     logging.SetMinimumLevel(LogLevel.Trace);
                 })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
