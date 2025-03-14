using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.ValueObjects;

namespace Terreiro.Tests.Entities;

public class UserTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "User")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = _faker.Random.String(5, 100);
        var expectedCpf = _faker.Random.String(11);
        var expectedCellphone = CellphoneFixture.GenerateCellphones(1).First();

        // Act
        var user = new User(expectedName, expectedCpf, expectedCellphone);

        // Assert
        user.Name.Should().Be(expectedName);
        user.CPF.Should().Be(expectedCpf);
        user.Cellphone.Should().Be(expectedCellphone);
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "User")]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();

        var expectedName = _faker.Random.String(5, 100);
        var expectedCpf = _faker.Random.String(11);
        var expectedCellphone = CellphoneFixture.GenerateCellphones(1).First();

        //Act
        user.Update(expectedName, expectedCpf, expectedCellphone);

        //Assert
        user.Name.Should().Be(expectedName);
        user.CPF.Should().Be(expectedCpf);
        user.Cellphone.Should().Be(expectedCellphone);
        user.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "User")]
    [Trait("Method", "SetDeletedAt")]
    public void SetDeletedAt_WhenIsCalled_SetDeletedAtCorrectly()
    {
        //Arrange
        var user = UserFixture.GenerateUsers(1).First();

        //Act
        user.SetDeletedAt();

        //Assert
        user.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
