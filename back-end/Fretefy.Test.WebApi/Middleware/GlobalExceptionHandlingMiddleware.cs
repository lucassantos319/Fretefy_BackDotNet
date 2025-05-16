using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fretefy.Test.WebApi.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) =>
            _logger = logger;
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problem = new ProblemDetails()
                {
                    Status = (int) HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Internal Server Error",
                    Detail = $"An internal server error ocurred. Exception: {ex.Message} {ex.InnerException}"
                };

                string response = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(response);
                
                context.Response.ContentType = "application/json";

            }
        }
    }
}