using System.Runtime.CompilerServices;
using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class CreditAccount : IBankAccount
{
    private decimal _sumOfCommission;
    private List<INotificationStrategy> _notificationStrategies;

    public CreditAccount(Bank bank, Client client, IMoney money, Guid id, CreditAccountTerms? creditAccTerms = null)
    {
        Bank = bank;
        Client = client;
        Balance = new PosNegMoney(money.Value);
        Id = id;
        CreditAccountTerms = creditAccTerms;
        _sumOfCommission = 0;
        _notificationStrategies = new List<INotificationStrategy>();
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public PosNegMoney Balance { get; private set; }

    public CreditAccountTerms? CreditAccountTerms { get; private set; }

    public TransferTransaction Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        Withdraw(transferValue, dateTime);
        toAcc.Replenish(transferValue, dateTime);

        var log = new TransferTransaction(this, toAcc, transferValue, CreditAccountTerms, dateTime, Guid.NewGuid());
        return log;
    }

    public ReplenishTransaction Replenish(PosOnlyMoney money, DateTime dateTime)
    {
        Balance = new PosNegMoney(Balance.Value + money.Value);
        return new ReplenishTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public WithdrawTransaction Withdraw(PosOnlyMoney money, DateTime dateTime)
    {
        ValidateAndDecrease(money);

        return new WithdrawTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public void UndoTransactionConsequences(TransferTransaction transferTransaction, DateTime dateTime)
    {
        if (transferTransaction.Terms is not CreditAccountTerms creditAccountTerms)
            throw new Exception();

        if (Equals(transferTransaction.FromAccount))
        {
            _sumOfCommission -= creditAccountTerms.Commission.Value;
            return;
        }

        if (Equals(transferTransaction.ToAccount))
        {
            _sumOfCommission += creditAccountTerms.Commission.Value;
            return;
        }

        throw new Exception();
    }

    public void UpdateAccruals()
    {
    }

    public void WriteOffAccruals(DateTime dateTime)
    {
        Withdraw(new PosOnlyMoney(_sumOfCommission), dateTime);
        _sumOfCommission = 0;
    }

    public void AddNewNotificationStrategy(INotificationStrategy notificationStrategy)
    {
        if (_notificationStrategies.Contains(notificationStrategy))
            throw new Exception();

        _notificationStrategies.Add(notificationStrategy);
    }

    public void UpdateTerms(CreditAccountTerms creditAccountTerms)
    {
        if (creditAccountTerms.Equals(CreditAccountTerms))
            return;

        CreditAccountTerms = creditAccountTerms;

        string message = GetMessageForNotify(creditAccountTerms);

        foreach (INotificationStrategy notificationStrategy in _notificationStrategies)
        {
            notificationStrategy.Notify(Client, message);
        }
    }

    public void AcceptVisitor(IAccountTermsVisitor termsVisitor)
    {
        termsVisitor.CreateAccountTerms(this);
    }

    private void ValidateAndDecrease(PosOnlyMoney money)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        if (!UnreliableLimitValidation(money))
            throw new Exception();

        decimal newBalance = Balance.Value - money.Value;

        if (CreditAccountTerms.CreditLimit.Value > newBalance)
            throw new Exception();

        if (newBalance < 0)
            _sumOfCommission += CreditAccountTerms.Commission.Value;

        Balance = new PosNegMoney(Balance.Value - money.Value);
    }

    private bool UnreliableLimitValidation(IMoney transferValue)
    {
        if (CreditAccountTerms == null)
            throw new Exception();

        return Client.Verified || transferValue.Value < CreditAccountTerms.UnreliableClientLimit.Value;
    }

    private string GetMessageForNotify(CreditAccountTerms creditAccountTerms)
    {
        return "We have updated our credit accounts terms:" +
               $" \n New commission for transactions with negative balance: {creditAccountTerms.Commission} " +
               $"\n New credit limit:{creditAccountTerms.CreditLimit}" +
               $"\n New limit for unreliable clients: {creditAccountTerms.UnreliableClientLimit}";
    }
}