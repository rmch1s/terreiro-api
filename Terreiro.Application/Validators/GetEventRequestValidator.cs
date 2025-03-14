using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

public class GetEventRequestValidator : AbstractValidator<GetEventRequest>
{
    public GetEventRequestValidator()
    {
        When(x => x.StartDate.HasValue, () =>
        {
            RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage(x => TerreiroResource.FIELD_EMPTY.InsertParams(nameof(x.StartDate)));
        });

        When(x => x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate!.Value)
                .NotEmpty()
                .WithMessage(x => TerreiroResource.FIELD_EMPTY.InsertParams(nameof(x.EndDate)));
        });

        When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage(x => TerreiroResource.FIELD_GREATER_THAN.InsertParams(nameof(x.EndDate), nameof(x.StartDate)));
        });
    }
}
