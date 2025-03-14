using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public record CreateEventRequest(
    string Name,
    Period Period,
    string? Description
)
{
    public UpsertEventItemRequest[] Items { get; set; } = [];
}