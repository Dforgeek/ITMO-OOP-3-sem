using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public record DepositChangeRate
{
    public DepositChangeRate(PosOnlyMoney threshold, Percent percent)
    {
        Threshold = threshold;
        Percent = percent;
    }

    public PosOnlyMoney Threshold { get; }

    public Percent Percent { get; }

    public override string ToString()
    {
        return $"Threshold: {Threshold.Value}, Percent per annum: {Percent.Value}%";
    }
}