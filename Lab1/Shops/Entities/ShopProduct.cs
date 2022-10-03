using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class ShopProduct
{
    public ShopProduct(string name, Money price, uint quantity = 1, Guid id = default)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        if (string.IsNullOrEmpty(name))
            throw ShopProductException.IsNull();
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Money Price { get; private set; }
    public uint Quantity { get; }

    public void ChangePrice(Money newPrice)
    {
        Price = newPrice;
    }
}