using Bogus;
using FluentAssertions;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.ValueObjects;

public class PeriodTest : TestBase
{
    [Fact]
    [Trait("Category", "Period")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedStartDate = faker.Date.Future();
        var expectedEndDate = faker.Date.Future().OrNull(faker);

        // Act
        var period = new Period(expectedStartDate, expectedEndDate);

        // Assert
        period.StartDate.Should().Be(expectedStartDate);
        period.EndDate.Should().Be(expectedEndDate);
    }
}
