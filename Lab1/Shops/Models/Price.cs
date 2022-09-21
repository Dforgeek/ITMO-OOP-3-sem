using Shops.Exceptions;

namespace Shops.Models;

public class Price
{
    public Price(decimal price)
    {
        if (price <= 0)
            throw new ShopException($"Invalid price: {price}");
        Value = price;
    }

    public decimal Value { get; }
}