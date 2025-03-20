using Moq;
using Terreiro.Application.Repositories;
using Terreiro.Application.Services.UpsertUserRole;

namespace Terreiro.Tests.Fixtures.Services;

[CollectionDefinition(nameof(UpsertUserRoleServiceCollection))]
public class UpsertUserRoleServiceCollection : ICollectionFixture<UpsertUserRoleServiceFixture>;

public class UpsertUserRoleServiceFixture : ServiceFixtureBase
{
    public Mock<IUserRoleRepository>? UserRoleRepository;
    internal UpsertUserRoleService? UpsertUserRoleService;

    public override void GenerateService()
    {
        UserRoleRepository = autoMocker.GetMock<IUserRoleRepository>();

        UpsertUserRoleService = autoMocker.CreateInstance<UpsertUserRoleService>();
    }
}
