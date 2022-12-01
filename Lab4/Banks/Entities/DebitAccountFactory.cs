using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DebitAccountFactory : IBankAccountFactory
{
    public DebitAccountFactory(DebitAccountTerms? debitAccountTerms = null)
    {
        DebitAccountTerms = null;
    }

    public DebitAccountTerms? DebitAccountTerms { get; private set; }

    public void SetAccountTerms(IBankAccountTerms bankAccountTerms)
    {
        if (bankAccountTerms is not DebitAccountTerms debitAccountTerms)
            throw new Exception();
        DebitAccountTerms = debitAccountTerms;
    }

    public IBankAccount CreateBankAccount(Bank bank, Client client, PosOnlyMoney balance)
    {
        if (DebitAccountTerms == null)
            throw new Exception();
        return new DebitAccount(bank, client, balance, DebitAccountTerms, Guid.NewGuid());
    }
}