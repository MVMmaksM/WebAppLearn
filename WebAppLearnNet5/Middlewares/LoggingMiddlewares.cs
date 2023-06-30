using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
            LogConsole(context);
            await LogFile(context);
            await _next.Invoke(context);
        }

        private async Task LogFile(HttpContext context)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            var logsList = new List<string> { $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}" };
            await File.AppendAllLinesAsync(logFilePath, logsList);
        }
        private void LogConsole(HttpContext context) => Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    }
}
