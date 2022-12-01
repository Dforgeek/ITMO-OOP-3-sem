using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DepositAccountFactory : IBankAccountFactory
{
    public void SetAccountTerms(IBankAccountTerms bankAccountTerms)
    {
        throw new NotImplementedException();
    }

    public IBankAccount CreateBankAccount(Bank bank, Client client, PosOnlyMoney balance)
    {
        throw new NotImplementedException();
    }
}