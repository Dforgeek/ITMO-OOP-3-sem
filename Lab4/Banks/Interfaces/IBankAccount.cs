using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccount
{
    Guid Id { get; }

    IBank Bank { get; }
    Client Client { get; }

    decimal Money { get; }

    void AddMoney(decimal money);

    void RemoveMoney(decimal money);
}