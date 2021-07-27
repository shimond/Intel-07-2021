using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<LoggingMiddleware> logger)
        {
            logger.LogInformation("Request Started {verb} {url}", httpContext.Request.Method, httpContext.Request.Path.Value);
            using (logger.BeginScope(new Dictionary<string, object>() { ["verb"] = httpContext.Request.Method, ["Url"] = httpContext.Request.Path.Value }))
            {
                await _next(httpContext);
                logger.LogInformation("Request End {verb} {url}", httpContext.Request.Method, httpContext.Request.Path.Value);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
