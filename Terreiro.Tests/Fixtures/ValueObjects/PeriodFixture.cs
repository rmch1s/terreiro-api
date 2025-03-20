using Bogus;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.Fixtures.ValueObjects;

internal class PeriodFixture
{
    public static IEnumerable<Period> GeneratePeriods(int quantity) =>
        new Faker<Period>()
            .CustomInstantiator(f => new(
                f.Date.Future(),
                f.Date.Future().OrNull(f))
            ).Generate(quantity);
}
