using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebAppLearnNet5.Middlewares
{
    public class LoggingMiddlewares
    {
        private readonly RequestDelegate _next;

        public LoggingMiddlewares(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            await _next.Invoke(context);
        }
    }
}
