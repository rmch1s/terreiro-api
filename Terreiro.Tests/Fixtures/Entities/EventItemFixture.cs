using Bogus;
using Terreiro.Domain.Entities;

namespace Terreiro.Tests.Fixtures.Entities;

public class EventItemFixture
{
    public static IEnumerable<EventItem> GenerateEventItems(int quantity) =>
        new Faker<EventItem>()
            .CustomInstantiator(f => new(
                f.Person.FirstName,
                f.Random.Int(),
                f.Random.Int())
            ).Generate(quantity);
}
