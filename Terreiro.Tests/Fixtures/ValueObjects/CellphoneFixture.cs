using Bogus;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.Fixtures.ValueObjects;

public class CellphoneFixture
{
    public static IEnumerable<Cellphone> GenerateCellphones(int quantity) =>
        new Faker<Cellphone>()
            .CustomInstantiator(f => new(
                f.Random.UInt(2).ToString(),
                f.Random.UInt(8, 9).ToString())
            ).Generate(quantity);
}
