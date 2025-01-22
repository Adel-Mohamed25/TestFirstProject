using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Project.Apis.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                ProblemDetails problem = new()
                {
                    Status = context.Response.StatusCode,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
