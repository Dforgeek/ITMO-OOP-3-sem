using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    public Customer(string name, decimal money)
    {
        if (string.IsNullOrEmpty(name))
            throw CustomerException.IsNull();
        Name = name;
        Money = new Money(money);
    }

    public string Name { get; }
    public Money Money { get; private set; }

    public void ChangeMoneyAmount(decimal newMoney)
    {
        Money = new Money(newMoney);
    }
}