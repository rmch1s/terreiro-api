using Bogus;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Fixtures.Entities;

public class RoleFixture
{
    public static IEnumerable<Role> GenerateRoles(int quantity) =>
        new Faker<Role>()
            .CustomInstantiator(f => new(
                f.Random.String(5, 100),
                f.Random.String(5, 300).OrNull(f))
            ).Generate(quantity);
}
