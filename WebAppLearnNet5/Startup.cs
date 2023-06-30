using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppLearnNet5.Middlewares;

namespace WebAppLearnNet5
{
    public class Startup
    {
        private static IWebHostEnvironment env;
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            env = environment;

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<LoggingMiddlewares>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome!");
                });
            });

            app.Map("/about", About);
            app.Map("/config", Config);
            //app.Run(async context =>
            //{
            //    int zero = 0;
            //    int result = 4 / zero;
            //    await context.Response.WriteAsync($"Page Not Found!");
            //});

        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }

        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Config");
            });
        }
    }
}
