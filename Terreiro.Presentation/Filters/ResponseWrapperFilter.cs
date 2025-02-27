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
        if (context.Result is NotFoundObjectResult notFoundResult)
        {
            string[]? errorMessages = notFoundResult.Value is not string errorMessage ? [] : [errorMessage];
            notFoundResult.Value = new BaseRequestResponse<object>(null, true, errorMessages);
        }
        else if (context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            if (badRequestObjectResult?.Value is BaseRequestResponse<object>)
                return;

            string[]? errorMessages = badRequestObjectResult!.Value is not string errorMessage ? [] : [errorMessage];
            badRequestObjectResult.Value = new BaseRequestResponse<object>(null, true, errorMessages);
        }
        else if (context.Result is UnprocessableEntityObjectResult unprocessableEntityObjectResult)
        {
            string[]? errorMessages = unprocessableEntityObjectResult!.Value is not string errorMessage ? [] : [errorMessage];
            unprocessableEntityObjectResult.Value = new BaseRequestResponse<object>(null, true, errorMessages);
        }
        else if (context.Result is ObjectResult objectResult)
        {
            var result = objectResult.Value;
            objectResult.Value = new BaseRequestResponse<object>(result, false, []);
        }
        else
        {
            context.Result = new ObjectResult(new BaseRequestResponse<object>(null, false, []));
        }
    }
}
