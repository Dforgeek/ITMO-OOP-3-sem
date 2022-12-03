using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DebitAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private decimal _sumOfPercentsPerAnnum;
    private List<INotificationStrategy> _notificationStrategies;

    public DebitAccount(Bank bank, Client client, IMoney money, Guid id, DebitAccountTerms? debitAccountTerms = null)
    {
        Balance = new PosOnlyMoney(money.Value);
        Bank = bank;
        Client = client;
        Id = id;
        DebitAccountTerms = debitAccountTerms;
        _sumOfPercentsPerAnnum = 0;
        _notificationStrategies = new List<INotificationStrategy>();
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public PosOnlyMoney Balance { get; private set; }

    public DebitAccountTerms? DebitAccountTerms { get; private set; }

    public TransferTransaction Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        Withdraw(transferValue, dateTime);
        toAcc.Replenish(transferValue, dateTime);

        return new TransferTransaction(this, toAcc, transferValue, DebitAccountTerms, dateTime, Guid.NewGuid());
    }

    public ReplenishTransaction Replenish(PosOnlyMoney money, DateTime dateTime)
    {
        Balance = new PosOnlyMoney(Balance.Value + money.Value);
        return new ReplenishTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public WithdrawTransaction Withdraw(PosOnlyMoney money, DateTime dateTime)
    {
        if (!TransferValidation(money))
            throw new Exception();

        Balance = new PosOnlyMoney(Balance.Value - money.Value);
        return new WithdrawTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public void UndoTransactionConsequences(TransferTransaction transferTransaction, DateTime dateTime)
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        TimeSpan timeSpan = dateTime - transferTransaction.DateTime;
        int days = timeSpan.Days;

        if (Equals(transferTransaction.FromAccount))
        {
            _sumOfPercentsPerAnnum += days * transferTransaction.TransferValue.Value *
                DebitAccountTerms.PercentPerAnnum.GetInCoefficientForm / DaysInYear;
            return;
        }

        if (Equals(transferTransaction.ToAccount))
        {
            _sumOfPercentsPerAnnum -= days * transferTransaction.TransferValue.Value *
                DebitAccountTerms.PercentPerAnnum.GetInCoefficientForm / DaysInYear;
            return;
        }

        throw new Exception();
    }

    public void UpdateAccruals()
    {
        if (DebitAccountTerms == null)
            throw new Exception();

        _sumOfPercentsPerAnnum +=
            Balance.Value * (DebitAccountTerms.PercentPerAnnum.GetInCoefficientForm / DaysInYear);
    }

    public void WriteOffAccruals(DateTime dateTime)
    {
        Replenish(new PosOnlyMoney(_sumOfPercentsPerAnnum), dateTime);
        _sumOfPercentsPerAnnum = 0;
    }

    public void AddNewNotificationStrategy(INotificationStrategy notificationStrategy)
    {
        if (_notificationStrategies.Contains(notificationStrategy))
            throw new Exception();

        _notificationStrategies.Add(notificationStrategy);
    }

    public void UpdateTerms(DebitAccountTerms debitAccountTerms)
    {
        if (debitAccountTerms.Equals(DebitAccountTerms))
            throw new Exception();

        DebitAccountTerms = debitAccountTerms;
        string message = GetMessageForNotify(debitAccountTerms);
        foreach (INotificationStrategy notificationStrategy in _notificationStrategies)
        {
            notificationStrategy.Notify(Client, message);
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

    private string GetMessageForNotify(DebitAccountTerms debitAccountTerms)
    {
        return "We have updated our debit accounts terms:" +
               $" \n New percent per annum: {debitAccountTerms.PercentPerAnnum}" +
               $"\n New limit for unreliable clients: {debitAccountTerms.UnreliableClientLimit}";
    }
}