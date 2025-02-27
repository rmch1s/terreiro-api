namespace Terreiro.Application.Requests;

public class PatchPinRequest(string? oldPin, string newPin)
{
    public string? OldPin { get; } = oldPin;
    public string NewPin { get; } = newPin;
}
