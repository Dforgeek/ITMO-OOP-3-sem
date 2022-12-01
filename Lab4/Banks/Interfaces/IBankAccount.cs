using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccount
{
    Guid Id { get; }

    Bank Bank { get; }

    Client Client { get; }

    IMoney Balance { get; }

    void AddMoney(PosOnlyMoney money);

    void RemoveMoney(PosOnlyMoney money);

    void AddSumOfPercentsPerAnnumToBalance();

    void AcceptVisitor(IAccountTermsVisitor termsVisitor);
}