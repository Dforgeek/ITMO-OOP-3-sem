using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public interface IBankAccount
{
    Guid Id { get; }

    Bank Bank { get; }

    Client Client { get; }

    bool GetNotified { get; set; }

    TransactionLog Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime);

    void AddMoney(PosOnlyMoney money);

    void RemoveMoney(PosOnlyMoney money);

    void UndoTransactionConsequences(TransactionLog transactionLog);

    void UpdateAccruals();

    void WriteOffAccruals();

    void AcceptVisitor(IAccountTermsVisitor termsVisitor);
}