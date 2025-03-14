using Bogus;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.ValueObjects;

namespace Terreiro.Tests.Fixtures.Entities;

public class UserFixture
{
    public static IEnumerable<User> GenerateUsers(int quantity) =>
        new Faker<User>()
            .CustomInstantiator(f => new(
                f.Random.String(5, 100),
                f.Random.String(11),
                CellphoneFixture.GenerateCellphones(1).First())
            ).Generate(quantity);
}
