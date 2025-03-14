using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

internal class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.CPF)
            .Must(x => x.Length is 11)
            .WithMessage(x => TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(x.CPF), 11))
            .Matches(@"^\d+$")
            .WithMessage(x => TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(x.CPF)));
    }
}
