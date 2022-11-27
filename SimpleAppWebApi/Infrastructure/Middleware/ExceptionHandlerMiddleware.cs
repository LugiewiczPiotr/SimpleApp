using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SimpleApp.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                var result = JsonConvert.SerializeObject(exception);
                _logger.LogError(exception, "An unhandled exception has occurred");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }
}
