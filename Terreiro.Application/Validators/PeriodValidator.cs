using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Resources;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Validators;

public class PeriodValidator : AbstractValidator<Period>
{
    public PeriodValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage(x => TerreiroResource.FIELD_EMPTY.InsertParams(nameof(x.StartDate)));

        When(x => x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate!.Value)
                .NotEmpty()
                .WithMessage(x => TerreiroResource.FIELD_EMPTY.InsertParams(nameof(x.EndDate)))
                .GreaterThan(x => x.StartDate)
                .WithMessage(x => TerreiroResource.FIELD_GREATER_THAN.InsertParams(nameof(x.EndDate), nameof(x.StartDate)));

        });
    }
}
