using Bogus;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.Fixtures.ValueObjects;

internal class CellphoneFixture
{
    public static IEnumerable<Cellphone> GenerateCellphones(int quantity) =>
        new Faker<Cellphone>()
            .CustomInstantiator(f => new(
                f.Random.Int(11, 99).ToString(),
                f.Random.Int(111111111, 999999999).ToString())
            ).Generate(quantity);
}
