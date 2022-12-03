using Banks.BankAccounts;

namespace Banks.BankAccountTerms;

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
        creditAccount.UpdateTerms(CreditAccountTerms);
        return CreditAccountTerms;
    }

    public DebitAccountTerms CreateAccountTerms(DebitAccount debitAccount)
    {
        debitAccount.UpdateTerms(DebitAccountTerms);
        return DebitAccountTerms;
    }

    public DepositAccountTerms CreateAccountTerms(DepositAccount depositAccount)
    {
        depositAccount.UpdateTerms(DepositAccountTerms);
        return DepositAccountTerms;
    }
}