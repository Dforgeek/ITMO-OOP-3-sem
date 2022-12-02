using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DebitAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateBankAccount(Bank bank, Client client, IMoney balance)
    {
        return new DebitAccount(bank, client, balance, Guid.NewGuid());
    }
}