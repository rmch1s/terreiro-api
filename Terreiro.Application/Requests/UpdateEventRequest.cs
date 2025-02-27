using Terreiro.Domain.ValueObjects;

namespace Terreiro.Application.Requests;

public class UpdateEventRequest(string name, Period period, string? description)
{
    public string Name { get; } = name;
    public Period Period { get; } = period;
    public string? Description { get; } = description;
}
