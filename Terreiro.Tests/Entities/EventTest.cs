using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Domain.ValueObjects;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "Event")]
public class EventTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = faker.Random.String(5, 100);
        var expectedPeriod = new Period(faker.Date.Future(), faker.Date.Future().OrNull(faker));
        var expectedItems = EventItemFixture.GenerateEventItems(1).ToArray();
        var expectedDescription = faker.Random.String(5, 300).OrNull(faker);

        // Act
        var @event = new Event(expectedName, expectedPeriod, expectedItems, expectedDescription);

        // Assert
        @event.Name.Should().Be(expectedName);
        @event.Period.Should().Be(expectedPeriod);
        @event.Items.Should().Equal(expectedItems);
        @event.Description.Should().Be(expectedDescription);
        @event.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        // Arrange
        var @event = EventFixture.GenerateEvents(1).First();

        var expectedName = faker.Person.FirstName;
        var expectedPeriod = new Period(faker.Date.Future(), faker.Date.Future());
        var expectedDescription = faker.Random.String().OrNull(faker);

        //Act
        @event.Update(expectedName, expectedPeriod, expectedDescription);

        //Assert
        @event.Name.Should().Be(expectedName);
        @event.Period.Should().Be(expectedPeriod);
        @event.Description.Should().Be(expectedDescription);
        @event.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
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
