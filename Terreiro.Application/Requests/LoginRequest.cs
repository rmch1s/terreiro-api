namespace Terreiro.Application.Requests;

public class LoginRequest(string cpf)
{
    public string CPF { get; } = cpf;
}
