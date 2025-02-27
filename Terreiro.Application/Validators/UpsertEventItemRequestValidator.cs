using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

public class UpsertEventItemRequestValidator : AbstractValidator<UpsertEventItemRequest>
{
    public UpsertEventItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .Must(x => x.Length >= 5 && x.Length <= 100)
            .WithMessage(x => TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(x.Name), 5, 100));

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage(x => TerreiroResource.FIELD_GREATER_THAN.InsertParams(nameof(x.Quantity), 0));
    }
}
