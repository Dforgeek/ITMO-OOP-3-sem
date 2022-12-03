using Banks.BankAccounts;

namespace Banks.BankAccountTerms;

public interface IAccountTermsVisitor
{
    CreditAccountTerms CreateAccountTerms(CreditAccount creditAccount);

    DebitAccountTerms CreateAccountTerms(DebitAccount debitAccount);

    DepositAccountTerms CreateAccountTerms(DepositAccount depositAccount);
}