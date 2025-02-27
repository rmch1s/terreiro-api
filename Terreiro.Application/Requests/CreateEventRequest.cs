using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public class CreateEventRequest(string name, Period period, UpsertEventItemRequest[] items, string? description)
{
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public Period Period { get; } = period;
    public UpsertEventItemRequest[] Items { get; } = items;
}
