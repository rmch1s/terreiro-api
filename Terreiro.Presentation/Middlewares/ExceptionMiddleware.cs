using System.Net;
using System.Text.Json;
using Terreiro.Application.Exceptions;
using Terreiro.Application.Models;
using Terreiro.Application.Resources;

namespace Terreiro.Presentation.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            BadRequestExeption => ((int)HttpStatusCode.BadRequest, exception.Message),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, exception.Message),
            _ => ((int)HttpStatusCode.InternalServerError, TerreiroResource.INTERNAL_SERVER_ERROR_MESSAGE)
        };

        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(new BaseRequestResponse<object>(null, true, [message]));

        return context.Response.WriteAsync(result);
    }
}

