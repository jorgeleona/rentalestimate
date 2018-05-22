using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System;

namespace rentalestimate.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		public IContainer ApplicationContainer { get; private set; }
		public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
        {
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();

			// In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

			var builder = new ContainerBuilder();
            builder.Populate(services);

			Assembly servicesAssembly = Assembly.Load("rentalestimate.service");
            builder.RegisterAssemblyTypes(servicesAssembly).
                   Where(t => t.Name.EndsWith("Service", System.StringComparison.OrdinalIgnoreCase)).
                   AsImplementedInterfaces().
                   InstancePerRequest().
                   InstancePerLifetimeScope();

            Assembly dataAccessAssembly = Assembly.Load("rentalestimate.dataaccess");
            builder.RegisterAssemblyTypes(dataAccessAssembly).
                   Where(t => t.Name.EndsWith("Repository", System.StringComparison.OrdinalIgnoreCase)).
                   AsImplementedInterfaces().
                   InstancePerRequest().
                   InstancePerLifetimeScope();



           

			this.ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
