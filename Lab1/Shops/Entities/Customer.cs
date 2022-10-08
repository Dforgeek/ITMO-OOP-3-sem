using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    public Customer(string name, Money money)
    {
        if (string.IsNullOrEmpty(name))
            throw CustomerException.IsNull();
        Name = name;
        Money = money;
    }

    public string Name { get; }
    public Money Money { get; private set; }

    public void EarnMoney(Money moneyIncreaseAmount)
    {
        Money = new Money(moneyIncreaseAmount.Value + Money.Value);
    }

    public void SpendMoney(Money moneyDecreaseAmount)
    {
        Money = new Money(Money.Value - moneyDecreaseAmount.Value);
    }
}