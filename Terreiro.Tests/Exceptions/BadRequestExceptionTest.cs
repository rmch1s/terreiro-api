using FluentAssertions;
using Terreiro.Application.Exceptions;

namespace Terreiro.Tests.Exceptions;

[Trait("Category", "Role")]
[Trait("Method", "Constructor")]
public class BadRequestExceptionTest : TestBase
{
    [Fact]
    public void Constructor_GivenMessage_ThenSetMessageCorrectlyToException()
    {
        // Arrange
        var expectedMessage = faker.Random.String(20);

        // Act
        var exception = new BadRequestException(expectedMessage);

        // Assert
        exception.Message.Should().Be(expectedMessage);
    }
}
