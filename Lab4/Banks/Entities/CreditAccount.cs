using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    public CreditAccount(Bank bank, Client client, IMoney posNegMoney, Guid id)
    {
        if (posNegMoney.Value < 0)
            throw new Exception("Creating credit with already negative value?...");
        Bank = bank;
        Client = client;
        Balance = posNegMoney;
        Id = id;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public IMoney Balance { get; private set; }

    public void Transfer(PosOnlyMoney money, IBankAccount anotherBankAccount)
    {
        throw new NotImplementedException();
    }

    public void AddMoney(PosOnlyMoney money)
    {
        throw new NotImplementedException();
    }

    public void RemoveMoney(PosOnlyMoney money)
    {
        throw new NotImplementedException();
    }

    public void AddPercentsPerAnnum() { }
}