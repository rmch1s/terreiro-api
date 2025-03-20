using Terreiro.Tests.Fixtures.Services;

namespace Terreiro.Tests.Services;

public class ServiceTestBase<T> where T : ServiceFixtureBase
{
    protected readonly T fixture;

    public ServiceTestBase(T fixture)
    {
        this.fixture = fixture;
        fixture.GenerateService();
    }
}
