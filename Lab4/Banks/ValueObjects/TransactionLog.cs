using Banks.BankAccounts;
using Banks.BankAccountTerms;

namespace Banks.ValueObjects;

public class TransactionLog
{
    public TransactionLog(IBankAccount fromAcc, IBankAccount toAcc, PosOnlyMoney transferValue, IBankAccountTerms terms, DateTime dateTime)
    {
        TransferValue = transferValue;
        ToAccount = toAcc;
        FromAccount = fromAcc;
        DateTime = dateTime;
        Terms = terms;
        Cancelled = false;
    }

    public IBankAccount ToAccount { get; }

    public IBankAccount FromAccount { get; }

    public PosOnlyMoney TransferValue { get; }

    public DateTime DateTime { get; }

    public IBankAccountTerms Terms { get; }

    public bool Cancelled { get; private set; }

    public void CancelTransaction()
    {
        Cancelled = true;
    }
}