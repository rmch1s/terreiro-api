using Terreiro.Domain.Entities.Base;
using Terreiro.Domain.Execptions;
using Terreiro.Domain.ValueObjects;

namespace Terreiro.Domain.Entities;

public class User : BaseEntity
{
    private User() { }

    public User(string name, string cpf, Cellphone cellphone)
    {
        Name = name;
        CPF = cpf;
        Cellphone = cellphone;
    }

    public string Name { get; private set; } = string.Empty;
    public string CPF { get; private set; } = string.Empty;
    public string? PIN { get; private set; }
    public Cellphone Cellphone { get; private set; } = default!;

    public virtual ICollection<Role> Roles { get; private set; } = [];
    public virtual ICollection<EventItem> EventItems { get; private set; } = [];
    public virtual ICollection<Event> Events { get; private set; } = [];

    public void SetPin(string? oldPin, string newPin)
    {
        if (PIN != oldPin)
            throw new WrongPinException();

        PIN = newPin;
    }

    public void Update(string name, string cpf, Cellphone cellphone)
    {
        Name = name;
        CPF = cpf;
        Cellphone = cellphone;
        UpdatedAt = DateTime.UtcNow;
    }
}
