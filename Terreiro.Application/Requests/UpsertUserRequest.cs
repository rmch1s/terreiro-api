using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public class UpsertUserRequest(string name, string cpf, Cellphone cellphone)
{
    public string Name { get; set; } = name;
    public string CPF { get; set; } = cpf;
    public Cellphone Cellphone { get; set; } = cellphone;
}
