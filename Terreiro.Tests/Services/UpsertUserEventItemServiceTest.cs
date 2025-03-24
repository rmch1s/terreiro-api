using FluentAssertions;
using Moq;
using Terreiro.Application.Exceptions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserEventItemServiceCollection))]
[Trait("Category", "UpsertUserEventItemService")]
public class UpsertUserEventItemServiceTest(UpsertUserEventItemServiceFixture fixture) :
    ServiceTestBase<UpsertUserEventItemServiceFixture>(fixture)
{
    [Theory]
    [Trait("Method", "Upsert")]
    [MemberData(nameof(GetInvalidUpsertInputs))]
    public void Upsert_GivenNullUserOrNullEventItem_ThenThrowException(User? user, EventItem? eventItem)
    {
        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var action = async () => await fixture.UpsertUserEventItemService!.Upsert(user, eventItem);
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        action.Should().ThrowAsync<NullEntityExecption>();
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GivenUserWithEmptyEventItem_ThenAddEventItemSuccessfully()
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
    public async Task Upsert_GivenUserWithEventItemPassedInParameter_ThenDeleteEventItemSuccessfully()
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

    public static IEnumerable<object?[]> GetInvalidUpsertInputs()
    {
        yield return [UserFixture.GenerateUsers(1).First(), null];
        yield return [null, EventItemFixture.GenerateEventItems(1).First()];
    }
}
