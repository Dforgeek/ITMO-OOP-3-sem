using Banks.ValueObjects;

namespace Banks.Transactions;

public interface ITransaction
{
    Guid Id { get; }

    PosOnlyMoney TransferValue { get; }

    DateTime DateTime { get; }

    bool Cancelled { get; }

    void CancelTransaction();

    ITransaction Accept(ITransactionVisitor visitor);
}