using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

public class UserRoleTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "UserRole")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = _faker.Random.Int();
        var expectedRoleId = _faker.Random.Int();

        // Act
        var user = new UserRole(expectedUserId, expectedRoleId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.RoleId.Should().Be(expectedRoleId);
    }
}
