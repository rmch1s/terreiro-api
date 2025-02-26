using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Terreiro.Application.Models;

namespace Terreiro.Presentation.Filter;

public class AsyncAutoValidation(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            if (parameter?.BindingInfo?.BindingSource == BindingSource.Body ||
                (parameter?.BindingInfo?.BindingSource == BindingSource.Query && parameter.ParameterType.IsClass))
            {
                var validator = serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameter.ParameterType)) as IValidator;
                if (validator is not null)
                {
                    var subject = context.ActionArguments[parameter.Name];
                    var result = await validator.ValidateAsync(new ValidationContext<object?>(subject), context.HttpContext.RequestAborted);
                    if (!result.IsValid)
                        result.AddToModelState(context.ModelState, null);
                }
            }
        }

        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .Select(x => string.Join(",", x.Value?.Errors.Select(e => e.ErrorMessage) ?? []))
                .ToArray();

            var result = new BaseRequestResponse<object>(
                data: null,
                error: true,
                errorMessages: errors
            );

            context.Result = new BadRequestObjectResult(result);
        }
        else await next();
    }
}


