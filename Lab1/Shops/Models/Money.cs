using Shops.Exceptions;

namespace Shops.Models;

public record Money
{
    public Money(decimal price)
    {
        if (price <= 0)
            throw MoneyException.MoneyNumberIsNegativeOrNull(price);
        Value = price;
    }

    public decimal Value { get; private set; }

    public void AddMoney(decimal newMoney)
    {
        Value += newMoney;
    }
}