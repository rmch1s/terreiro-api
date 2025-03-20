using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "Role")]
public class RoleTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = faker.Person.FirstName;
        var expectedDescription = faker.Random.String().OrNull(faker);

        // Act
        var role = new Role(expectedName, expectedDescription);

        // Assert
        role.Name.Should().Be(expectedName);
        role.Description.Should().Be(expectedDescription);
        role.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        // Arrange
        var role = RoleFixture.GenerateRoles(1).First();

        var expectedName = faker.Random.String(5, 100);
        var expectedDescription = faker.Random.String(5, 300).OrNull(faker);

        //Act
        role.Update(expectedName, expectedDescription);

        //Assert
        role.Name.Should().Be(expectedName);
        role.Description.Should().Be(expectedDescription);
        role.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Method", "SetDeletedAt")]
    public void SetDeletedAt_WhenIsCalled_SetDeletedAtCorrectly()
    {
        //Arrange
        var role = RoleFixture.GenerateRoles(1).First();

        //Act
        role.SetDeletedAt();

        //Assert
        role.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
