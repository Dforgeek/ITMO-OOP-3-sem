using System.Data.Common;
using Banks.BankAccounts;
using Banks.ValueObjects;

namespace Banks.Transactions;

public class WithdrawTransaction : ITransaction
{
    public WithdrawTransaction(IBankAccount bankAccount, PosOnlyMoney value, DateTime dateTime, Guid id)
    {
        BankAccount = bankAccount;
        TransferValue = value;
        DateTime = dateTime;
        Id = id;
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