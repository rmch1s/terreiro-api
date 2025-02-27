using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public class UpsertUserRequest(string name, string cpf, Cellphone cellphone)
{
    public string Name { get; } = name;
    public string CPF { get; } = cpf;
    public Cellphone Cellphone { get; } = cellphone;
}
