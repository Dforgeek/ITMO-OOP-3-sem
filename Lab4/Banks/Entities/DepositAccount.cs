using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    public DepositAccount(Bank bank, Client client, PosOnlyMoney money, Guid id)
    {
        Bank = bank;
        Client = client;
        Balance = money;
        Id = id;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public IMoney Balance { get; }

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

    public void AddPercentsPerAnnum()
    {
        throw new NotImplementedException();
    }
}