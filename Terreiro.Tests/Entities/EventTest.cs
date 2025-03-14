using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Domain.ValueObjects;
using Terreiro.Tests.Fixtures;

namespace Terreiro.Tests.Entities;

public class EventTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "Event")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = _faker.Person.FirstName;
        var expectedPeriod = new Period(_faker.Date.Future(), _faker.Date.Future().OrNull(_faker));
        var expetedItems = EventItemFixture.GenerateEventItems(1).ToArray();
        var expectedDescription = _faker.Random.String().OrNull(_faker);

        // Act
        var @event = new Event(expectedName, expectedPeriod, expetedItems, expectedDescription);

        // Assert
        @event.Name.Should().Be(expectedName);
        @event.Period.Should().Be(expectedPeriod);
        @event.Items.Should().Equal(expetedItems);
        @event.Description.Should().Be(expectedDescription);
        @event.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "Event")]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        // Arrange
        var @event = EventFixture.GenerateEvents(1).First();

        var expectedName = _faker.Person.FirstName;
        var expectedPeriod = new Period(_faker.Date.Future(), _faker.Date.Future());
        var expectedDescription = _faker.Random.String().OrNull(_faker);

        //Act
        @event.Update(expectedName, expectedPeriod, expectedDescription);

        //Assert
        @event.Name.Should().Be(expectedName);
        @event.Period.Should().Be(expectedPeriod);
        @event.Description.Should().Be(expectedDescription);
        @event.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "Event")]
    [Trait("Method", "SetDeletedAt")]
    public void SetDeletedAt_WhenIsCalled_SetDeletedAtCorrectly()
    {
        //Arrange
        var @event = EventFixture.GenerateEvents(1).First();

        //Act
        @event.SetDeletedAt();

        //Assert
        @event.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
