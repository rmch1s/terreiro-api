using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "UserEvent")]
public class UserEventTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = faker.Random.Int();
        var expectedEventId = faker.Random.Int();

        // Act
        var user = new UserEvent(expectedUserId, expectedEventId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.EventId.Should().Be(expectedEventId);
    }
}