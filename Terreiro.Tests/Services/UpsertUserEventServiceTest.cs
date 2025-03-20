using FluentAssertions;
using Moq;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserEventServiceCollection))]
[Trait("Category", "UpdateUserEventService")]
public class UpsertUserEventServiceTest
{
    private readonly UpsertUserEventServiceFixture _fixture;

    public UpsertUserEventServiceTest(UpsertUserEventServiceFixture fixture)
    {
        _fixture = fixture;
        _fixture.GenerateService();
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEmptyEvent_ThenAddEventSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();
        var expectedEvent = EventFixture.GenerateEvents(1).First();

        _fixture.UserEventRepository!.Setup(s => s.Add(It.IsAny<UserEvent>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEvent) = await _fixture.UpdateUserEventService!.Upsert(user, expectedEvent);

        // Assert
        upsertedEvent.Should().Be(expectedEvent);
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEventPassedInParameter_ThenDeleteEventSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUserMock();
        var @event = EventFixture.GenerateEvents(1).First();

        user.Setup(s => s.Events).Returns([@event]);
        _fixture.UserEventRepository!.Setup(s => s.Delete(It.IsAny<UserEvent>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEvent) = await _fixture.UpdateUserEventService!.Upsert(user.Object, @event);

        // Assert
        upsertedEvent.Should().Be(null);
    }
}
