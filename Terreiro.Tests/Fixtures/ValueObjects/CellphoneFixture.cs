using Bogus;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.Fixtures.ValueObjects;

public class CellphoneFixture
{
    public static IEnumerable<Cellphone> GenerateCellphones(int quantity) =>
        new Faker<Cellphone>()
            .CustomInstantiator(f => new(
                f.Random.String(3),
                f.Random.String(8, 9))
            ).Generate(quantity);
}
