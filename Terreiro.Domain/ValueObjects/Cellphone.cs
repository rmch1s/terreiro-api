namespace Terreiro.Domain.ValueObjects;

public class Cellphone
{
    private Cellphone() { }

    public Cellphone(string ddd, string number)
    {
        DDD = ddd;
        Number = number;
    }

    public string DDD { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
}
