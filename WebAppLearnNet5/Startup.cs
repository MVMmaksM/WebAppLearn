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

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                string logFilePath = Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt");
                var logsList = new List<string> { $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}" };
                await File.AppendAllLinesAsync(logFilePath, logsList);

                await next.Invoke();
            });

            app.UseMiddleware<LoggingMiddlewares>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome!");
                });

                //endpoints.Map("/about", async context =>
                //{
                //    await context.Response.WriteAsync("About");
                //});

                //endpoints.MapGet("/config", async context =>
                //{
                //    await context.Response.WriteAsync($"Config");
                //});
            });

            app.Map("/about", About);
            app.Map("/config", Config);
            app.Run(async context => await context.Response.WriteAsync($"Page Not Found!"));
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
