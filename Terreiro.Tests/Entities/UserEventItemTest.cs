using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

public class UserEventItemTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "UserEventItem")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = _faker.Random.Int();
        var expectedEventItemId = _faker.Random.Int();

        // Act
        var user = new UserEventItem(expectedUserId, expectedEventItemId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.EventItemId.Should().Be(expectedEventItemId);
    }
}
