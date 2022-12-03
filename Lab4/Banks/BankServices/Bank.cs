using Banks.BankAccounts;
using Banks.BankAccountTerms;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankServices;

public class Bank : IBank
{
    private readonly List<IBankAccount> _bankAccounts;
    private readonly List<ITransaction> _transactions;

    public Bank(string name, IClock clock, IAccountTermsVisitor accountTermsVisitor, Guid id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        Clock = clock;
        Id = id;
        _bankAccounts = new List<IBankAccount>();
        _transactions = new List<ITransaction>();
        AccountTermsVisitor = accountTermsVisitor;
    }

    public string Name { get; }

    public IClock Clock { get; }

    public Guid Id { get; }

    public IReadOnlyCollection<IBankAccount> BankAccounts => _bankAccounts.AsReadOnly();

    public IReadOnlyCollection<ITransaction> Transactions => _transactions.AsReadOnly();

    public IAccountTermsVisitor AccountTermsVisitor { get; private set; }

    public IBankAccount GetBankAccount(Guid bankAccountId)
    {
        return _bankAccounts.FirstOrDefault(ba => ba.Id == bankAccountId) ?? throw new Exception();
    }

    public ITransaction GetTransaction(Guid transactionId)
    {
        return _transactions.FirstOrDefault(tr => tr.Id == transactionId) ?? throw new Exception();
    }

    public WithdrawTransaction Withdraw(Guid bankAccountId, PosOnlyMoney money)
    {
        WithdrawTransaction transaction = GetBankAccount(bankAccountId).Withdraw(money, Clock.Now);
        _transactions.Add(transaction);
        return transaction;
    }

    public ReplenishTransaction Replenish(Guid bankAccountId, PosOnlyMoney money)
    {
        ReplenishTransaction transaction = GetBankAccount(bankAccountId).Replenish(money, Clock.Now);
        _transactions.Add(transaction);
        return transaction;
    }

    public TransferTransaction Transfer(Guid fromAccountId, IBankAccount toAcc, PosOnlyMoney money)
    {
        IBankAccount from = GetBankAccount(fromAccountId);

        TransferTransaction transferTransaction = from.Transfer(toAcc, money, Clock.Now);

        _transactions.Add(transferTransaction);

        return transferTransaction;
    }

    public ITransaction UndoInBankTransfer(ITransaction transferTransaction)
    {
        var undoVisitor = new UndoTransactionVisitor(Clock.Now);
        ITransaction cancelling = transferTransaction.Accept(undoVisitor);
        _transactions.Add(cancelling);
        return cancelling;
    }

    public IBankAccount CreateBankAccount(IBankAccountFactory bankAccountFactory, Client client, IMoney money)
    {
        IBankAccount newAccount = bankAccountFactory
            .CreateBankAccount(this, client, new PosOnlyMoney(money.Value));
        newAccount.AcceptVisitor(AccountTermsVisitor);
        _bankAccounts.Add(newAccount);
        return newAccount;
    }

    public void UpdateAllAccruals()
    {
        foreach (IBankAccount bankAccount in _bankAccounts)
        {
            bankAccount.UpdateAccruals();
        }
    }

    public void WriteOffAllAccruals()
    {
        foreach (IBankAccount bankAccount in _bankAccounts)
        {
            bankAccount.WriteOffAccruals(Clock.Now);
        }
    }

    public void UpdateAccountTerms(IAccountTermsVisitor accountTermsVisitor)
    {
        AccountTermsVisitor = accountTermsVisitor;

        foreach (IBankAccount bankAccount in _bankAccounts)
        {
            bankAccount.AcceptVisitor(accountTermsVisitor);
        }
    }

    public void SubscribeToNotifications(Guid accountId, INotificationStrategy notificationStrategy)
    {
        GetBankAccount(accountId).AddNewNotificationStrategy(notificationStrategy);
    }
}