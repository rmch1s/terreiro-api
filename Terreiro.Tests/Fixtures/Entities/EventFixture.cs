using Bogus;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.ValueObjects;

namespace Terreiro.Tests.Fixtures.Entities;

public class EventFixture
{
    public static IEnumerable<Event> GenerateEvents(int quantity) =>
        new Faker<Event>()
            .CustomInstantiator(f => new(
                f.Random.String(5, 100),
                PeriodFixture.GeneratePeriods(1).First(),
                EventItemFixture.GenerateEventItems(1).ToArray(),
                f.Random.String(5, 300).OrNull(f)
            )).Generate(quantity);
}
