using Banks.BankAccounts;
using Banks.BankAccountTerms;
using Banks.ValueObjects;

namespace Banks.Transactions;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(IBankAccount fromAcc, IBankAccount toAcc, PosOnlyMoney transferValue, IBankAccountTerms terms, DateTime dateTime, Guid id)
    {
        TransferValue = transferValue;
        ToAccount = toAcc;
        FromAccount = fromAcc;
        DateTime = dateTime;
        Terms = terms;
        Id = id;
        Cancelled = false;
    }

    public IBankAccount ToAccount { get; }

    public IBankAccount FromAccount { get; }

    public Guid Id { get; }

    public PosOnlyMoney TransferValue { get; }

    public DateTime DateTime { get; }

    public IBankAccountTerms Terms { get; }

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