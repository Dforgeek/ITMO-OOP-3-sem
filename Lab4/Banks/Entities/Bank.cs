using System.Security.Principal;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Bank
{
    private readonly List<IBankAccount> _bankAccounts;

    public Bank(string name, PosNegMoney transferLimit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        _bankAccounts = new List<IBankAccount>();
        TransferLimit = transferLimit;
    }

    public string Name { get; }
    public PosNegMoney TransferLimit { get; }
    public CreditAccountTerms CreditAccountTerms { get; private set; }
    public DebitAccountTerms DebitAccountTerms { get; private set; }
    public DepositAccountTerms DepositAccountTerms { get; private set; }

    public CreditAccount CreateCreditAccount(Client client, IMoney money)
    {
        var newAccount = new CreditAccount(this, client, money, Guid.NewGuid());
        _bankAccounts.Add(newAccount);
        return newAccount;
    }

    public void Transfer(Guid fromAccountId, Guid toAccountId, PosOnlyMoney money)
    {
        throw new NotImplementedException();
    }

    public void Update(CreditAccountTerms newCreditAccountTerms)
    {
        CreditAccountTerms = newCreditAccountTerms;
    }

    public void Update(DebitAccountTerms newDebitAccountTerms)
    {
        DebitAccountTerms = newDebitAccountTerms;
    }

    public void Update(DepositAccountTerms newDepositAccountTerms)
    {
        DepositAccountTerms = newDepositAccountTerms;
    }
}