using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public class CreditAccountTerms : IBankAccountTerms
{
    public CreditAccountTerms(PosOnlyMoney unreliableClientLimit, PosNegMoney creditLimit, PosOnlyMoney commission)
    {
        UnreliableClientLimit = unreliableClientLimit;
        CreditLimit = creditLimit;
        Commission = commission;
    }

    public PosOnlyMoney UnreliableClientLimit { get; }

    public PosNegMoney CreditLimit { get; }

    public PosOnlyMoney Commission { get; }
}