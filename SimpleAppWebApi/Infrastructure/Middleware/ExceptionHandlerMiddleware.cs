using System;
using System.Data;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                context.Response.ContentType = "application/json";
                var response = context.Response;
                switch (exception)
                {
                    case DbUpdateConcurrencyException concurrencyException:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        _logger.LogError(concurrencyException, "Changes to this 'customer' record can't be saved.");
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError(exception, "An unhandled exception has occurred");
                        break;
                }

                var result = JsonConvert.SerializeObject(exception);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
