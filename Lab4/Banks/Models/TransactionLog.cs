using System.Reflection.Emit;
using Banks.Interfaces;

namespace Banks.Models;

public record TransactionLog
{
    public TransactionLog(IBankAccount toAccount, IBankAccount fromAccount, decimal transferValue, DateTime dateTime)
    {
        if (transferValue <= 0)
            throw new Exception();
        TransferValue = transferValue;
        ToAccount = toAccount;
        FromAccount = fromAccount;
        DateTime = dateTime;
    }

    public IBankAccount ToAccount { get; }
    public IBankAccount FromAccount { get; }
    public decimal TransferValue { get; }
    public DateTime DateTime { get; }
}