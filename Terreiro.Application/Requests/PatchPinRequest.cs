namespace Terreiro.Application.Requests;

public class PatchPinRequest(string? oldPin, string newPin)
{
    public string? OldPin { get; set; } = oldPin;
    public string NewPin { get; set; } = newPin;
}
