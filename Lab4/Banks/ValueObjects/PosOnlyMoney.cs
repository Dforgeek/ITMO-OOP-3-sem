namespace Banks.ValueObjects;

public record PosOnlyMoney : IMoney
{
    public PosOnlyMoney(decimal value)
    {
        if (value < 0)
            throw new Exception();

        Value = value;
    }

    public decimal Value { get; }
}