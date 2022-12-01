using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IAccountTermsVisitor
{
    CreditAccountTerms CreateAccountTerms(CreditAccount creditAccount);

    DebitAccountTerms CreateAccountTerms(DebitAccount debitAccount);

    DepositAccountTerms CreateAccountTerms(DepositAccount depositAccount);
}