using Banks.Interfaces;

namespace Banks.Models;

public class CreditAccountTerms : IBankAccountTerms
{
    public CreditAccountTerms(PosOnlyMoney unreliableClientLimit, PosNegMoney creditLimit, Percent commission)
    {
        UnreliableClientLimit = unreliableClientLimit;
        CreditLimit = creditLimit;
        Commission = commission;
    }

    public PosOnlyMoney UnreliableClientLimit { get; }

    public PosNegMoney CreditLimit { get; }

    public Percent Commission { get; }
}