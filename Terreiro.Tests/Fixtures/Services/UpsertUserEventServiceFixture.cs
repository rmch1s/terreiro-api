using Moq;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserEvent;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserEventServiceCollection))]
public class UpsertUserEventServiceCollection : ICollectionFixture<UpsertUserEventServiceFixture>;

public class UpsertUserEventServiceFixture : ServiceFixtureBase
{
    public Mock<IUserEventRepository>? UserEventRepository;
    internal UpsertUserEventService? UpsertUserEventService;

    public override void GenerateService()
    {
        UserEventRepository = autoMocker.GetMock<IUserEventRepository>();

        UpsertUserEventService = autoMocker.CreateInstance<UpsertUserEventService>();
    }
}
