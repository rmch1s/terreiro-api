using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.SetPin;

public interface ISetPinService
{
    void SetPin(User user, string? oldPin, string newPin);
}
