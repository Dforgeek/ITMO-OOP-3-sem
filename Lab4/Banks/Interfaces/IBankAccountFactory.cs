using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccountFactory
{
    IBankAccount CreateBankAccount(Bank bank, Client client, PosOnlyMoney balance);
}