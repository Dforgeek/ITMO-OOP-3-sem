using Banks.Interfaces;

namespace Banks.Models;

public class DebitAccountTerms : IBankAccountTerms
{
    public DebitAccountTerms(PosOnlyMoney limit, Percent percentPerAnnum)
    {
        UnreliableClientLimit = limit;
        PercentPerAnnum = percentPerAnnum;
    }

    public Percent PercentPerAnnum { get; }

    public PosOnlyMoney UnreliableClientLimit { get; }
}