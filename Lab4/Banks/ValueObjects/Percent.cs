namespace Banks.ValueObjects;

public record Percent
{
    public Percent(decimal value)
    {
        if (value < 0)
            throw new Exception();

        Value = value;
    }

    public decimal GetInCoefficientForm => Value / 100;

    public decimal Value { get; }
}