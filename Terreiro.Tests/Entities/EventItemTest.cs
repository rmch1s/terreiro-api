using FluentAssertions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;

namespace Terreiro.Tests.Entities;

[Trait("Category", "EventItem")]
public class EventItemTest : TestBase
{
    [Fact]
    [Trait("Method", "Constructor")]
    public void Constructor_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var expectedName = faker.Random.String(5, 100);
        var expectedQuantity = faker.Random.Int();
        var expectedEventId = faker.Random.Int();

        //Act
        var eventItem = new EventItem(expectedName, expectedQuantity, expectedEventId);

        //Arrange
        eventItem.Name.Should().Be(expectedName);
        eventItem.Quantity.Should().Be(expectedQuantity);
        eventItem.EventId.Should().Be(expectedEventId);
        eventItem.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    [Trait("Method", "Update")]
    public void Update_GivenAllParameters_ThenSetPropertiesCorrectly()
    {
        //Arrange
        var eventItem = EventItemFixture.GenerateEventItems(1).First();

        var expectedName = faker.Person.FirstName;
        var expectedQuantity = faker.Random.Int();

        //Act
        eventItem.Update(expectedName, expectedQuantity);

        //Assert
        eventItem.Name.Should().Be(expectedName);
        eventItem.Quantity.Should().Be(expectedQuantity);
        eventItem.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
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