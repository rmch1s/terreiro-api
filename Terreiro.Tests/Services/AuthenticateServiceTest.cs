using FluentAssertions;
using Terreiro.Application.Exceptions;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(AuthenticateServiceCollection))]
[Trait("Category", "AuthenticateService")]
public class AuthenticateServiceTest(AuthenticateServiceFixture fixture) : ServiceTestBase<AuthenticateServiceFixture>(fixture)
{
    [Fact]
    [Trait("Method", "GenerateToken")]
    public void GenerateToken_GivenNullUser_ThenThrowException()
    {
        // Arrange
        User? user = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var action = () => fixture.AuthenticateService!.GenerateToken(user);
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        action.Should().Throw<NullEntityExecption>();
    }

    [Fact]
    [Trait("Method", "GenerateToken")]
    public void GenerateToken_GivenUser_ThenGenerateTokenSuccessfully()
    {
        // Arrange
        var roles = RoleFixture.GenerateRoles(1);
        var userMock = UserFixture.GenerateUserMock();

        userMock.Setup(s => s.Roles).Returns([.. roles]);

        // Act
        var token = fixture.AuthenticateService!.GenerateToken(userMock.Object);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }
}
