using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WebFrontend
{
    public static class Program
    {
        private static string ApplicationName = typeof(Program).FullName;

        public static void Main(string[] args)
        {
            var isWindowsService = Microsoft.Extensions.Hosting.WindowsServices.WindowsServiceHelpers.IsWindowsService();

            var service = new CustomWindowsService();
            if (isWindowsService)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                service.RunAsConsole(args);
            }
        }
    }
}