using FluentAssertions;
using Moq;
using Terreiro.Application.Exceptions;
using Terreiro.Application.Services.SetPin;
using Terreiro.Domain.Execptions;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Services;

[Trait("Category", "SetPinService")]
public class SetPinServiceTest
{
    [Fact]
    [Trait("Method", "SetPin")]
    public void SetPin_GivenAllParametersWithWrongOldPin_ThenThrowException()
    {
        // Arrange
        var userMock = UserFixture.GenerateUserMock();
        userMock.Setup(s => s.SetPin(It.IsAny<string?>(), It.IsAny<string>()))
            .Throws<WrongPinException>();
        var setPinService = new SetPinService();

        // Act
        var action = () => setPinService.SetPin(userMock.Object, It.IsAny<string?>(), It.IsAny<string>());

        // Assert
        action.Should().Throw<BadRequestException>();
    }
}
