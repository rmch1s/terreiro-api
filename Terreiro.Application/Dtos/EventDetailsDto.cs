using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Dtos;

public record EventDetailsDto(
    string Name,
    string? Description,
    Period Period,
    UserDto[] Users
);