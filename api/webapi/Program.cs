using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kernel;
using Kernel.Actions;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Webapi
{
    public class Program
    {
        static IConfiguration Configuration = null;

        public static void Main(string[] args)
        {
            Configuration = CreateConfiguration();
            var builder = CreateWebHostBuilder(args);
            builder
                .UseConfiguration(Configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(Configure)
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(5000);
                })
                .Build()
                .Run();
        }

        private static IConfiguration CreateConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            return builder.Build();
        }

        private static void Configure(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
            services.Configure<ExternalSourceOptions>(Configuration.GetSection("ExternalSource"));

            services.AddSingleton<IDataProviderFactory, DataProviderFactory>();
            services.AddMediatR(typeof(GetAllPhonesRequest));
            services.AddSingleton<IServiceScope>(p => p.CreateScope());
            services.AddScoped<SynchronizationService>();
            services.AddSingleton<IHostedService>(p => {
                IServiceScope singletonScope = p.GetService(typeof(IServiceScope)) as IServiceScope;
                return singletonScope.ServiceProvider.GetService(typeof(SynchronizationService)) as IHostedService;
            });
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
