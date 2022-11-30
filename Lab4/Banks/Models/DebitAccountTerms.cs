using Banks.Interfaces;

namespace Banks.Models;

public record DebitAccountTerms
{
    public DebitAccountTerms(PosOnlyMoney limit, decimal percentPerAnnum)
    {
        Limit = limit;
        PercentPerAnnum = percentPerAnnum;
    }

    public decimal PercentPerAnnum { get; }

    public PosOnlyMoney Limit { get; }
}