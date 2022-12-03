using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public class DebitAccountTerms : IBankAccountTerms
{
    public DebitAccountTerms(PosOnlyMoney unreliableClientLimit, Percent percentPerAnnum)
    {
        UnreliableClientLimit = unreliableClientLimit;
        PercentPerAnnum = percentPerAnnum;
    }

    public Percent PercentPerAnnum { get; }

    public PosOnlyMoney UnreliableClientLimit { get; }
}