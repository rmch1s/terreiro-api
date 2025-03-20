using Moq;
using Moq.AutoMock;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserRole;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserRoleServiceCollection))]
public class UpsertUserRoleServiceCollection : ICollectionFixture<UpsertUserRoleServiceFixture>;

public class UpsertUserRoleServiceFixture
{
    public Mock<IUserRoleRepository>? UserRoleRepository;
    internal UpsertUserRoleService? UpsertUserRoleService;

    public void GenerateService()
    {
        var autoMocker = new AutoMocker();

        UserRoleRepository = autoMocker.GetMock<IUserRoleRepository>();

        UpsertUserRoleService = autoMocker.CreateInstance<UpsertUserRoleService>();
    }
}
