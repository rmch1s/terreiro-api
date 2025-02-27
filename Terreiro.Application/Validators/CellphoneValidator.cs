using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Resources;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Validators;

public class CellphoneValidator : AbstractValidator<Cellphone>
{
    public CellphoneValidator()
    {
        RuleFor(x => x.DDD)
            .Must(x => x.Length is 2)
            .WithMessage(x => TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(x.DDD), 2))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.DDD)));

        RuleFor(x => x.Number)
            .Must(x => x.Length is 9 || x.Length is 8)
            .WithMessage(x => TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(x.Number), 8, 9))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.Number)));
    }
}
