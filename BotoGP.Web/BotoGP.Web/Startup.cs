using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotoGP.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ConfigureServices");
            services.AddMvc();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RuntimeEnvironment.Setup(env);

            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                Console.WriteLine("");
                Console.WriteLine("env.ContentRootPath:");
                Console.WriteLine(env.ContentRootPath);
                Console.WriteLine("");

				builder.SetBasePath(env.ContentRootPath)
	                    .AddJsonFile("appsettings.local.json", optional: false)
	                    .AddEnvironmentVariables();
            }

            Configuration = builder.Build();
            Console.WriteLine( string.Join(", ", Configuration.AsEnumerable().Select(x => x.Key)));

            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseDefaultFiles()
               .UseStaticFiles()

            .UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            })
            .UseSignalR(routes =>
            {
                routes.MapHub<BotoGP.Hubs.RaceHub>("race");
            });
        }
    }
}
