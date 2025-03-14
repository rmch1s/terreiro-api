using Bogus;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Fixtures.Entities;

public class RoleFixture
{
    public static IEnumerable<Role> GenerateRoles(int quantity) =>
        new Faker<Role>()
            .CustomInstantiator(f => new(
                f.Person.FirstName,
                f.Random.String().OrNull(f))
            ).Generate(quantity);
}
