using Terreiro.Application.Exceptions;
using Terreiro.Application.Resources;
using Terreiro.Domain.Entities;
using Terreiro.Domain.Execptions;

namespace Terreiro.Application.Services.SetPin;

internal class SetPinService : ISetPinService
{
    public void SetPin(User user, string? oldPin, string newPin)
    {
        try
        {
            user.SetPin(oldPin, newPin);
        }
        catch (WrongPinException)
        {
            throw new BadRequestException(TerreiroResource.WRONG_PIN);
        }
    }
}
