using Bogus;
using Moq.AutoMock;

namespace Terreiro.Tests.Fixtures.Services;

public abstract class ServiceFixtureBase
{
    protected readonly AutoMocker autoMocker = new AutoMocker();
    protected readonly Faker faker = new("pt_BR");

    public abstract void GenerateService();
}
