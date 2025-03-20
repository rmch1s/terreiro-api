using Microsoft.Extensions.Configuration;
using Moq;
using Moq.AutoMock;
using Terreiro.Application.Services.Authenticate;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(AuthenticateServiceCollection))]
public class AuthenticateServiceCollection : ICollectionFixture<AuthenticateServiceFixture>;

public class AuthenticateServiceFixture : TestBase
{
    public Mock<IConfiguration>? Configuration;
    internal AuthenticateService? AuthenticateService;

    public void GenerateService()
    {
        var autoMocker = new AutoMocker();

        Configuration = autoMocker.GetMock<IConfiguration>();
        var sectionExpirationHoursMock = new Mock<IConfigurationSection>();
        sectionExpirationHoursMock.Setup(x => x.Value).Returns(faker.Random.Int(1, 5).ToString());
        Configuration.Setup(x => x["Jwt:SecretKey"]).Returns(faker.Random.String());
        Configuration.Setup(x => x.GetSection("Jwt:ExpirationHours")).Returns(sectionExpirationHoursMock.Object);
        Configuration.Setup(x => x["Jwt:Issuer"]).Returns(faker.Random.String(10));
        Configuration.Setup(x => x["Jwt:Audience"]).Returns(faker.Random.String(10));

        AuthenticateService = autoMocker.CreateInstance<AuthenticateService>();
    }
}
