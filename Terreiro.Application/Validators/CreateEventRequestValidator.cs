using FluentValidation;
using Terreiro.Application.Helpers;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;

namespace Terreiro.Application.Validators;

public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventRequestValidator()
    {
        RuleFor(x => x.Name)
            .Must(x => x.Length >= 5 && x.Length <= 100)
            .WithMessage(x => TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(x.Name), 5, 100));

        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description)
                .Must(x => x.Length >= 5 && x.Length <= 100)
                .WithMessage(x => TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(x.Description), 5, 300));
        });

        RuleForEach(x => x.Items)
            .SetValidator(new UpsertEventItemRequestValidator());

        RuleFor(x => x.Period)
            .SetValidator(new PeriodValidator());
    }
}
