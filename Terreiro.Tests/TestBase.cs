using Bogus;

namespace Terreiro.Tests;

public class TestBase
{
    protected readonly Faker faker = new("pt_BR");
}
