using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public interface IBankAccount
{
    Guid Id { get; }

    Bank Bank { get; }

    Client Client { get; }

    TransferTransaction Transfer(IBankAccount toAcc, PosOnlyMoney transferValue, DateTime dateTime);

    ReplenishTransaction Replenish(PosOnlyMoney money, DateTime dateTime);

    WithdrawTransaction Withdraw(PosOnlyMoney money, DateTime dateTime);

    void UndoTransactionConsequences(TransferTransaction transferTransaction, DateTime dateTime);

    void UpdateAccruals();

    void WriteOffAccruals(DateTime dateTime);

    void AddNewNotificationStrategy(INotificationStrategy notificationStrategy);

    void AcceptVisitor(IAccountTermsVisitor termsVisitor);
}