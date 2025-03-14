﻿using Bogus;
using FluentAssertions;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.ValueObjects;

public class PeriodTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "Period")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedStartDate = _faker.Date.Future();
        var expectedEndDate = _faker.Date.Future().OrNull(_faker);

        // Act
        var period = new Period(expectedStartDate, expectedEndDate);

        // Assert
        period.StartDate.Should().Be(expectedStartDate);
        period.EndDate.Should().Be(expectedEndDate);
    }
}
