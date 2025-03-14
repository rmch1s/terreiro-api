using Bogus;
using Terreiro.Domain.Entities;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Tests.Fixtures;

public class EventFixture
{
    public static IEnumerable<Event> GenerateEvents(int quantity) =>
        new Faker<Event>()
            .CustomInstantiator(f => new(
                f.Person.FirstName,
                new Period(f.Date.Future(), f.Date.Future().OrNull(f)),
                EventItemFixture.GenerateEventItems(1).ToArray(),
                f.Random.String().OrNull(f)
            )).Generate(quantity);
}
