namespace Banks.Transactions;

public interface ITransactionVisitor
{
    WithdrawTransaction Visit(ReplenishTransaction transaction);

    TransferTransaction Visit(TransferTransaction transferTransaction);

    ReplenishTransaction Visit(WithdrawTransaction transaction);
}