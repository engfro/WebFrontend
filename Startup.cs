using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.ServiceProcess;

namespace WebFrontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(n =>
            {
                n.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                n.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class CustomWindowsService : ServiceBase
    {
        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Press <Enter> to exit");
            Console.ReadLine();
            OnStop();
        }

        protected override async void OnStart(string[] args)
        {
            Console.WriteLine("OnStart ... ");

            IHostBuilder hostBuilder = CreateHostBuilder(args);

            using IHost host = hostBuilder.Build();

            await host.RunAsync();//host.Run();
        }

        protected override void OnStop()
        {
            Console.WriteLine("OnStop ... ");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseUrls("http://*:5000")
                    .UseHttpSys(options =>
                    {
                        options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                        options.Authentication.AllowAnonymous = false;
                    })
                    .CaptureStartupErrors(true)
                    .UseStartup<Startup>();
            });

            return hostBuilder;
        }
    }
}