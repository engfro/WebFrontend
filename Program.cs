using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebFrontend
{
    public static class Program
    {
        private static string ApplicationName = typeof(Program).FullName;

        public static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
                    //.AddEventLog();
            });

            ILogger logger = loggerFactory.CreateLogger(ApplicationName);

            var isWindowsService = WindowsServiceHelpers.IsWindowsService();
            logger.LogInformation($"Starting up ... (IsWindowsService={isWindowsService})");

            try
            {
                logger.LogInformation($"Start creating host builder");
                IHostBuilder hostBuilder = CreateHostBuilder(args, logger);
                logger.LogInformation($"Done creating host builder");

                logger.LogInformation($"Start building host");
                using IHost host = hostBuilder.Build();
                logger.LogInformation($"Done building host");

                logger.LogInformation($"Start running host (async)");
                await host.RunAsync();
                logger.LogInformation($"Done running host (async)");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred", args);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args, ILogger logger)
        {
            logger.LogInformation("Start CreateDefaultBuilder");
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            logger.LogInformation("Done CreateDefaultBuilder");

            var isWindowsService = WindowsServiceHelpers.IsWindowsService();
            logger.LogInformation($"Start UseWindowsService (IsWindowsService={isWindowsService})");
            hostBuilder.UseWindowsService();
            logger.LogInformation($"End UseWindowsService (IsWindowsService={isWindowsService})");

            logger.LogInformation("Start ConfigureWebHostDefaults");
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://*:5000")
                        .UseHttpSys(options =>
                        {
                            options.Authentication.Schemes = AuthenticationSchemes.Negotiate | AuthenticationSchemes.NTLM;
                            options.Authentication.AllowAnonymous = false;
                        })
                        .CaptureStartupErrors(true)
                        .UseStartup<Startup>();
                });
            logger.LogInformation("End ConfigureWebHostDefaults");

            return hostBuilder;
        }
    }
}