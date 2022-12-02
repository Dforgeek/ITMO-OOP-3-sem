using Banks.BankAccounts;
using Banks.BankAccountTerms;
using Banks.Entities;
using Banks.TimeManagement;
using Banks.ValueObjects;

namespace Banks.BankServices;

public class Bank : IBank
{
    private readonly List<IBankAccount> _bankAccounts;
    private readonly List<TransactionLog> _transactionLogs;

    public Bank(string name, IAccountTermsVisitor accountTermsVisitor)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        _bankAccounts = new List<IBankAccount>();
        _transactionLogs = new List<TransactionLog>();
        AccountTermsVisitor = accountTermsVisitor;
    }

    public string Name { get; }

    public IReadOnlyCollection<IBankAccount> BankAccounts => _bankAccounts.AsReadOnly();

    public IReadOnlyCollection<TransactionLog> TransactionLogs => _transactionLogs.AsReadOnly();

    public IAccountTermsVisitor AccountTermsVisitor { get; private set; }

    public IBankAccount GetBankAccount(Guid bankAccountId)
    {
        return _bankAccounts.FirstOrDefault(ba => ba.Id == bankAccountId) ?? throw new Exception();
    }

    public TransactionLog InBankTransfer(Guid fromAccountId, Guid toAccountId, PosOnlyMoney money, IClock clock)
    {
        IBankAccount from = GetBankAccount(fromAccountId);
        IBankAccount to = GetBankAccount(toAccountId);
        TransactionLog transactionLog = from.Transfer(to, money, clock.Now);
        _transactionLogs.Add(transactionLog);
        return transactionLog;
    }

    public void UndoInBankTransfer(TransactionLog transactionLog, IClock clock)
    {
        if (transactionLog.Cancelled)
            return;

        IBankAccount from = transactionLog.FromAccount;
        IBankAccount to = transactionLog.ToAccount;

        TransactionLog cancelling = to.Transfer(from, transactionLog.TransferValue, clock.Now);
        to.UndoTransactionConsequences(cancelling);
        from.UndoTransactionConsequences(transactionLog);

        transactionLog.CancelTransaction();
        _transactionLogs.Add(cancelling);
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
            bankAccount.WriteOffAccruals();
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

    public void TurnOnNotifications(Guid accountId)
    {
        GetBankAccount(accountId).GetNotified = true;
    }

    public void TurnOffNotifications(Guid accountId)
    {
        GetBankAccount(accountId).GetNotified = false;
    }
}