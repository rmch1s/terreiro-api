using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Dtos;

public record EventDto
(
    int Id,
    string Name,
    string? Description,
    Period Period
);
