using System.Net.Http.Headers;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    private decimal _sumOfPercentsPerAnnum;

    public DebitAccount(Bank bank, Client client, PosOnlyMoney money, DebitAccountTerms debitAccountTerms, Guid id)
    {
        Balance = money;
        Bank = bank;
        Client = client;
        Id = id;
        _sumOfPercentsPerAnnum = 0;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public IMoney Balance { get; private set; }

    public void Transfer(PosOnlyMoney money, IBankAccount anotherBankAccount)
    {
        RemoveMoney(money);
        anotherBankAccount.AddMoney(money);
    }

    public void AddMoney(PosOnlyMoney money)
    {
        Balance = new PosOnlyMoney(Balance.Value + money.Value);
    }

    public void RemoveMoney(PosOnlyMoney money)
    {
        Balance = new PosOnlyMoney(Balance.Value - money.Value);
    }

    public void AddPercentsPerAnnum()
    {
        Balance = new PosOnlyMoney(Balance.Value + _sumOfPercentsPerAnnum);
        _sumOfPercentsPerAnnum = 0;
    }
}