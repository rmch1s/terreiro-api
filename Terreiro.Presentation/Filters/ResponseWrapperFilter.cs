using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Terreiro.Application.Dtos;

namespace Terreiro.Presentation.Filter;

public class ResponseWrapperFilter : IResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {

    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            objectResult.Value = objectResult switch
            {
                NotFoundObjectResult notFound => WrapResponse(notFound.Value, true),
                BadRequestObjectResult badRequest => WrapBadRequestResponse(badRequest.Value),
                UnprocessableEntityObjectResult unprocessable => WrapResponse(unprocessable.Value, true),
                _ => new BaseResponseDto<object>(
                    objectResult.Value,
                    objectResult.StatusCode >= 300,
                    []
                )
            };
        }
        else
            context.Result = new ObjectResult(WrapResponse(null, false));
    }

    private static BaseResponseDto<object> WrapResponse(object? value, bool error)
    {
        string[] errorMessages = value switch
        {
            string message => [message],
            _ => []
        };

        return new BaseResponseDto<object>(null, error, errorMessages);
    }

    private static BaseResponseDto<object> WrapBadRequestResponse(object? value)
    {
        string[] errorMessages = value switch
        {
            ValidationProblemDetails validation => validation.Errors.SelectMany(x => x.Value).ToArray(),
            string message => [message],
            _ => []
        };

        return new BaseResponseDto<object>(null, true, errorMessages);
    }
}
