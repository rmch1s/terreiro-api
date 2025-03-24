using Bogus;
using Moq;
using Terreiro.Domain.Entities;
using Terreiro.Tests.Fixtures.ValueObjects;

namespace Terreiro.Tests.Fixtures.Entities;

public class UserFixture
{
    public static IEnumerable<User> GenerateUsers(int quantity) =>
        new Faker<User>()
            .CustomInstantiator(f => new(
                f.Random.String(5, 100),
                f.Random.Long(11111111111, 99999999999).ToString(),
                CellphoneFixture.GenerateCellphones(1).First())
            ).Generate(quantity);

    public static Mock<User> GenerateUserMock()
    {
        var user = GenerateUsers(1).First();
        return new Mock<User>(user.Name, user.CPF, user.Cellphone) { CallBase = true };
    }
}
