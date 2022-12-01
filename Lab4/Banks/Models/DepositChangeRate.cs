namespace Banks.Models;

public record DepositChangeRate
{
    public DepositChangeRate(PosOnlyMoney threshold, Percent percent)
    {
        Threshold = threshold;
        Percent = percent;
    }

    public PosOnlyMoney Threshold { get; }

    public Percent Percent { get; }
}