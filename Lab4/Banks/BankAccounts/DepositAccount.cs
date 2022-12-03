using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DepositAccount : IBankAccount
{
    private const int DaysInYear = 365;
    private decimal _sumOfPercentsPerAnnum;
    private List<INotificationStrategy> _notificationStrategies;

    public DepositAccount(Bank bank, Client client, IMoney money, Guid id, DateTime dateTime, DepositAccountTerms? depositAccTerms = null)
    {
        Bank = bank;
        Client = client;
        Balance = new PosOnlyMoney(money.Value);
        Id = id;
        DepositAccountTerms = depositAccTerms;
        TimeOfCreation = dateTime;

        _sumOfPercentsPerAnnum = 0;
        _notificationStrategies = new List<INotificationStrategy>();
    }

    public Guid Id { get; }

    public Bank Bank { get; }

    public Client Client { get; }

    public DateTime TimeOfCreation { get; }

    public PosOnlyMoney Balance { get; private set; }

    public DepositAccountTerms? DepositAccountTerms { get; private set; }

    public TransferTransaction Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime)
    {
        if (DepositAccountTerms == null)
            throw new Exception();

        Withdraw(transferValue, dateTime);
        toAcc.Replenish(transferValue, dateTime);

        return new TransferTransaction(this, toAcc, transferValue, DepositAccountTerms, dateTime, Guid.NewGuid());
    }

    public ReplenishTransaction Replenish(PosOnlyMoney money, DateTime dateTime)
    {
        Balance = new PosOnlyMoney(Balance.Value + money.Value);
        return new ReplenishTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public WithdrawTransaction Withdraw(PosOnlyMoney money, DateTime dateTime)
    {
        if (DepositAccountTerms == null)
            throw new Exception();

        if (!TransferValidation(money))
            throw new Exception();

        if (dateTime - TimeOfCreation < DepositAccountTerms.WithdrawUnavailableTimeSpan)
            throw new Exception();

        Balance = new PosOnlyMoney(Balance.Value - money.Value);
        return new WithdrawTransaction(this, money, dateTime, Guid.NewGuid());
    }

    public void UndoTransactionConsequences(TransferTransaction transferTransaction, DateTime dateTime)
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

    public void UpdateTerms(DepositAccountTerms depositAccountTerms)
    {
        if (depositAccountTerms.Equals(DepositAccountTerms))
            throw new Exception();

        DepositAccountTerms = depositAccountTerms;
        string message = GetMessageForNotify(depositAccountTerms);
        foreach (INotificationStrategy notificationStrategy in _notificationStrategies)
        {
            notificationStrategy.Notify(Client, message);
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

    private string GetMessageForNotify(DepositAccountTerms depositAccountTerms)
    {
        if (DepositAccountTerms == null)
            throw new Exception();

        return "We have updated our deposit accounts terms:" +
               $"\n New limit for unreliable clients: {DepositAccountTerms.UnreliableClientLimit} " +
               $"\n New rates for percent per annum:\n {GetChangeRatesInString(depositAccountTerms)}";
    }
}