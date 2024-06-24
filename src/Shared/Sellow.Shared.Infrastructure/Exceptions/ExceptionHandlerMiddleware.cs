using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sellow.Shared.Abstractions.Exceptions;

namespace Sellow.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{Message}", exception.Message);

            await HandleException(context, exception);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        var message = "Whops! Something went wrong.";
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "server_error";

        if (exception is PresentableException presentableException)
        {
            message = presentableException.Message;
            statusCode = presentableException.StatusCode;
            errorCode = presentableException.ErrorCode;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            message,
            statusCode,
            errorCode
        });
    }
}