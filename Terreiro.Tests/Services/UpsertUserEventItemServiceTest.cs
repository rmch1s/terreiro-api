using FluentAssertions;
using Moq;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserEventItemServiceCollection))]
[Trait("Category", "UpsertUserEventItemService")]
public class UpsertUserEventItemServiceTest
{
    private readonly UpsertUserEventItemServiceFixture _fixture;

    public UpsertUserEventItemServiceTest(UpsertUserEventItemServiceFixture fixture)
    {
        _fixture = fixture;
        _fixture.GenerateService();
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEmptyEventItem_ThenAddEventItemSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();
        var expectedEventItem = EventItemFixture.GenerateEventItems(1).First();

        _fixture.UserEventItemRepository!.Setup(s => s.Add(It.IsAny<UserEventItem>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEventItem) = await _fixture.UpsertUserEventItemService!.Upsert(user, expectedEventItem);

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
        _fixture.UserEventItemRepository!.Setup(s => s.Add(It.IsAny<UserEventItem>())).ReturnsAsync(1);

        // Act
        // Act
        (_, var upsertedEventItem) = await _fixture.UpsertUserEventItemService!.Upsert(user.Object, eventItem);

        // Assert
        upsertedEventItem.Should().Be(null);
    }
}
