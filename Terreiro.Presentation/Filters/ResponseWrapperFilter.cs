using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Terreiro.Application.Models;

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
            if (objectResult.Value is BaseRequestResponse<object>)
                return;

            objectResult.Value = objectResult switch
            {
                NotFoundObjectResult notFound => WrapResponse(notFound.Value),
                BadRequestObjectResult badRequest => WrapBadRequestResponse(badRequest.Value),
                UnprocessableEntityObjectResult unprocessable => WrapResponse(unprocessable.Value),
                _ => new BaseRequestResponse<object>(
                    objectResult.Value,
                    objectResult.StatusCode >= 200 && objectResult.StatusCode <= 300,
                    []
                )
            };
        }
        else
            context.Result = new ObjectResult(new BaseRequestResponse<object>(null, false, []));
    }

    private static BaseRequestResponse<object> WrapResponse(object? value)
    {
        string[] errorMessages = value switch
        {
            string message => [message],
            _ => []
        };

        return new BaseRequestResponse<object>(null, true, errorMessages);
    }

    private static BaseRequestResponse<object> WrapBadRequestResponse(object? value)
    {
        string[] errorMessages = value switch
        {
            ValidationProblemDetails validation => validation.Errors.SelectMany(x => x.Value).ToArray(),
            string message => [message],
            _ => []
        };

        return new BaseRequestResponse<object>(null, true, errorMessages);
    }
}
