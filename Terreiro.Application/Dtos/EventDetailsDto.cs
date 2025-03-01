using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Dtos;

public record EventDetailsDto(
    int Id,
    string Name,
    string? Description,
    Period Period,
    UserDto[] Users,
    EventItemDto[] Items
);