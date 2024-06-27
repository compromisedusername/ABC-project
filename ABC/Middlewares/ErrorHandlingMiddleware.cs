using System.Net;
using ABC.Exceptions;

namespace GakkoHorizontalSlice.Middlewares;

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
            // Call the next middleware in the pipeline
            await _next(context);
        }
        catch (DomainException de)
        {
            _logger.LogError(de, de.Message);
            await HandleDomainExceptionAsync(context, de);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unhandled exception occurred");

            // Handle the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleDomainExceptionAsync(HttpContext context, DomainException de)
    {
        context.Response.StatusCode = de.StatusCode;
        context.Response.ContentType = "application/json";

        // Create a response model
        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                detail = de.Message,
                statusCode = de.StatusCode
            }
        };

        // Serialize the response model to JSON
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        // Write the JSON response to the HTTP response
        return context.Response.WriteAsync(jsonResponse);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Set the status code and response content
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        // Create a response model
        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                detail = exception.Message
            }
        };

        // Serialize the response model to JSON
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        // Write the JSON response to the HTTP response
        return context.Response.WriteAsync(jsonResponse);
    }
}