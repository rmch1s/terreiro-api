using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public record UpsertUserRequest(
    string Name,
    string CPF,
    Cellphone Cellphone
);
