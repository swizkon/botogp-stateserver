using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BotoGP.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Console.WriteLine("Startup(IConfiguration configuration)");
        Configuration = configuration;

        Console.WriteLine(string.Join(", ", Configuration.AsEnumerable().Select(x => x.Key)));


    }

    public static IConfiguration Configuration { get; set; }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        Console.WriteLine("Configure(IApplicationBuilder app, IHostingEnvironment env)");
        RuntimeEnvironment.Setup(env);

        var builder = new ConfigurationBuilder();

        if (env.IsDevelopment())
        {
            Console.WriteLine("");
            Console.WriteLine("env.ContentRootPath:");
            Console.WriteLine(env.ContentRootPath);
            Console.WriteLine("");

            builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.local.json", optional: true)
                .AddEnvironmentVariables();
        }

        Configuration = builder.Build();
        Console.WriteLine(string.Join(", ", Configuration.AsEnumerable().Select(x => x.Key)));

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
            });
            
            
       // app.MapHub<BotoGP.Hubs.RaceHub>("/race");
    }
        
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("ConfigureServices(IServiceCollection services)");
        
        // MvcOptions.EnableEndpointRouting = false'
        services.AddMvc(options =>
        {
            options.EnableEndpointRouting = false;
        });
        services.AddSignalR();

        services.AddSingleton<ICircuitRepository, CircuitRepository>();

        services.AddLogging();
    }
}