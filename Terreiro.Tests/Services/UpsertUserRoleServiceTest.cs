﻿using FluentAssertions;
using Moq;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(UpsertUserRoleServiceCollection))]
[Trait("Category", "UpsertUserRoleService")]
public class UpsertUserRoleServiceTest(UpsertUserRoleServiceFixture fixture) : ServiceTestBase<UpsertUserRoleServiceFixture>(fixture)
{
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
}
