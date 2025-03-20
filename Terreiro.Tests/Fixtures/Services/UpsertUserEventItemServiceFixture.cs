using Moq;
using Moq.AutoMock;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserEventItem;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserEventItemServiceCollection))]
public class UpsertUserEventItemServiceCollection : ICollectionFixture<UpsertUserEventItemServiceFixture>;

public class UpsertUserEventItemServiceFixture
{
    public Mock<IUserEventItemRepository>? UserEventItemRepository;
    internal UpsertUserEventItemService? UpsertUserEventItemService;

    public void GenerateService()
    {
        var autoMocker = new AutoMocker();

        UserEventItemRepository = autoMocker.GetMock<IUserEventItemRepository>();

        UpsertUserEventItemService = autoMocker.CreateInstance<UpsertUserEventItemService>();
    }
}
