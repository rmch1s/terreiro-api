using FluentAssertions;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "UserRole")]
public class UserRoleTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedUserId = faker.Random.Int();
        var expectedRoleId = faker.Random.Int();

        // Act
        var user = new UserRole(expectedUserId, expectedRoleId);

        // Assert
        user.UserId.Should().Be(expectedUserId);
        user.RoleId.Should().Be(expectedRoleId);
    }
}
