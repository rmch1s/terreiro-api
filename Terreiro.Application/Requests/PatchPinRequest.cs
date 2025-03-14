namespace Terreiro.Application.Requests;

public record PatchPinRequest(string? OldPin, string NewPin);
