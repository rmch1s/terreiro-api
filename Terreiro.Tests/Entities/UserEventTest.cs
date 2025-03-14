using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

public class UserEventTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "UserEvent")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = _faker.Random.Int();
        var expectedEventId = _faker.Random.Int();

        // Act
        var user = new UserEvent(expectedUserId, expectedEventId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.EventId.Should().Be(expectedEventId);
    }
}