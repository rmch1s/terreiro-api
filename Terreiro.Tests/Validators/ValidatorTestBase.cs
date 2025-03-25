using FluentValidation;

namespace Terreiro.Tests.Validators;

public class ValidatorTestBase<TValidator, T> : TestBase where TValidator : AbstractValidator<T>, new()
{
    protected readonly TValidator validator = new();
}
