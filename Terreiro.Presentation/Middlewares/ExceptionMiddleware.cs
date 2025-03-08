using System.Net;
using System.Text.Json;
using Terreiro.Application.Dtos;
using Terreiro.Application.Exceptions;
using Terreiro.Application.Resources;

namespace Terreiro.Presentation.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
            await HandleErrorResponse(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            BadRequestException => ((int)HttpStatusCode.BadRequest, exception.Message),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, exception.Message),
            _ => ((int)HttpStatusCode.InternalServerError, TerreiroResource.INTERNAL_SERVER_ERROR_MESSAGE)
        };

        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(
            new BaseResponseDto<object>(null, true, [message]));

        return context.Response.WriteAsync(result);
    }

    private static async Task HandleErrorResponse(HttpContext context)
    {
        string[]? errors = context.Response.StatusCode switch
        {
            StatusCodes.Status401Unauthorized => [TerreiroResource.UNAUTHORIZED_MESSAGE],
            StatusCodes.Status403Forbidden => [TerreiroResource.FORBIDDEN_MESSAGE],
            _ => null
        };

        if (errors is null)
            return;

        var result = new BaseResponseDto<object>(null, true, errors);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var newResponseBody = JsonSerializer.Serialize(result, jsonSerializerOptions);

        await context.Response.WriteAsync(newResponseBody);
    }
}