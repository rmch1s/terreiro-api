using Moq;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserEventItem;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserEventItemServiceCollection))]
public class UpsertUserEventItemServiceCollection : ICollectionFixture<UpsertUserEventItemServiceFixture>;

public class UpsertUserEventItemServiceFixture : ServiceFixtureBase
{
    public Mock<IUserEventItemRepository>? UserEventItemRepository;
    internal UpsertUserEventItemService? UpsertUserEventItemService;

    public override void GenerateService()
    {
        UserEventItemRepository = autoMocker.GetMock<IUserEventItemRepository>();

        UpsertUserEventItemService = autoMocker.CreateInstance<UpsertUserEventItemService>();
    }
}
