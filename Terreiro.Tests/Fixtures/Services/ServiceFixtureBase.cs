using Moq.AutoMock;

namespace Terreiro.Tests.Fixtures.Services;

public abstract class ServiceFixtureBase : TestBase
{
    protected readonly AutoMocker autoMocker = new AutoMocker();

    public abstract void GenerateService();
}
