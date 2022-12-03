using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class CreditAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateBankAccount(Bank bank, Client client, IMoney balance)
    {
        return new CreditAccount(bank, client, balance, Guid.NewGuid());
    }
}