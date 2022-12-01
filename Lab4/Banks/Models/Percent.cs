namespace Banks.Models;

public class Percent
{
    public Percent(decimal value)
    {
        if (value < 0)
            throw new Exception();
    }

    public decimal GetInCoefficientForm => Value / 100;

    public decimal Value { get; }
}