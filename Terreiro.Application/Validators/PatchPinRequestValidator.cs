using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

internal class PatchPinRequestValidator : AbstractValidator<PatchPinRequest>
{
    public PatchPinRequestValidator()
    {
        RuleFor(x => x.OldPin)
            .Must(x => x is null || x.Length is 4)
            .WithMessage(x => TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(x.OldPin), 4))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.OldPin)));

        RuleFor(x => x.NewPin)
            .Must(x => x.Length is 4)
            .WithMessage(x => TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(x.NewPin), 4))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.OldPin)));
    }
}
