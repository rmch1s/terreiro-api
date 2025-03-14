using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public record UpdateEventRequest(
    string Name,
    Period Period,
    string? Description
);