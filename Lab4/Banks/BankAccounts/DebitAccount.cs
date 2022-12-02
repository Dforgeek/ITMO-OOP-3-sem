using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private decimal _sumOfPercentsPerAnnum;

    public DebitAccount(Bank bank, Client client, IMoney money, Guid id, DebitAccountTerms? debitAccountTerms = null)
    {
        Balance = new PosOnlyMoney(money.Value);
        Bank = bank;
        Client = client;
        Id = id;
        _sumOfPercentsPerAnnum = 0;
        DebitAccountTerms = debitAccountTerms;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public bool GetNotified { get; set; }

    public PosOnlyMoney Balance { get; private set; }

    public DebitAccountTerms? DebitAccountTerms { get; private set; }

    public TransactionLog Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        RemoveMoney(transferValue);
        toAcc.RemoveMoney(transferValue);
        var log = new TransactionLog(this, toAcc, transferValue, DebitAccountTerms, dateTime);
        return log;
    }

    public void AddMoney(PosOnlyMoney money)
    {
        Balance = new PosOnlyMoney(Balance.Value + money.Value);
    }

    public void RemoveMoney(PosOnlyMoney money)
    {
        if (!TransferValidation(money))
            throw new Exception();

        Balance = new PosOnlyMoney(Balance.Value - money.Value);
    }

    public void UndoTransactionConsequences(TransactionLog transactionLog)
    {
    }

    public void UpdateAccruals()
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        _sumOfPercentsPerAnnum +=
            Balance.Value * (DebitAccountTerms.PercentPerAnnum.GetInCoefficientForm / DaysInYear);
    }

    public void WriteOffAccruals()
    {
        AddMoney(new PosOnlyMoney(_sumOfPercentsPerAnnum));
        _sumOfPercentsPerAnnum = 0;
    }

    public void UpdateTerms(DebitAccountTerms debitAccountTerms)
    {
        if (debitAccountTerms.Equals(DebitAccountTerms))
            throw new Exception();

        DebitAccountTerms = debitAccountTerms;
        if (GetNotified)
        {
            Client.GetNotification($"We have updated our debit accounts terms:" +
                                   $" \n New percent per annum: {debitAccountTerms.PercentPerAnnum}" +
                                   $"\n New limit for unreliable clients: {debitAccountTerms.UnreliableClientLimit}");
        }
    }

    public void AcceptVisitor(IAccountTermsVisitor termsVisitor)
    {
        termsVisitor.CreateAccountTerms(this);
    }

    private bool TransferValidation(PosOnlyMoney transferValue)
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        return Client.Verified || transferValue.Value < DebitAccountTerms.UnreliableClientLimit.Value;
    }
}