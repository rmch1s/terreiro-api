namespace Terreiro.Application.Requests;

public class UpsertEventItemRequest(string name, int quantity)
{
    public string Name { get; } = name;
    public int Quantity { get; } = quantity;
}