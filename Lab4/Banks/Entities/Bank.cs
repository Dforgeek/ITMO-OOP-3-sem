using System.Security.Principal;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Bank
{
    private readonly List<IBankAccount> _bankAccounts;
    private readonly List<TransactionLog> _transactionLogs;

    public Bank(string name, PosNegMoney unreliableTransferLimit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        _bankAccounts = new List<IBankAccount>();
        _transactionLogs = new List<TransactionLog>();
    }

    public string Name { get; }

    public IAccountTermsVisitor AccountTermsVisitor { get; private set; }

    public IBankAccount CreateBankAccount(IBankAccountFactory bankAccountFactory, Client client, IMoney money)
    {
        IBankAccount newAccount = bankAccountFactory
            .CreateBankAccount(this, client, new PosOnlyMoney(money.Value));
        newAccount.AcceptVisitor(AccountTermsVisitor);
        _bankAccounts.Add(newAccount);
        return newAccount;
    }

    public void UpdateAccountTerms(IAccountTermsVisitor accountTermsVisitor)
    {
        AccountTermsVisitor = accountTermsVisitor;
        foreach (IBankAccount bankAccount in _bankAccounts)
        {
            bankAccount.AcceptVisitor(accountTermsVisitor);
        }
    }

    public void Transfer(Guid fromAccountId, Guid toAccountId, PosOnlyMoney money)
    {
        throw new NotImplementedException();
    }
}