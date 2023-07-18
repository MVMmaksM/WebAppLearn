using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLearnNet5.Middlewares;
using WebAppMVC.DB;
using WebAppMVC.DB.LoggingRepository;
using WebAppMVC.DB.Repository;

namespace WebAppMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(options=>options.UseSqlServer(connectionString));

            services.AddSingleton<ILoggingRepository, LoggingRepository>();
            string loggingConnectionString = Configuration.GetConnectionString("LoggingConnectionString");
            services.AddDbContext<LoggingContext>(options => options.UseSqlServer(loggingConnectionString), ServiceLifetime.Singleton);
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseMiddleware<LoggingMiddlewares>();

            app.UseHttpsRedirection();

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
}
