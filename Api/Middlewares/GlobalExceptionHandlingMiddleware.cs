using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            await SetContextResponseAsync(context, (int)HttpStatusCode.BadRequest, "BadRequest", "Validation error", e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        
            await SetContextResponseAsync(context, (int)HttpStatusCode.InternalServerError, "InternalServerError", "Server error", "An internal server error has ocurred.");
        }
    }

    private async Task SetContextResponseAsync(HttpContext context, int statusCode, string type, string title, string detail)
    {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
        
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails() {
                Status = statusCode,
                Type = type,
                Title = title,
                Detail = detail,
            }));
    }
}