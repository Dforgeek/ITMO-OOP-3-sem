using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class CreditAccount : IBankAccount
{
    private decimal _sumOfCommission;

    public CreditAccount(Bank bank, Client client, IMoney money, Guid id, CreditAccountTerms? creditAccTerms = null)
    {
        Bank = bank;
        Client = client;
        Balance = new PosNegMoney(money.Value);
        Id = id;
        CreditAccountTerms = creditAccTerms;
        _sumOfCommission = 0;
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public bool GetNotified { get; set; }

    public PosNegMoney Balance { get; private set; }

    public CreditAccountTerms? CreditAccountTerms { get; private set; }

    public TransactionLog Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        RemoveMoney(transferValue);
        toAcc.AddMoney(transferValue);
        var log = new TransactionLog(this, toAcc, transferValue, CreditAccountTerms, dateTime);
        return log;
    }

    public void AddMoney(PosOnlyMoney money)
    {
        Balance = new PosNegMoney(Balance.Value + money.Value);
    }

    public void RemoveMoney(PosOnlyMoney money)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        if (!TransferValidation(money))
            throw new Exception();

        decimal newBalance = Balance.Value - money.Value;

        if (CreditAccountTerms.CreditLimit.Value > newBalance)
            throw new Exception();

        if (newBalance < 0)
            _sumOfCommission += CreditAccountTerms.Commission.Value;

        Balance = new PosNegMoney(Balance.Value - money.Value);
    }

    public void UndoTransactionConsequences(TransactionLog transactionLog)
    {
        if (transactionLog.Terms is not CreditAccountTerms creditAccountTerms)
            throw new Exception();

        _sumOfCommission -= creditAccountTerms.Commission.Value;
    }

    public void UpdateAccruals()
    {
    }

    public void WriteOffAccruals()
    {
        Balance = new PosNegMoney(Balance.Value - _sumOfCommission);
        _sumOfCommission = 0;
    }

    public void UpdateTerms(CreditAccountTerms creditAccountTerms)
    {
        if (creditAccountTerms.Equals(CreditAccountTerms))
            return;

        CreditAccountTerms = creditAccountTerms;
        if (GetNotified)
        {
            Client.GetNotification($"We have updated our credit accounts terms:" +
                                   $" \n New commission for transactions with negative balance: {creditAccountTerms.Commission} " +
                                   $"\n New credit limit:{creditAccountTerms.CreditLimit}" +
                                   $"\n New limit for unreliable clients: {creditAccountTerms.UnreliableClientLimit}");
        }
    }

    public void AcceptVisitor(IAccountTermsVisitor termsVisitor)
    {
        termsVisitor.CreateAccountTerms(this);
    }

    private bool TransferValidation(IMoney transferValue)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        return Client.Verified || transferValue.Value < CreditAccountTerms.UnreliableClientLimit.Value;
    }
}