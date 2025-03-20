using FluentAssertions;
using Moq;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserEventItemServiceCollection))]
[Trait("Category", "UpsertUserEventItemService")]
public class UpsertUserEventItemServiceTest(UpsertUserEventItemServiceFixture fixture) : ServiceTestBase<UpsertUserEventItemServiceFixture>(fixture)
{
    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEmptyEventItem_ThenAddEventItemSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();
        var expectedEventItem = EventItemFixture.GenerateEventItems(1).First();

        fixture.UserEventItemRepository!.Setup(s => s.Add(It.IsAny<UserEventItem>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEventItem) = await fixture.UpsertUserEventItemService!.Upsert(user, expectedEventItem);

        // Assert
        upsertedEventItem.Should().Be(expectedEventItem);
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEventItemPassedInParameter_ThenDeleteEventItemSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUserMock();
        var eventItem = EventItemFixture.GenerateEventItems(1).First();

        user.Setup(s => s.EventItems).Returns([eventItem]);
        fixture.UserEventItemRepository!.Setup(s => s.Add(It.IsAny<UserEventItem>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEventItem) = await fixture.UpsertUserEventItemService!.Upsert(user.Object, eventItem);

        // Assert
        upsertedEventItem.Should().Be(null);
    }
}
