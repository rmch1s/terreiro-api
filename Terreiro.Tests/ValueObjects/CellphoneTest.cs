using Bogus;
using FluentAssertions;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.ValueObjects;

public class CellphoneTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "Cellphone")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var ddd = _faker.Random.UInt(2).ToString();
        var number = _faker.Random.UInt(7, 8).ToString();

        // Act
        var period = new Cellphone(ddd, number);

        // Assert
        period.DDD.Should().Be(ddd);
        period.Number.Should().Be(number);
    }
}
