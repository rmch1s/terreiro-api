using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Dtos;

public record UserDto
(
    int Id,
    string Name,
    string CPF,
    Cellphone Cellphone
);
