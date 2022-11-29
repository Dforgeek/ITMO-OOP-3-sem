using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    public DepositAccount(IBank bank, Client client, decimal money, Guid id)
    {
        if (money < 0)
            throw new Exception();
        Bank = bank;
        Client = client;
        Money = money;
        Id = id;
    }

    public Guid Id { get; }
    public IBank Bank { get; }
    public Client Client { get; }
    public decimal Money { get; private set; }

    public void AddMoney(decimal money)
    {
        if (money < 0)
        Money += money;
    }

    public void RemoveMoney(decimal money)
    {
        throw new NotImplementedException();
    }
}