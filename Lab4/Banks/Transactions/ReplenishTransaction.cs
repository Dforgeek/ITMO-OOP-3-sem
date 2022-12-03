using Banks.BankAccounts;
using Banks.ValueObjects;

namespace Banks.Transactions;

public class ReplenishTransaction : ITransaction
{
    public ReplenishTransaction(IBankAccount bankAccount, PosOnlyMoney transferValue, DateTime dateTime, Guid id)
    {
        BankAccount = bankAccount;
        TransferValue = transferValue;
        DateTime = dateTime;
        Id = id;
        Cancelled = false;
    }

    public IBankAccount BankAccount { get; }

    public Guid Id { get; }

    public PosOnlyMoney TransferValue { get; }

    public DateTime DateTime { get; }

    public bool Cancelled { get; private set; }

    public void CancelTransaction()
    {
        Cancelled = true;
    }

    public ITransaction Accept(ITransactionVisitor visitor)
    {
        return visitor.Visit(this);
    }
}