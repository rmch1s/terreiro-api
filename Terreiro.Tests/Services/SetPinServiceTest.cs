using FluentAssertions;
using Moq;
using Terreiro.Application.Exceptions;
using Terreiro.Application.Services.SetPin;
using Terreiro.Domain.Entities;
using Terreiro.Domain.Execptions;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Services;

[Trait("Category", "SetPinService")]
public class SetPinServiceTest
{
    [Fact]
    [Trait("Method", "SetPin")]
    public void SetPin_GivenNullUser_ThenThrowException()
    {
        // Arrange
        User? user = null;
        var setPinService = new SetPinService();

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var action = () => setPinService.SetPin(user, It.IsAny<string?>(), It.IsAny<string>());
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        action.Should().Throw<NullEntityExecption>();
    }

    [Fact]
    [Trait("Method", "SetPin")]
    public void SetPin_GivenAllParametersWithWrongOldPin_ThenThrowException()
    {
        // Arrange
        var userMock = UserFixture.GenerateUserMock();
        var setPinService = new SetPinService();

        userMock.Setup(s => s.SetPin(It.IsAny<string?>(), It.IsAny<string>()))
            .Throws<WrongPinException>();

        // Act
        var action = () => setPinService.SetPin(userMock.Object, It.IsAny<string?>(), It.IsAny<string>());

        // Assert
        action.Should().Throw<BadRequestException>();
    }
}
