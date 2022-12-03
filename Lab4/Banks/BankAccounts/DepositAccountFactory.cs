using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.TimeManagement;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public class DepositAccountFactory : IBankAccountFactory
{
    public DepositAccountFactory(IClock clock)
    {
        Clock = clock;
    }

    public IClock Clock { get; }

    public IBankAccount CreateBankAccount(Bank bank, Client client, IMoney balance)
    {
        return new DepositAccount(bank, client, balance, Guid.NewGuid(), Clock.Now);
    }
}