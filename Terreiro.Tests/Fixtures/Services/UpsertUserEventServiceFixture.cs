using Moq;
using Moq.AutoMock;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserEvent;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserEventServiceCollection))]
public class UpsertUserEventServiceCollection : ICollectionFixture<UpsertUserEventServiceFixture>;

public class UpsertUserEventServiceFixture
{
    public Mock<IUserEventRepository>? UserEventRepository;
    internal UpsertUserEventService? UpsertUserEventService;

    public void GenerateService()
    {
        var autoMocker = new AutoMocker();

        UserEventRepository = autoMocker.GetMock<IUserEventRepository>();

        UpsertUserEventService = autoMocker.CreateInstance<UpsertUserEventService>();
    }
}
