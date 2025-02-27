using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Dtos;

public record UserDetailsDto
(
    int Id,
    string Name,
    string CPF,
    Cellphone Cellphone,
    RoleDto[] Roles
);
