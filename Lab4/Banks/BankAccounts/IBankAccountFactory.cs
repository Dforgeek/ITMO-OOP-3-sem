using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.BankAccounts;

public interface IBankAccountFactory
{
    IBankAccount CreateBankAccount(Bank bank, Client client, IMoney balance);
}