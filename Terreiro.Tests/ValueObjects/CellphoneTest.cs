using FluentAssertions;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.ValueObjects;

[Trait("Category", "Cellphone")]
[Trait("Method", "Constructor")]
public class CellphoneTest : TestBase
{
    [Fact]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var ddd = faker.Random.UInt(2).ToString();
        var number = faker.Random.UInt(7, 8).ToString();

        // Act
        var period = new Cellphone(ddd, number);

        // Assert
        period.DDD.Should().Be(ddd);
        period.Number.Should().Be(number);
    }
}
