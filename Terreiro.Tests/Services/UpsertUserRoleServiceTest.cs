using FluentAssertions;
using Moq;
using Terreiro.Application.Exceptions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserRoleServiceCollection))]
[Trait("Category", "UpsertUserRoleService")]
public class UpsertUserRoleServiceTest(UpsertUserRoleServiceFixture fixture) : ServiceTestBase<UpsertUserRoleServiceFixture>(fixture)
{
    [Theory]
    [Trait("Method", "Upsert")]
    [MemberData(nameof(GetInvalidUpsertInputs))]
    public void Upsert_GivenNullUserOrNullRole_ThenThrowException(User? user, Role? role)
    {
        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var action = async () => await fixture.UpsertUserRoleService!.Upsert(user, role);
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        action.Should().ThrowAsync<NullEntityExecption>();
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithEmptyRole_ThenAddRoleSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUsers(1).First();
        var expectedRole = RoleFixture.GenerateRoles(1).First();

        fixture.UserRoleRepository!.Setup(s => s.Add(It.IsAny<UserRole>())).ReturnsAsync(1);

        // Act
        (_, var upsertedRole) = await fixture.UpsertUserRoleService!.Upsert(user, expectedRole);

        // Assert
        upsertedRole.Should().Be(expectedRole);
    }

    [Fact]
    [Trait("Method", "Upsert")]
    public async Task Upsert_GiveUserWithRolePassedInParameter_ThenDeleteRoleSuccessfully()
    {
        // Arrange
        var user = UserFixture.GenerateUserMock();
        var @event = RoleFixture.GenerateRoles(1).First();

        user.Setup(s => s.Roles).Returns([@event]);
        fixture.UserRoleRepository!.Setup(s => s.Delete(It.IsAny<UserRole>())).ReturnsAsync(1);

        // Act
        (_, var upsertedRole) = await fixture.UpsertUserRoleService!.Upsert(user.Object, @event);

        // Assert
        upsertedRole.Should().Be(null);
    }

    public static IEnumerable<object?[]> GetInvalidUpsertInputs()
    {
        yield return [UserFixture.GenerateUsers(1).First(), null];
        yield return [null, RoleFixture.GenerateRoles(1).First()];
    }
}
