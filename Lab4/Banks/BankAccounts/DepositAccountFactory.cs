using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DepositAccountFactory : IBankAccountFactory
{
    public IBankAccount CreateBankAccount(Bank bank, Client client, IMoney balance)
    {
        return new DepositAccount(bank, client, balance, Guid.NewGuid());
    }
}