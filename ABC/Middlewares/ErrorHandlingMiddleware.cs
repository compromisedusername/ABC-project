using System.Net;
using System.Net.Http.Headers;
using ABC.Exceptions;

namespace ABC.Middlewares;

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
        catch (DomainException de)
        {
            _logger.LogError(de, de.Message);
            await HandleDomainExceptionAsync(context, de);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleDomainExceptionAsync(HttpContext context, DomainException de)
    {
        context.Response.StatusCode = de.StatusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                detail = de.Message,
                statusCode = de.StatusCode
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                detail = exception.Message
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}