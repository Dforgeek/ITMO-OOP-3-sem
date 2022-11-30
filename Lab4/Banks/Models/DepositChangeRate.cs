namespace Banks.Models;

public record DepositChangeRate
{
    public DepositChangeRate(PosOnlyMoney threshold, decimal percent)
    {
        Threshold = threshold;
        Percent = percent;
    }

    public PosOnlyMoney Threshold { get; }
    public decimal Percent { get; }
}