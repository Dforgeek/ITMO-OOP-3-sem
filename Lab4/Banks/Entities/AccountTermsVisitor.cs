using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class AccountTermsVisitor : IAccountTermsVisitor
{
    public AccountTermsVisitor(CreditAccountTerms credAccT, DebitAccountTerms debAccT, DepositAccountTerms depAccT)
    {
        CreditAccountTerms = credAccT;
        DebitAccountTerms = debAccT;
        DepositAccountTerms = depAccT;
    }

    public CreditAccountTerms CreditAccountTerms { get; }

    public DebitAccountTerms DebitAccountTerms { get; }

    public DepositAccountTerms DepositAccountTerms { get; }

    public CreditAccountTerms CreateAccountTerms(CreditAccount creditAccount)
    {
        throw new NotImplementedException();
    }

    public DebitAccountTerms CreateAccountTerms(DebitAccount debitAccount)
    {
        throw new NotImplementedException();
    }

    public DepositAccountTerms CreateAccountTerms(DepositAccount depositAccount)
    {
        throw new NotImplementedException();
    }
}