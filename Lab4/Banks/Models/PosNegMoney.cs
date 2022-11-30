using Banks.Interfaces;

namespace Banks.Models;

public record PosNegMoney : IMoney
{
    public PosNegMoney(decimal value)
    {
        Value = value;
        IsNegative = value < 0;
    }

    public bool IsNegative { get; }
    public decimal Value { get; }
}