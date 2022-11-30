using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccount
{
    Guid Id { get; }

    Bank Bank { get; }

    Client Client { get; }

    IMoney Balance { get; }

    void Transfer(PosOnlyMoney money, IBankAccount anotherBankAccount);

    void AddMoney(PosOnlyMoney money);

    void RemoveMoney(PosOnlyMoney money);

    void AddPercentsPerAnnum();
}