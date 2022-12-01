using System.Net.Http.Headers;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private decimal _sumOfPercentsPerAnnum;

    public DebitAccount(Bank bank, Client client, PosOnlyMoney money, DebitAccountTerms debitAccountTerms, Guid id)
    {
        Balance = money;
        Bank = bank;
        Client = client;
        Id = id;
        _sumOfPercentsPerAnnum = 0;
        DebitAccountTerms = debitAccountTerms;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public IMoney Balance { get; private set; }

    public DebitAccountTerms DebitAccountTerms { get; private set; }
    
    public void Transfer(I)

    public void AddMoney(PosOnlyMoney money)
    {
        Balance = new PosOnlyMoney(Balance.Value + money.Value);
    }

    public void RemoveMoney(PosOnlyMoney money)
    {
        Balance = new PosOnlyMoney(Balance.Value - money.Value);
    }

    public void AddSumOfPercentsPerAnnumToBalance()
    {
        Balance = new PosOnlyMoney(Balance.Value + _sumOfPercentsPerAnnum);
        _sumOfPercentsPerAnnum = 0;
    }

    public void AddPercentsToSum()
    {
        _sumOfPercentsPerAnnum +=
            Balance.Value * (DebitAccountTerms.PercentPerAnnum.GetInCoefficientForm / DaysInYear);
    }

    public void Update(DebitAccountTerms debitAccountTerms)
    {
        Client.GetNotification();
        DebitAccountTerms = debitAccountTerms;
    }

    public void AcceptVisitor(IAccountTermsVisitor termsVisitor)
    {
        termsVisitor.CreateAccountTerms(this);
    }
}