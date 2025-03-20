using Microsoft.Extensions.Configuration;
using Moq;
using Terreiro.Application.Services.Authenticate;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(AuthenticateServiceCollection))]
public class AuthenticateServiceCollection : ICollectionFixture<AuthenticateServiceFixture>;

public class AuthenticateServiceFixture : ServiceFixtureBase
{
    public Mock<IConfiguration>? Configuration;
    internal AuthenticateService? AuthenticateService;

    public override void GenerateService()
    {
        Configuration = autoMocker.GetMock<IConfiguration>();
        var sectionExpirationHoursMock = new Mock<IConfigurationSection>();

        sectionExpirationHoursMock.Setup(x => x.Value).Returns(faker.Random.Int(1, 5).ToString());
        Configuration.Setup(s => s["Jwt:SecretKey"]).Returns(faker.Random.String());
        Configuration.Setup(s => s.GetSection("Jwt:ExpirationHours")).Returns(sectionExpirationHoursMock.Object);
        Configuration.Setup(s => s["Jwt:Issuer"]).Returns(faker.Random.String(10));
        Configuration.Setup(s => s["Jwt:Audience"]).Returns(faker.Random.String(10));

        AuthenticateService = autoMocker.CreateInstance<AuthenticateService>();
    }
}
