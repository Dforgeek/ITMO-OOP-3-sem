using Banks.BankAccounts;

namespace Banks.Transactions;

public class UndoTransactionVisitor : ITransactionVisitor
{
    public UndoTransactionVisitor(DateTime dateTime)
    {
        Now = dateTime;
    }

    public DateTime Now { get; }

    public WithdrawTransaction Visit(ReplenishTransaction transaction)
    {
        if (transaction.Cancelled)
            throw new Exception();

        IBankAccount bankAccount = transaction.BankAccount; // UndoTransactionConsequences?
        transaction.CancelTransaction();
        return bankAccount.Withdraw(transaction.TransferValue, Now);
    }

    public TransferTransaction Visit(TransferTransaction transferTransaction)
    {
        if (transferTransaction.Cancelled)
            throw new Exception();

        IBankAccount from = transferTransaction.FromAccount;
        IBankAccount to = transferTransaction.ToAccount;

        TransferTransaction
            cancelling = to.Transfer(from, transferTransaction.TransferValue, Now); // UndoTransactionConsequences?

        transferTransaction.CancelTransaction();
        return cancelling;
    }

    public ReplenishTransaction Visit(WithdrawTransaction transaction)
    {
        if (transaction.Cancelled)
            throw new Exception();

        IBankAccount bankAccount = transaction.BankAccount;
        transaction.CancelTransaction();
        return bankAccount.Replenish(transaction.TransferValue, Now);
    }
}