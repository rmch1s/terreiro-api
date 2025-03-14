using Bogus;
using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Entities;

public class EventItemTest
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    [Trait("Category", "EventItem")]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = _faker.Person.FirstName;
        var expectedQuantity = _faker.Random.Int();
        var expectedEventId = _faker.Random.Int();

        //Act
        var eventItem = new EventItem(expectedName, expectedQuantity, expectedEventId);

        //Arrange
        eventItem.Name.Should().Be(expectedName);
        eventItem.Quantity.Should().Be(expectedQuantity);
        eventItem.EventId.Should().Be(expectedEventId);
        eventItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "EventItem")]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var eventItem = EventItemFixture.GenerateEventItems(1).First();

        var expectedName = _faker.Person.FirstName;
        var expectedQuantity = _faker.Random.Int();

        //Act
        eventItem.Update(expectedName, expectedQuantity);

        //Assert
        eventItem.Name.Should().Be(expectedName);
        eventItem.Quantity.Should().Be(expectedQuantity);
        eventItem.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Category", "EventItem")]
    [Trait("Method", "SetDeletedAt")]
    public void SetDeletedAt_WhenIsCalled_SetDeletedAtCorrectly()
    {
        //Arrange
        var eventItem = EventItemFixture.GenerateEventItems(1).First();

        //Act
        eventItem.SetDeletedAt();

        //Assert
        eventItem.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}