using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuberDinner.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext,ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorResult = new { error = "An error occurred while processing your request" };
            var result = JsonSerializer.Serialize(errorResult);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsync(result);
        }
    }
}
