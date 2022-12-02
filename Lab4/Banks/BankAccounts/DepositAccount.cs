using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DepositAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private decimal _sumOfPercentsPerAnnum;

    public DepositAccount(Bank bank, Client client, IMoney money, Guid id, DepositAccountTerms? depositAccTerms = null)
    {
        Bank = bank;
        Client = client;
        Balance = new PosOnlyMoney(money.Value);
        Id = id;
        DepositAccountTerms = depositAccTerms;
        _sumOfPercentsPerAnnum = 0;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public bool GetNotified { get; set; }

    public PosOnlyMoney Balance { get; private set; }

    public DepositAccountTerms? DepositAccountTerms { get; private set; }

    public TransactionLog Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (DepositAccountTerms == null)
            throw new Exception();

        RemoveMoney(transferValue);
        toAcc.AddMoney(transferValue);
        return new TransactionLog(this, toAcc, transferValue, DepositAccountTerms, dateTime);
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
        if (DepositAccountTerms == null)
            throw new Exception();

        Percent percent = DepositAccountTerms.GetPercentForConcreteBalance(Balance);
        _sumOfPercentsPerAnnum +=
            Balance.Value * (percent.GetInCoefficientForm / DaysInYear);
    }

    public void WriteOffAccruals()
    {
        AddMoney(new PosOnlyMoney(_sumOfPercentsPerAnnum));
        _sumOfPercentsPerAnnum = 0;
    }

    public void UpdateTerms(DepositAccountTerms depositAccountTerms)
    {
        if (depositAccountTerms.Equals(DepositAccountTerms))
            throw new Exception();

        DepositAccountTerms = depositAccountTerms;
        if (GetNotified)
        {
            Client.GetNotification($"We have updated our deposit accounts terms:" +
                                   $"\n New limit for unreliable clients: {DepositAccountTerms.UnreliableClientLimit} " +
                                   $"\n New rates for percent per annum:\n {GetChangeRatesInString(depositAccountTerms)}");
        }
    }

    public void AcceptVisitor(IAccountTermsVisitor termsVisitor)
    {
        termsVisitor.CreateAccountTerms(this);
    }

    private string GetChangeRatesInString(DepositAccountTerms depositAccountTerms)
    {
        IReadOnlyCollection<DepositChangeRate> changeRates = depositAccountTerms.DepositAccountTermsList;

        return changeRates
            .Aggregate(string.Empty, (current, depositChangeRate) => current + (depositChangeRate + "\n"));
    }

    private bool TransferValidation(PosOnlyMoney transferValue)
    {
        if (DepositAccountTerms == null)
            throw new Exception();

        return Client.Verified || transferValue.Value < DepositAccountTerms.UnreliableClientLimit.Value;
    }
}