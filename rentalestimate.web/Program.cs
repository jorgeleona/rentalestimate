using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;

namespace rentalestimate.web
{
    public class Program
    {
        public static void Main(string[] args)
        {

			CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
		           .UseUrls("https://jldemo.azurewebsites.net/")
                   .UseStartup<Startup>()
                   .ConfigureServices(services => services.AddAutofac())
		           .UseStartup<Startup>();
    }
}
