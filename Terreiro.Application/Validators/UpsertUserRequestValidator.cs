using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

internal class UpsertUserRequestValidator : AbstractValidator<UpsertUserRequest>
{
    public UpsertUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .Must(x => x.Length >= 5 && x.Length <= 100)
            .WithMessage(x => TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(x.Name), 5, 100));

        RuleFor(x => x.CPF)
            .Must(x => x.Length is 11)
            .WithMessage(x => TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(x.CPF), 11))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.CPF)));

        RuleFor(x => x.Cellphone)
            .SetValidator(new CellphoneValidator());
    }
}
