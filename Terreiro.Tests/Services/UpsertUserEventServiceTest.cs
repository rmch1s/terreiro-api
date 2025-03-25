using FluentAssertions;
using Moq;
using Terreiro.Application.Exceptions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserEventServiceCollection))]
[Trait("Category", "UpdateUserEventService")]
[Trait("Method", "Upsert")]
public class UpsertUserEventServiceTest(UpsertUserEventServiceFixture fixture) :
    ServiceTestBase<UpsertUserEventServiceFixture>(fixture)
{
    [Theory]
    [MemberData(nameof(GetInvalidUpsertInputs))]
    public void Upsert_GivenNullUserOrNullEvent_ThenThrowException(User? user, Event? @event)
    {
        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var action = async () => await fixture.UpsertUserEventService!.Upsert(user, @event);
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        action.Should().ThrowAsync<NullEntityExecption>();
    }

    [Fact]
    public async Task Upsert_GivenUserWithEmptyEvent_ThenAddEventSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();
        var expectedEvent = EventFixture.GenerateEvents(1).First();

        fixture.UserEventRepository!.Setup(s => s.Add(It.IsAny<UserEvent>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEvent) = await fixture.UpsertUserEventService!.Upsert(user, expectedEvent);

        // Assert
        upsertedEvent.Should().Be(expectedEvent);
    }

    [Fact]
    public async Task Upsert_GivenUserWithEventPassedInParameter_ThenDeleteEventSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUserMock();
        var @event = EventFixture.GenerateEvents(1).First();

        user.Setup(s => s.Events).Returns([@event]);
        fixture.UserEventRepository!.Setup(s => s.Delete(It.IsAny<UserEvent>())).ReturnsAsync(1);

        // Act
        (_, var upsertedEvent) = await fixture.UpsertUserEventService!.Upsert(user.Object, @event);

        // Assert
        upsertedEvent.Should().Be(null);
    }

    public static IEnumerable<object?[]> GetInvalidUpsertInputs()
    {
        yield return [UserFixture.GenerateUsers(1).First(), null];
        yield return [null, EventFixture.GenerateEvents(1).First()];
    }
}
