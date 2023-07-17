using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebAppMVC.DB.LoggingRepository;
using WebAppMVC.Models.Db;

namespace WebAppLearnNet5.Middlewares
{
    public class LoggingMiddlewares
    {
        private readonly RequestDelegate _next;
        private ILoggingRepository _loggingRepository;

        public LoggingMiddlewares(RequestDelegate next, ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            await LogDb(context);
            await _next.Invoke(context);
        }

        private async Task LogDb(HttpContext context)
        {
            var request = new Request()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Url = $"{context.Request.Host.Value + context.Request.Path}"
            };

            await _loggingRepository.AddRequest(request);
        }
        private void LogConsole(HttpContext context) => Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    }
}
