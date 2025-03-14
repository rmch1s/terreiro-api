using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Entities;

public class RoleTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "Role")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = _faker.Person.FirstName;
        var expectedDescription = _faker.Random.String().OrNull(_faker);

        // Act
        var role = new Role(expectedName, expectedDescription);

        // Assert
        role.Name.Should().Be(expectedName);
        role.Description.Should().Be(expectedDescription);
        role.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "Role")]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        // Arrange
        var role = RoleFixture.GenerateRoles(1).First();

        var expectedName = _faker.Random.String(5, 100);
        var expectedDescription = _faker.Random.String(5, 300).OrNull(_faker);

        //Act
        role.Update(expectedName, expectedDescription);

        //Assert
        role.Name.Should().Be(expectedName);
        role.Description.Should().Be(expectedDescription);
        role.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "Role")]
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
