using FluentAssertions;
using Terreiro.Tests.Fixtures.Entities;
using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

[Collection(nameof(AuthenticateServiceCollection))]
[Trait("Category", "AuthenticateService")]
public class AuthenticateServiceTest
{
    private readonly AuthenticateServiceFixture _fixture;

    public AuthenticateServiceTest(AuthenticateServiceFixture fixture)
    {
        _fixture = fixture;
        _fixture.GenerateService();
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
        var token = _fixture.AuthenticateService!.GenerateToken(userMock.Object);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }
}
