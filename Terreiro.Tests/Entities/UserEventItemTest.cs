using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "UserEventItem")]
public class UserEventItemTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = faker.Random.Int();
        var expectedEventItemId = faker.Random.Int();

        // Act
        var user = new UserEventItem(expectedUserId, expectedEventItemId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.EventItemId.Should().Be(expectedEventItemId);
    }
}
