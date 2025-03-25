using FluentValidation.TestHelper;
using Terreiro.Application.Helpers;
using Terreiro.Application.Resources;
using Terreiro.Application.Validators;
using Terreiro.Domain.ValueObjects;
using Terreiro.Tests.Fixtures.ValueObjects;

namespace Terreiro.Tests.Validators;

[Trait("Category", "CellphoneValidator")]
[Trait("Method", "Validate")]
public class CellphoneValidatorTest : ValidatorTestBase<CellphoneValidator, Cellphone>
{
    [Fact]
    public void Validate_GivenWrongDDD_ThenReturnError()
    {
        // Assert
        var cellphone = new Cellphone("A", faker.Random.UInt(11111111, 999999999).ToString());

        // Act & Assert
        validator.TestValidate(cellphone)
            .ShouldHaveValidationErrorFor(x => x.DDD)
            .WithErrorMessage(TerreiroResource.FIELD_MUST_BE_LENGTH.InsertParams(nameof(cellphone.DDD), 2));
        validator.TestValidate(cellphone)
            .ShouldHaveValidationErrorFor(x => x.DDD)
            .WithErrorMessage(TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(cellphone.DDD)));
        validator.TestValidate(cellphone)
            .ShouldNotHaveValidationErrorFor(x => x.Number);
    }

    [Fact]
    public void Validate_GivenWrongNumber_ThenReturnError()
    {
        // Assert
        var cellphone = new Cellphone(faker.Random.Int(11, 99).ToString(), "A".ToString());

        // Act & Assert
        validator.TestValidate(cellphone)
            .ShouldHaveValidationErrorFor(x => x.Number)
            .WithErrorMessage(TerreiroResource.FIELD_BETWEEN_LENGTH.InsertParams(nameof(cellphone.Number), 8, 9));
        validator.TestValidate(cellphone)
            .ShouldHaveValidationErrorFor(x => x.Number)
            .WithErrorMessage(TerreiroResource.FIELD_ONLY_NUMBERS.InsertParams(nameof(cellphone.Number)));
        validator.TestValidate(cellphone)
            .ShouldNotHaveValidationErrorFor(x => x.DDD);
    }

    [Fact]
    public void Validate_GivenCorrectlyCellphone_ThenNotReturnError()
    {
        // Assert
        var cellphone = CellphoneFixture.GenerateCellphones(1).First();

        // Act & Assert
        validator.TestValidate(cellphone)
            .ShouldNotHaveValidationErrorFor(x => x.DDD);
        validator.TestValidate(cellphone)
            .ShouldNotHaveValidationErrorFor(x => x.Number);
    }
}
