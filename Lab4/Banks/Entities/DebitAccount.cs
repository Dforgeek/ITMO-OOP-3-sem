using System.Net.Http.Headers;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    public DebitAccount(IBank bank, Client client, decimal money, Guid id)
    {
        if (money < 0)
            throw new Exception();
        Money = money;
        Bank = bank;
        Client = client;
        Id = id;
    }

    public Guid Id { get; }
    public IBank Bank { get; }
    public Client Client { get; }
    public decimal Money { get; private set; }

    public void AddMoney(decimal money)
    {
        if (money < 0)
            throw new Exception();
        Money += money;
    }

    public void RemoveMoney(decimal money)
    {
        if (money < 0)
            throw new Exception();
        if (Money - money < 0)
            throw new Exception();
        Money -= money;
    }
}